using System.Collections.Generic;
using System.Linq;

namespace Boggle
{
    public class BoggleSolver : ISolver
    {
        private readonly List<string> words = new List<string>();
        private char[,] board;
        private readonly TrieNode tree;

        public BoggleSolver(string dictionaryPath)
        {
            tree = new TrieHelper(dictionaryPath).MakeTrie();
        }

        public IResults FindWords(char[,] randomBoard)
        {
            board = randomBoard;

            for (var i = 0; i < randomBoard.GetLength(0); i++)
            {
                for (var j = 0; j < randomBoard.GetLength(1); j++)
                {
                    FindWord(i, j, string.Empty, new List<Letter>(), tree);
                }
            }

            return new Result(words);
        }

        public void FindWord(int r, int c, string prefix, List<Letter> path, TrieNode parentNode)
        {
            // Check indexes in bounds
            if (!board.IsInBoardRange(r, c))
            {
                return;
            }
            // Check same cell using
            if (path.Any(bogLet => bogLet.Column == c && bogLet.Row == r))
            {
                return;
            }
            // Check max word length
            if (prefix.Length == board.GetLength(0) * board.GetLength(1) + 1)
            {
                return;
            }

            // Check if word-part have next character
            TrieNode node;
            if (!parentNode.children.TryGetValue(board[r, c], out node))
            {
                return;
            }

            var character = board[r, c];

            path.Add(new Letter(r, c, character));

            prefix += character;

            if (character == 'q')
            {
                // using QHack function because if we have 'Q' letter in cell it's actually 'QU', so
                // we have get next node for 'U' letter
                if (QHack(ref node)) return;
            }

            if (node.IsWord)
            {
                words.Add(prefix);
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

        private static bool QHack(ref TrieNode node)
        {
            TrieNode node2;
            if (node.children.TryGetValue('u', out node2))
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
}
