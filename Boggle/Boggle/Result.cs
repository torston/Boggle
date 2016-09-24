using System.Collections.Generic;

namespace Boggle
{
    public class Result : IResults
    {
        public Result(IEnumerable<string> words, int score)
        {
            Words = words;
            Score = score;
        }

        public IEnumerable<string> Words { get; }
        public int Score { get; }
    }
}
