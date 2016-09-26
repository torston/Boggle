using System.Collections.Generic;

namespace Boggle
{
    public class TrieHelper : ITrieHelper
    {
        private readonly HashSet<string> _loadedWords;
        private readonly ITrieNodeFactory _nodeFactory;

        public TrieHelper(IWordsRepository wordsRepository, ITrieNodeFactory factory)
        {
            _nodeFactory = factory;
            _loadedWords = wordsRepository.Load();
        }

        public ITrieNode MakeTrie()
        {
            var root = _nodeFactory.CrateNode();

            foreach (var word in _loadedWords)
            {
                var curNode = root;

                foreach (var letter in word)
                {
                    ITrieNode nextNode;
                    if (curNode.Children.ContainsKey(letter))
                    {
                        nextNode = curNode.Children[letter];
                    }
                    else
                    {
                        nextNode = _nodeFactory.CrateNode();
                        nextNode.SetParent(curNode);

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
        ITrieNode MakeTrie();
    }
}