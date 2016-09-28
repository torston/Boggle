using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Boggle.Tests.Tests
{
    [TestFixture]
    public class BoggleSolverTestsInOut
    {
        private readonly string _dictionaryPath = TestContext.CurrentContext.TestDirectory + "/Resources/dictionary.txt";
        private ISolver _solver;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            _solver = MyBoggleSolution.CreateSolver(_dictionaryPath);
        }

        [TestCase(3, 3, "aaaaaaaaa", new string[0])]
        [TestCase(3, 4, "aahaaaaaaaaa", new[] { "aah", "aha" })]
        [TestCase(3, 3, "aahaaaaaa", new[] { "aah", "aha" })]
        [TestCase(3, 3, "abcabcabc", new[] { "aba", "abba", "baa", "baba" })]
        [TestCase(3, 3, "dzxeaiqut", new[] { "daze", "daut", "zed", "zax", "zit", "eau", "eat", "adz", "adze", "ait", "qua", "quad", "quai", "uta", "tad", "tax", "taxi", "tae", "tau", "tui", "tuque" })]
        public void from_task_values_test(int rows, int columns, string boardString, string[] foundWords)
        {
            char[,] board = new char[rows, columns];

            board = board.CreateBoardFromString(boardString);

            var actual = _solver.FindWords(board);

            var set = new HashSet<string>();

            foreach (var foundWord in foundWords)
            {
                set.Add(foundWord);
            }

            var expected = new Result(foundWords);

            Console.WriteLine(string.Join(", ", actual.Words));
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
