using System.Collections.Generic;

namespace Boggle
{
    public class TrieNode
    {
        public TrieNode Parent { get; private set; }
        public bool IsWord { get; set; }

        public Dictionary<char, TrieNode> Children { get; }

        public TrieNode()
        {
            Children = new Dictionary<char, TrieNode>();
        }

        public TrieNode(TrieNode parent) : this()
        {
            Parent = parent;
        }
    }
}