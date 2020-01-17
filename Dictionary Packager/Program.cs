using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dictionary_Packager.dictionary;
using Dictionary_Packager.Japanese;

namespace Dictionary_Packager
{
    class Program
    {
        private readonly string OUTPUT_PATH = "temp.cdf";

        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            Console.WriteLine("Start Writing...");
            var writer = new JapaneseDictionaryWriter(OUTPUT_PATH);

            for (var i = 0; i < 10; i++)
            {
                writer.TryReadingFromFile($"Japanese/dictionary/dictionary0{i}.txt", segments => new JapaneseDictEntry()
                {
                    Hiragana = segments[0],
                    DictionaryEntry = segments[4],
                    Cost = Convert.ToInt32(segments[3])
                });
            }

            writer.TryReadingFromFile("Japanese/dictionary/single_kanji.tsv", segments =>
            {
                var dictEntries = new List<JapaneseDictEntry>();
                foreach (var kanji in segments[1].ToCharArray())
                {
                    dictEntries.Add(new JapaneseDictEntry());
                }

                return dictEntries;
            });
            writer.Write();

            Console.WriteLine("Done...");
            Console.ReadLine();
        }
    }
}
