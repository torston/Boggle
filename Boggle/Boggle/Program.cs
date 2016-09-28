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
            _board = new char[3,3];
            _board = _board.CreateBoardFromString("dzxeaiqut");

            PrintBoard();

            var solver = MyBoggleSolution.CreateSolver(PathToDictionary);
            var result = solver.FindWords(_board);

            Console.WriteLine($"The score is: {result.Score} points!");
            Console.WriteLine($"Words are: \n {string.Join("\", \"", result.Words)}");
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
