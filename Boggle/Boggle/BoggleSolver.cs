namespace Boggle
{
    public class BoggleSolver : ISolver
    {
        private readonly IValidator _validator;
        private readonly IWordFinder _wordFinder;
        private readonly ITrieHelper _trieHelper;
        private readonly IWordsRepository _wordsRepository;

        private TrieNode _cachedTrie;

        public BoggleSolver(
            ITrieHelper trieHelper, 
            IWordsRepository wordsRepository, 
            IWordFinder wordFinder, 
            IValidator validator
            )
        {
            _trieHelper = trieHelper;
            _wordsRepository = wordsRepository;
            _wordFinder = wordFinder;
            _validator = validator;
        }

        public IResults FindWords(char[,] inputBoard)
        {
            _validator.ValidateBoard(inputBoard);

            if (_cachedTrie == null)
            {
                var loadedWords = _wordsRepository.Load();

                _validator.ValidateDictionary(loadedWords);

                _cachedTrie = _trieHelper.MakeTrie(loadedWords);
            }

            var words = _wordFinder.FindWords(inputBoard, _cachedTrie);

            return new Result(words);
        }
    }
}
