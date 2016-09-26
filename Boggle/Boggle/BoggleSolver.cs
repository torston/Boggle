namespace Boggle
{
    public class BoggleSolver : ISolver
    {
        private IWordFinder _wordFinder;
        private ITrieHelper _trieHelper;
        private IWordsRepository _wordsRepository;

        private TrieNode cachedTrie;

        public BoggleSolver(ITrieHelper trieHelper, IWordsRepository wordsRepository, IWordFinder wordFinder)
        {
            _trieHelper = trieHelper;
            _wordsRepository = wordsRepository;
            _wordFinder = wordFinder;
        }

        public IResults FindWords(char[,] inputBoard)
        {
            if (cachedTrie == null)
            {
                var loadedWords = _wordsRepository.Load();
                cachedTrie = _trieHelper.MakeTrie(loadedWords);
            }

            var words = _wordFinder.FindWords(inputBoard, cachedTrie);

            return new Result(words);
        }
    }
}
