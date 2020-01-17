using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary_Packager.Japanese;
using Newtonsoft.Json;

namespace Mocz_Parser
{
    public class MoczModel
    {

        public List<JapaneseDictEntry> Ipadic { get; private set; } = new List<JapaneseDictEntry>();

        public void Load()
        {
            Task.Run(() =>
            {
                using (var stream = File.Open("dictionary/dictionary.cdf", FileMode.Open))
                {
                    using (var gzipStream = new GZipStream(stream, CompressionMode.Decompress))
                    {
                        using (var sr = new StreamReader(gzipStream, Encoding.UTF8))
                        {
                            var content = sr.ReadToEnd();
                            Ipadic = JsonConvert.DeserializeObject<List<JapaneseDictEntry>>(content);
                        }
                    }
                }
            });
        }

    }
}
