using System.Collections.Generic;
using System.IO;

namespace Boggle
{
    public class WordsRepository : IWordsRepository
    {
        private readonly string _path;

        public WordsRepository(string path)
        {
            _path = path;
        }

        public HashSet<string> Load()
        {
            if (!File.Exists(_path))
            {
                throw new FileNotFoundException("Dictionary not found!", _path);
            }

            var loadedWords = new HashSet<string>();

            using (var fs = File.Open(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var bs = new BufferedStream(fs))
            using (var sr = new StreamReader(bs))
            {
                string word;
                while ((word = sr.ReadLine()) != null)
                {
                    if (word.Length >= 3)
                    {
                        loadedWords.Add(word.Trim().ToLower());
                    }
                }
            }

            return loadedWords;
        }
    }
}