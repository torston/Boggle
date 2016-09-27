using System.IO;
using NUnit.Framework;
using FluentAssertions;

namespace Boggle.Tests
{
    [TestFixture]
    public class DictionaryLoadTests
    {
        [Test]
        public void Load_Dictionary_With_Not_Valid_Path()
        {
            var wordsRepository = new WordsRepository("");

            Assert.Throws<FileNotFoundException>(() => wordsRepository.Load());
        }

        [Test]
        public void Load_Dictionary_With_One_Word()
        {
            string path = TestContext.CurrentContext.TestDirectory + "/Resources/one_word_dictionary.txt";

            var wordsRepository = new WordsRepository(path);

            var words = wordsRepository.Load();

            words.Should().HaveCount(1).And.Contain("hello");
        }

        [Test]
        public void Load_Dictionary_With_No_Text()
        {
            string path = TestContext.CurrentContext.TestDirectory + "/Resources/empty_dictionary.txt";

            var wordsRepository = new WordsRepository(path);

            var words = wordsRepository.Load();

            words.Should().HaveCount(0);
        }

        [Test]
        public void Load_Dictionary_Check_Less_3_Character_Word_Removed()
        {
            string path = TestContext.CurrentContext.TestDirectory + "/Resources/2_characte_words_dictionary.txt";

            var wordsRepository = new WordsRepository(path);

            var words = wordsRepository.Load();

            words.Should().HaveCount(0);
        }

        [Test]
        public void Load_Dictionary_Check_Same_Words_Removed()
        {
            string path = TestContext.CurrentContext.TestDirectory + "/Resources/same_words_dictionary.txt";

            var wordsRepository = new WordsRepository(path);

            var words = wordsRepository.Load();

            words.Should().HaveCount(1).And.Contain("word");
        }

    }
}
