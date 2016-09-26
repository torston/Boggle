using System.Collections.Generic;

namespace Boggle
{
    public class TrieNode : ITrieNode
    {
        public ITrieNode Parent { get; private set; }
        public bool IsWord { get; set; }

        public Dictionary<char, ITrieNode> Children { get; }

        public TrieNode()
        {
            Children = new Dictionary<char, ITrieNode>();
        }

        public void SetParent(ITrieNode node)
        {
            Parent = node;
        }
    }
}