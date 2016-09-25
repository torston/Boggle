using System.Collections.Generic;

namespace Boggle
{
    public class TrieNode
    {
        public TrieNode Parent { get; }
        public bool IsWord { get; set; }

        public Dictionary<char, TrieNode> children = new Dictionary<char, TrieNode>();

        public TrieNode() { }

        public TrieNode(TrieNode node)
        {
            Parent = node;
        }
    }
}