using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Boggle
{
    public class BoggleSolver : ISolver
    {
        private static readonly Regex EnglishLettersPattern = new Regex("^[a-zA-Z]*$", RegexOptions.Compiled);

        private readonly IWordFinder _wordFinder;
        private readonly ITrieHelper _trieHelper;
        private readonly IWordsRepository _wordsRepository;

        private TrieNode _cachedTrie;

        public BoggleSolver(ITrieHelper trieHelper, IWordsRepository wordsRepository, IWordFinder wordFinder)
        {
            _trieHelper = trieHelper;
            _wordsRepository = wordsRepository;
            _wordFinder = wordFinder;
        }

        public IResults FindWords(char[,] inputBoard)
        {
            ValidateBoard(inputBoard);

            if (_cachedTrie == null)
            {
                var loadedWords = _wordsRepository.Load();
                ValidateDictionary(loadedWords);

                _cachedTrie = _trieHelper.MakeTrie(loadedWords);
            }

            var words = _wordFinder.FindWords(inputBoard, _cachedTrie);

            return new Result(words);
        }

        private static void ValidateBoard(char[,] inputBoard)
        {
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

        private static void ValidateDictionary(IEnumerable<string> loadedWords)
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
