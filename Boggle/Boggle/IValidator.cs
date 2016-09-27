using System.Collections.Generic;

namespace Boggle
{
    public interface IValidator
    {
        void ValidateBoard(char[,] inputBoard);
        void ValidateDictionary(IEnumerable<string> loadedWords);
    }
}