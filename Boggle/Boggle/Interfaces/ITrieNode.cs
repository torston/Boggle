using System.Collections.Generic;

namespace Boggle
{
    public interface ITrieNode
    {
        ITrieNode Parent { get; }
        bool IsWord { get; set; }
        Dictionary<char, ITrieNode> Children { get; }
        void SetParent(ITrieNode node);
    }
}