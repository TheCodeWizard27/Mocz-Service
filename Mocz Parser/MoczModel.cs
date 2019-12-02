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

        public List<Tuple<int, Tuple<string, string>>> Ipadic { get; private set; } = new List<Tuple<int, Tuple<string, string>>>();

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
            Ipadic.Add(
                new Tuple<int, Tuple<string, string>>(index*1000+Convert.ToInt32(parts[1]), new Tuple<string, string>(parts[0], parts[4]))
                );
        }

    }
}
