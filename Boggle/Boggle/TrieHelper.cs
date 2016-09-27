using System;
using System.Collections.Generic;

namespace Boggle
{
    public class TrieHelper : ITrieHelper
    {
        public TrieNode MakeTrie(HashSet<string> loadedWords)
        {
            if (loadedWords == null)
            {
                throw new ArgumentException("Word hashtable cannot be null");
            }

            var root = new TrieNode();

            foreach (var word in loadedWords)
            {
                var curNode = root;

                foreach (var letter in word)
                {
                    TrieNode nextNode;
                    if (curNode.Children.ContainsKey(letter))
                    {
                        nextNode = curNode.Children[letter];
                    }
                    else
                    {
                        nextNode = new TrieNode(curNode);

                        curNode.Children.Add(letter, nextNode);
                    }

                    curNode = nextNode;
                }
                curNode.IsWord = true;
            }

            return root;
        }
    }

    public interface ITrieHelper
    {
        TrieNode MakeTrie(HashSet<string> loadedWords);
    }
}