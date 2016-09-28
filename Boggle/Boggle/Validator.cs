using System;
using System.Collections.Generic;

namespace Boggle
{
    public class Validator : IValidator
    {
        public void ValidateBoard(char[,] inputBoard)
        {
            if (inputBoard == null)
            {
                throw new ArgumentException("Board can not be null");
            }
            if (inputBoard.GetUpperBound(0) < 2 || inputBoard.GetUpperBound(1) < 2)
            {
                throw new ArgumentException("Board size should be grater or equals 3x3");
            }

            foreach (var letter in inputBoard)
            {
                if (letter.IsNonEnglishCharacter())
                {
                    throw new ArgumentException(
                        $"Board have non english character: '{letter}', support only english characters");
                }
            }
        }

        public void ValidateDictionary(IEnumerable<string> loadedWords)
        {
            foreach (var loadedWord in loadedWords)
            {
                foreach (var letter in loadedWord.ToCharArray())
                {
                    if (letter.IsNonEnglishCharacter())
                    {
                        throw new ArgumentException(
                            $"Dictionary word have non english character: '{letter}' in word {loadedWord}," +
                            " support only english characters");
                    }

                }
            }
        }
    }
}