using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using FluentAssertions;

namespace Boggle.Tests
{
    [TestFixture]
    public class DictionaryLoadTests
    {
        [Test]
        public void load_dictionary_empty_pass_exception()
        {
            var wordsRepository = new WordsRepository("");

            wordsRepository.Invoking(r => r.Load()).ShouldThrow<FileNotFoundException>();
        }

        [Test]
        public void load_dictionary_one_word_loaded()
        {
            var path = TestContext.CurrentContext.TestDirectory + "/Resources/one_word_dictionary.txt";

            var wordsRepository = new WordsRepository(path);

            var words = wordsRepository.Load();

            words.Should().HaveCount(1).And.Contain("hello");
        }

        [Test]
        public void load_empty_dictionary_returns_empty_set()
        {
            var path = TestContext.CurrentContext.TestDirectory + "/Resources/empty_dictionary.txt";

            var wordsRepository = new WordsRepository(path);

            var words = wordsRepository.Load();

            words.Should().HaveCount(0);
        }

        [Test]
        public void load_dictionary_less_3_letter_words_filtered()
        {
            var path = TestContext.CurrentContext.TestDirectory + "/Resources/2_characte_words_dictionary.txt";

            var wordsRepository = new WordsRepository(path);

            var words = wordsRepository.Load();

            words.Should().HaveCount(0);
        }

        [Test]
        public void load_dictionary_same_words_removed()
        {
            var path = TestContext.CurrentContext.TestDirectory + "/Resources/same_words_dictionary.txt";

            var wordsRepository = new WordsRepository(path);

            var words = wordsRepository.Load();

            words.Should().HaveCount(1).And.Contain("word");
        }

        [Test]
        public void happy_path()
        {
            var path = TestContext.CurrentContext.TestDirectory + "/Resources/happy_dictionary.txt";
            var expected = new HashSet<string>() {"word","wordsecond","anotherword"};

            var wordsRepository = new WordsRepository(path);

            var words = wordsRepository.Load();

            words.ShouldAllBeEquivalentTo(expected);
        }
    }
}
