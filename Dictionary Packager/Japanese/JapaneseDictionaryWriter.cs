using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary_Packager.Japanese;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dictionary_Packager.dictionary
{
    public class JapaneseDictionaryWriter
    {
        public string OutputPath { get; private set; }

        public readonly List<JapaneseDictEntry> _dict = new List<JapaneseDictEntry>();

        public JapaneseDictionaryWriter(string outputPath)
        {
            OutputPath = outputPath;
        }

        public JapaneseDictionaryWriter Write()
        {
            using (var stream = File.OpenWrite(OutputPath))
            {
                using (var gzipStream = new GZipStream(stream, CompressionMode.Compress))
                {
                    var dictContent = JsonConvert.SerializeObject(_dict);
                    var dictByteArr = Encoding.UTF8.GetBytes(dictContent);
                    gzipStream.Write(dictByteArr, 0, dictByteArr.Length);
                }
            }

            return this;
        }

        public JapaneseDictionaryWriter ReadFromFile(string filePath, Func<string[], JapaneseDictEntry> filter, char separator = '\t')
        {
            DoForeachLine(filePath, segments =>
            {
                _dict.Add(filter(segments));
            }, separator);
            return this;
        }

        public JapaneseDictionaryWriter ReadFromFile(string filePath, Func<string[], IEnumerable<JapaneseDictEntry>> filter,
            char separator = '\t')
        {
            DoForeachLine(filePath, segments =>
            {
                _dict.AddRange(filter(segments));
            },separator);
            return this;
        }

        public JapaneseDictionaryWriter TryReadingFromFile(string filePath, Func<string[], JapaneseDictEntry> filter,
            char separator = '\t')
        {
            try
            {
                ReadFromFile(filePath, filter, separator);
            }
            catch (Exception)
            {
                // ignored
            }

            return this;
        }
        public JapaneseDictionaryWriter TryReadingFromFile(string filePath, Func<string[], IEnumerable<JapaneseDictEntry>> filter,
            char separator = '\t')
        {
            try
            {
                ReadFromFile(filePath, filter, separator);
            }
            catch (Exception)
            {
                // ignored
            }

            return this;
        }


        private void DoForeachLine(string filePath, Action<string[]> action, char separator = '\t')
        {
            using (var fr = new StreamReader(File.Open(filePath, FileMode.Open)))
            {
                string[] segments;

                var line = fr.ReadLine();
                while (line != null)
                {
                    if (string.IsNullOrEmpty(line)) continue;
                    segments = line.Split(separator);

                    action(segments);

                    line = fr.ReadLine();
                }


            }
        }

    }
}
