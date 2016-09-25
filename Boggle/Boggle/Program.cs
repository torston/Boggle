using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static char[,] board;

        public static HashSet<string> res = new HashSet<string>();

        private static void Main(string[] args)
        {
            board = new char[rows, colums];
            _dict = new Dict("./dictionary.txt");

            tree = MakeTrie();

            var g =
                "adz adze ait daut daze eat eau qua quad quai tad tae tau tax taxi tui tuque uta zax zed zit".Split(' ');

            for (var i = 0; i < colums; i++)
            {
                for (var j = 0; j < rows; j++)
                {
                    board[i, j] = GetLetter();
                }
            }

            PrintBoard();

            //Console.WriteLine();

            //foreach (var VARIABLE in w)
            //{
            //    Console.WriteLine(VARIABLE);
            //}


            //return;
            string prefix = string.Empty;

            var word1 = new BogWord();

            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (var i = 0; i < colums; i++)
            {
                for (var j = 0; j < rows; j++)
                {
                    FindWord(i, j, prefix, word1.ToList(),tree);
                }
            }
            Console.WriteLine("Time: " + timer.ElapsedMilliseconds / 1000f + " seconds " + y);

            int counter = 0;

            foreach (var word in words)
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
                //Console.WriteLine("My word: " + word);
                if (!g.Contains(word))
                {
                    Console.WriteLine("Missed word: " + word);
                }
                //Console.WriteLine(string.Concat(word.Select(w => w.Word)));
            }

            foreach (var s in g)
            {
                //Console.WriteLine("Task word: " + s);
                if (!words.Contains(s))
                {
                    Console.WriteLine("My Missed word: " + s);
                }
            }

            Console.WriteLine("g: " + g.Length);

            Console.WriteLine("Score: " + counter);
            Console.WriteLine("Count: " + words.Count);

        }


        public static TrieNode MakeTrie()
        {
            var root = new TrieNode();

            foreach (var word in _dict.array)
            {
                var curNode = root;

                foreach (var letter in word)
                {
                    TrieNode nextNode;
                    if (curNode.children.ContainsKey(letter))
                    {
                        nextNode = curNode.children[letter];
                    }
                    else
                    {
                        nextNode = new TrieNode(curNode);
                        curNode.children.Add(letter, nextNode);
                    }

                    curNode = nextNode;
                }
                curNode.isWord = true;
            }

            return root;
        }

        public static List<string> FindWords()
        {
            var queue = new Queue<TrieNodeLet>();
            var words = new List<string>();

            for (int i = 0; i < colums; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    var c = board[j, i];

                    TrieNode node;
                    if (tree.children.TryGetValue(c, out node))
                    {
                        queue.Enqueue(new TrieNodeLet(j, i, c.ToString(), node));
                    }
                }
            }

            //TrieNode node2;
            //if (tree.children.TryGetValue('a', out node2))
            //{
            //    queue.Enqueue(new TrieNodeLet(1, 1, 'e'.ToString(), node2));
            //}


            var d = 0;

            while (queue.Count > 0)
            {
                var let = queue.Dequeue();
                d++;

                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        var newRow = let.Row + i;
                        var newCol = let.Col + j;

                        if (board.In2DArrayBounds(newRow, newCol) && i != 0 && j != 0)
                        {
                            var prefix = let.Word + (board[newRow, newCol] == 'q' ? "qu" : board[newRow, newCol].ToString());
                            TrieNode node;

                            if (let.Node.children.TryGetValue(board[newRow, newCol], out node))
                            {
                                if (node.isWord)
                                {
                                    words.Add(prefix);
                                }

                                queue.Enqueue(new TrieNodeLet(newRow, newCol, prefix, node));
                            }
                        }
                    }
                }

            }
            Console.WriteLine(d);
            return words;
        }

        public static void PrintBoard()
        {
            for (var i = 0; i < colums; i++)
            {
                for (var j = 0; j < rows; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static int y = 0;

        public static HashSet<string> words = new HashSet<string>();

        public static void FindWord(int r, int c, string prefix, BogWord word, TrieNode parentNode)
        {
            if (!board.In2DArrayBounds(r, c))
            {
                return;
            }

            if (word.Any(bogLet => bogLet.Col == c && bogLet.Row == r))
            {
                return;
            }

            if (prefix.Length == rows * colums)
            {
                return;
            }


            word.Add(new BogLet(r, c, board[r, c]));

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

                if (node.isWord)
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



            //if (_dict.IsWord(prefix))
            //{
            //    //Console.WriteLine("word :" + prefix);
            //    res.Add(prefix);
            //    //return true;
            //}

            //if (!_dict.IsPrefix(prefix))
            //{
            //    return;
            //}

            //for (var i = r - 1; i < r + 2; i++)
            //{
            //    for (var j = c - 1; j < c + 2; j++)
            //    {
            //        FindWord(i, j, prefix, word.ToList());
            //    }
            //}
        }

        private static int index = -1;
        private static TrieNode tree;

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

    public class TrieNodeLet
    {
        public int Row { get; }
        public int Col { get; }
        public string Word { get; }
        public TrieNode Node { get; set; }


        public TrieNodeLet(int row, int col, string word, TrieNode node)
        {
            Row = row;
            Col = col;
            Word = word;
            Node = node;
        }

    }

    public class Dict
    {
        public HashSet<string> array;

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

    public class TrieNode
    {
        public TrieNode parent;
        public Dictionary<char, TrieNode> children = new Dictionary<char, TrieNode>();
        public bool isWord;

        public TrieNode() { }

        public TrieNode(TrieNode curNode)
        {
            parent = curNode;
        }
    }



}
