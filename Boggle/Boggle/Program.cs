using System;

namespace Boggle
{
    internal class Program
    {
        private const int Rows = 3;
        private const int Colums = 3;

        private static char[,] _board;

        private static void Main(string[] args)
        {
            CreateRandomBoard();

            PrintBoard();

            ISolver solver = MyBoggleSolution.CreateSolver("dictionary.txt");
            IResults result = solver.FindWords(_board);

            Console.WriteLine($"The score is: {result.Score} points!");
            Console.WriteLine($"Words are: \n {string.Join(", ", result.Words)}");

        }

        private static void CreateRandomBoard()
        {
            _board = new char[Rows, Colums];

            for (var i = 0; i < Colums; i++)
            {
                for (var j = 0; j < Rows; j++)
                {
                    _board[i, j] = "dzxeaiqut"[j + Colums * i];
                }
            }
        }

        private static void PrintBoard()
        {
            for (var i = 0; i < Colums; i++)
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
