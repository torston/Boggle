using System.Collections.Generic;
using System.Linq;

namespace Boggle
{
    public partial class WordFinder : IWordFinder
    {
        private HashSet<string> _words = new HashSet<string>();
        private char[,] _board;

        public HashSet<string> FindWords(char[,] board, TrieNode tree)
        {
            _board = board;

            for (var i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    FindWord(i, j, string.Empty, new List<PathNode>(), tree);
                }
            }

            return _words;
        }

        private void FindWord(int r, int c, string prefix, ICollection<PathNode> path, TrieNode parentNode)
        {
            // Check indexes in bounds
            if (!_board.IsInBoardRange(r, c))
            {
                return;
            }
            // Check same cell using
            if (path.Any(pathNode => pathNode.Column == c && pathNode.Row == r))
            {
                return;
            }
            // Check max word length
            if (prefix.Length == _board.GetLength(0) *_board.GetLength(1) + 1)
            {
                return;
            }

            // Check if word-part have next character
            TrieNode node;
            if (!parentNode.Children.TryGetValue(_board[r, c], out node))
            {
                return;
            }

            var character = _board[r, c];

            path.Add(new PathNode(r, c));

            prefix += character;

            // using HandleQ function because if we have 'q' letter in cell it's actually 'qu', so
            // we have get next node for 'U' letter
            if (character == 'q')
            {
                if (HandleQ(ref node)) return;

                prefix += 'u';
            }

            if (node.IsWord)
            {
                _words.Add(prefix);
            }

            // Checking neighbor cells
            for (var i = r - 1; i < r + 2; i++)
            {
                for (var j = c - 1; j < c + 2; j++)
                {
                    FindWord(i, j, prefix, path.ToList(), node);
                }
            }
        }

        private bool HandleQ(ref TrieNode node)
        {
            TrieNode node2;
            if (node.Children.TryGetValue('u', out node2))
            {
                node = node2;
            }
            else
            {
                return true;
            }
            return false;
        }
    }

    public interface IWordFinder
    {
        HashSet<string> FindWords(char[,] board, TrieNode tree);
    }
}