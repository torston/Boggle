using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BogWord = System.Collections.Generic.List<Boggle.BogLet>;

namespace Boggle
{
    internal class Program
    {
        public static int rows = 3;
        public static int colums = 3;

        private static readonly Random Rand = new Random();
        private static Dict _dict;

        public static List<List<char>> board;

        public static List<BogWord> gameResult = new List<BogWord>();

        public static HashSet<string> res = new HashSet<string>();

        private static void Main(string[] args)
        {
            board = new List<List<char>>(colums);
            _dict = new Dict("./dictionary.txt");

            var g =
                "adz adze ait daut daze eat eau qua quad quai tad tae tau tax taxi tui tuque uta zax zed zit".Split(' ');

            //foreach (var s in g)
            //{
            //    Console.WriteLine(_dict.IsWord(s) + " " + s);
            //}

            //return;
            for (var i = 0; i < colums; i++)
            {
                var row = new List<char>(rows);

                for (var j = 0; j < rows; j++)
                {
                    row.Add(GetLetter());
                }

                board.Add(row);
            }

            PrintBoard();

            string prefix = string.Empty;

            for (var i = 0; i < colums; i++)
            {
                for (var j = 0; j < rows; j++)
                {
                    var word = new BogWord();

                    FindWord(i, j, prefix, word.ToList());
                }
            }

            int counter = 0;

            foreach (var word in res)
            {
                if (word.Length <= 4)
                {
                    counter += 1;
                }
                else if (word.Length == 5)
                {
                    counter += 2;
                }
                else if (word.Length == 6)
                {
                    counter += 3;
                }
                else if (word.Length == 7)
                {
                    counter += 5;
                }
                else if (word.Length >= 8)
                {
                    counter += 11;
                }

                if (!g.Contains(word))
                {
                    Console.WriteLine("Missed word: " + word);
                }
                //Console.WriteLine(string.Concat(word.Select(w => w.C)));
            }

            foreach (var s in g)
            {
                if (!res.Contains(s))
                {
                    Console.WriteLine("My Missed word: " + s);
                }
            }

            Console.WriteLine("g: " + g.Length);

            Console.WriteLine("Score: " + counter);
            Console.WriteLine("Count: " + res.Count);

        }

        public static void PrintBoard()
        {
            for (var i = 0; i < colums; i++)
            {
                for (var j = 0; j < rows; j++)
                {
                    Console.Write(board[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static void FindWord(int r, int c, string prefix, BogWord word)
        {
            if (!board.InRange(r, c))
            {
                return;
            }

            if (word.Any(bogLet => bogLet.Col == c && bogLet.Row == r))
            {
                return;
            }

            if (board[r][c] == 'q')
            {
                prefix += board[r][c];
                prefix += 'u';
            }
            else
            {
                prefix += board[r][c];
            }

            if (prefix.Length > rows * colums)
            {
                return;
            }

            word.Add(new BogLet(r, c, board[r][c]));

            if (_dict.IsWord(prefix))
            {
                Console.WriteLine("word :" + prefix);
                res.Add(prefix);
                //return true;
            }

            if (!_dict.IsPrefix(prefix))
            {
                return;
            }

            for (var i = r - 1; i < r + 2; i++)
            {
                for (var j = c - 1; j < c + 2; j++)
                {
                    FindWord(i, j, prefix, word.ToList());
                }
            }
        }

        private static int index = -1;

        public static char GetLetter()
        {
            //const string chars = "abcdefghijklmnopqrstuvwxyz";

            //var num = Rand.Next(0, chars.Length - 1);
            //return chars[num];
            index++;
            return "dzxeaiqut"[index];
            //return "sexredhat"[index];
        }
    }

    public struct BogLet
    {
        public int Row { get; }
        public int Col { get; }
        public char C { get; }

        public BogLet(int row, int col, char c)
        {
            Row = row;
            Col = col;
            C = c;
        }
    }

    public class Dict
    {
        private HashSet<string> array;

        public Dict(string path)
        {
            Load(path);
        }

        private void Load(string path)
        {
            array = new HashSet<string>();

            using (var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var bs = new BufferedStream(fs))
            using (var sr = new StreamReader(bs))
            {
                string word;
                while ((word = sr.ReadLine()) != null)
                {
                    if (word.Length >= 3)
                    {
                        array.Add(word.Trim().ToLower());
                    }
                }
            }
        }

        public bool IsWord(string word)
        {
            return array.Contains(word);
        }

        public bool IsPrefix(string word)
        {
            return array.Any(w => w.StartsWith(word));
        }
    }
}
