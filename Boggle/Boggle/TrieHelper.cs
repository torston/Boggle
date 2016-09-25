using System.Collections.Generic;
using System.IO;

namespace Boggle
{
    public class TrieHelper
    {
        private HashSet<string> loadedWords;

        public TrieHelper(string path)
        {
            Load(path);
            MakeTrie();
        }

        public TrieNode MakeTrie()
        {
            var root = new TrieNode();

            foreach (var word in loadedWords)
            {
                var curNode = root;

                foreach (var letter in word)
                {
                    TrieNode nextNode;
                    if (curNode.children.ContainsKey(letter))
                    {
                        nextNode = curNode.children[letter];
                    }
                    else
                    {
                        nextNode = new TrieNode(curNode);
                        curNode.children.Add(letter, nextNode);
                    }

                    curNode = nextNode;
                }
                curNode.IsWord = true;
            }

            return root;
        }

        private void Load(string path)
        {
            loadedWords = new HashSet<string>();

            using (var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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
        }
    }
}