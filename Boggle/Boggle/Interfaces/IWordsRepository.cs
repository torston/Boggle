using System.Collections.Generic;

namespace Boggle.Interfaces
{
    public interface IWordsRepository
    {
        HashSet<string> Load();
    }
}