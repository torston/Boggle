using System.IO;
using NUnit.Framework;

namespace Boggle.Tests
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void Create_Solver_Not_Valid_Dictionary_Path()
        {
            Assert.Throws<FileNotFoundException>(() => MyBoggleSolution.CreateSolver(""));
        }

        [Test]
        public void TrieTest()
        {
            
        }
    }
}
