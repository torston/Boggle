using System;

namespace Boggle
{
    internal class Program
    {
        private const string PathToDictionary = "dictionary.txt";
        private const int Rows = 3;
        private const int Columns = 3;

        private static char[,] _board;

        private static void Main(string[] args)
        {
            CreateRandomBoard();

            PrintBoard();

            var solver = MyBoggleSolution.CreateSolver(PathToDictionary);
            var result = solver.FindWords(_board);

            Console.WriteLine($"The score is: {result.Score} points!");
            Console.WriteLine($"Words are: \n {string.Join(", ", result.Words)}");
        }

        private static void CreateRandomBoard()
        {
            _board = new char[Rows, Columns];

            for (var i = 0; i < Columns; i++)
            {
                for (var j = 0; j < Rows; j++)
                {
                    _board[i, j] = "dzxeaiqut"[j + Columns * i];
                }
            }
        }

        private static void PrintBoard()
        {
            for (var i = 0; i < Columns; i++)
            {
                for (var j = 0; j < Rows; j++)
                {
                    Console.Write(_board[i, j] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
