using System.Collections.Generic;

namespace Boggle
{
    public class Result : IResults
    {
        public IEnumerable<string> Words { get; }
        public int Score { get; }

        public Result(IEnumerable<string> words)
        {
            Words = words;
            Score = CalculateScore();
        }

        private int CalculateScore()
        {
            var counter = 0;

            foreach (var word in Words)
            {
                switch (word.Length)
                {
                    case 3:
                    case 4:
                        counter += 1;
                        break;
                    case 5:
                        counter += 2;
                        break;
                    case 6:
                        counter += 3;
                        break;
                    case 7:
                        counter += 5;
                        break;
                    default:
                        if (word.Length >= 8)
                        {
                            counter += 11;
                        }
                        break;
                }
            }

            return counter;
        }
    }
}
