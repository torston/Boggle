using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit2;

namespace Boggle.Tests
{
    [TestFixture]
    public class BoggleSolverTests
    {
        [Test, AutoNSubstituteData]
        public void findwords_call_validator_validateboard(
            [Frozen] IValidator validator,
            BoggleSolver sut,
            char[,] board
            )
        {
            sut.FindWords(board);

            validator.Received().ValidateBoard(board);
        }

        [Test, AutoNSubstituteData]
        public void findwords_call_wordrepository_load(
            [Frozen] IWordsRepository repository,
            BoggleSolver sut,
            char[,] board
            )
        {
            sut.FindWords(board);

            repository.Received().Load();
        }

        [Test, AutoNSubstituteData]
        public void findwords_call_validator_validatewords(
            [Frozen] IValidator validator,
            [Frozen] IWordsRepository repository,
            BoggleSolver sut,
            char[,] board,
            HashSet<string> words
            )
        {
            repository.Load().Returns(words);

            sut.FindWords(board);

            validator.Received().ValidateDictionary(words);
        }

        [Test, AutoNSubstituteData]
        public void findwords_call_triehelper_maketrie(
            [Frozen] IWordsRepository repository,
            [Frozen] ITrieHelper trieHelper,
            BoggleSolver sut,
            char[,] board,
            HashSet<string> words
            )
        {
            repository.Load().Returns(words);

            sut.FindWords(board);

            trieHelper.Received().MakeTrie(words);
        }


        [Test, AutoNSubstituteData]
        public void findwords_call_wordfinder_findwords(
            [Frozen] IWordsRepository repository,
            [Frozen] ITrieHelper trieHelper,
            [Frozen] IWordFinder wordFinder,
            BoggleSolver sut,
            char[,] board,
            HashSet<string> words,
            TrieNode trieNode
            )
        {
            repository.Load().Returns(words);
            trieHelper.MakeTrie(words).Returns(trieNode);

            sut.FindWords(board);

            wordFinder.Received().FindWords(board, trieNode);
        }


        [Test, AutoNSubstituteData]
        public void happy_path(
             [Frozen] IWordsRepository repository,
             [Frozen] ITrieHelper trieHelper,
             [Frozen] IWordFinder wordFinder,
             BoggleSolver sut,
             char[,] board,
             HashSet<string> words,
             TrieNode trieNode,
             HashSet<string> foundWords
             )
        {
            repository.Load().Returns(words);
            trieHelper.MakeTrie(words).Returns(trieNode);
            wordFinder.FindWords(board, trieNode).Returns(foundWords);

            var expected = new Result(foundWords);
            var actual = sut.FindWords(board);

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test, AutoNSubstituteData]
        public void if_cached_not_call_wordrepository_trie_helper_validatedictionary(
            [Frozen] IWordsRepository repository,
            [Frozen] ITrieHelper trieHelper,
            [Frozen] IValidator validator,
            [Frozen] IWordFinder wordFinder,
            BoggleSolver sut,
            char[,] board,
            HashSet<string> words,
            TrieNode trieNode
            )
        {
            repository.Load().Returns(words);
            trieHelper.MakeTrie(words).Returns(trieNode);

            sut.FindWords(board);
            sut.FindWords(board);

            repository.Received(1).Load();
            validator.Received(1).ValidateDictionary(words);
            trieHelper.Received(1).MakeTrie(words);
            wordFinder.Received(2).FindWords(board, trieNode);
        }
    }
}
