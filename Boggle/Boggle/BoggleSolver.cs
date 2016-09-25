using System.Collections.Generic;
using System.Linq;
using BogWord = System.Collections.Generic.List<Boggle.Letter>;

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
                    FindWord(i, j, string.Empty, new BogWord(), tree);
                }
            }

            return new Result(words);
        }

        public void FindWord(int r, int c, string prefix, List<Letter> word, TrieNode parentNode)
        {
            if (!board.IsInBoardRange(r, c))
            {
                return;
            }

            if (word.Any(bogLet => bogLet.Column == c && bogLet.Row == r))
            {
                return;
            }

            //if (prefix.Length == board.GetLength(0) * board.GetLength(1) + 1)
            //{
            //    return;
            //}

            word.Add(new Letter(r, c, board[r, c]));

            TrieNode node;

            if (parentNode.children.TryGetValue(board[r, c], out node))
            {
                if (board[r, c] == 'q')
                {
                    prefix += board[r, c];

                    TrieNode node2;
                    if (node.children.TryGetValue('u', out node2))
                    {
                        node = node2;
                    }
                    else
                    {
                        return;
                    }
                    prefix += 'u';
                }
                else
                {
                    prefix += board[r, c];
                }

                if (node.IsWord)
                {
                    words.Add(prefix);
                }

                for (var i = r - 1; i < r + 2; i++)
                {
                    for (var j = c - 1; j < c + 2; j++)
                    {
                        FindWord(i, j, prefix, word.ToList(), node);
                    }
                }
            }
        }
    }
}
