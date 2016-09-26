using System.Collections.Generic;

namespace Boggle
{
    public interface IWordsRepository
    {
        HashSet<string> Load();
    }
}