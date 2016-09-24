using System.Collections.Generic;

namespace Boggle
{
    public interface IResults
    {
        IEnumerable<string> Words { get; } // unique found words
        int Score { get; }                 // total score for all words found
    }
}