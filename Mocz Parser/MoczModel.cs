using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocz_Parser
{
    public class MoczModel
    {

        public List<DictionaryEntry> Ipadic { get; private set; } = new List<DictionaryEntry>();

        public void Load()
        {

            for (var i = 0; i < 10; i++)
            {
                var lines = File.ReadLines($"dictionary/dictionary0{i}.txt", Encoding.UTF8);

                foreach (var line in lines) LoadLine(i, line);
            }

        }

        /// <summary>
        /// Loads the line into the List.
        /// </summary>
        /// <param name="index">Index of Dictionary page.</param>
        /// <param name="line">Line as string of Dictionary page.</param>
        private void LoadLine(int index, string line)
        {
            var parts = line.Split('\t');
            Ipadic.Add(new DictionaryEntry()
            {
                InputText = parts[0],
                Conversion = parts[4],
                Cost = Convert.ToInt32(parts[3]),
                LeftContextId = Convert.ToInt32(parts[1]),
                RightContextId = Convert.ToInt32(parts[2])
            });
        }

    }
}
