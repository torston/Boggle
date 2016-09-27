using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Boggle.Tests
{
    [TestFixture]
    internal class TrieTests
    {
        //[Test]
        public void Create_Trie_From_Null()
        {
            var trieHelper = new TrieHelper();

            Assert.Throws<ArgumentException>(() => trieHelper.MakeTrie(null));
        }

        //[Test]
        public void Create_Trie_For_One_Letter_Word()
        {
            var trieHelper = new TrieHelper();

            var words = new HashSet<string> { "w" };

            var trie = trieHelper.MakeTrie(words);

            Assert.AreEqual(trie, new TrieNode
            {
                Children =
                {
                    {
                        'w', new TrieNode
                        {
                            IsWord = true
                        }
                    }
                }
            });
        }

        //[Test]
        public void Create_Trie_For_One_Word()
        {
            var trieHelper = new TrieHelper();

            var words = new HashSet<string> { "abc" };

            var trie = trieHelper.MakeTrie(words);

            var rootNode = new TrieNode();
            var node2 = new TrieNode(rootNode);
            var node3 = new TrieNode(node2);
            var node4 = new TrieNode(node3);

            rootNode.Children.Add('a', node2);
            node2.Children.Add('b', node3);
            node3.Children.Add('c', node4);

            node3.IsWord = true;

            trie.Should().Be(rootNode);
        }

        //[Test]
        public void Create_Trie_For_Three_Words()
        {
            var trieHelper = new TrieHelper();

            var words = new HashSet<string> { "a", "b", "c" };

            var trie = trieHelper.MakeTrie(words);

            var rootNode = new TrieNode();
            var node2 = new TrieNode(rootNode);
            var node3 = new TrieNode(rootNode);
            var node4 = new TrieNode(rootNode);

            rootNode.Children.Add('a', node2);
            rootNode.Children.Add('b', node3);
            rootNode.Children.Add('c', node4);

            node2.IsWord = true;
            node3.IsWord = true;
            node4.IsWord = true;

            trie.Should().BeSameAs(rootNode);
        }

        //[Test]
        public void Create_Trie_For_Same_Words()
        {
            var trieHelper = new TrieHelper();

            var words = new HashSet<string> { "abc", "abc", "abc" };

            var trie = trieHelper.MakeTrie(words);

            var rootNode = new TrieNode();
            var node2 = new TrieNode(rootNode);
            var node3 = new TrieNode(node2);
            var node4 = new TrieNode(node3);

            rootNode.Children.Add('a', node2);
            node2.Children.Add('b', node3);
            node3.Children.Add('c', node4);

            node3.IsWord = true;

            trie.Should().BeSameAs(rootNode);
        }

        //[Test]
        public void Create_Trie_For_Different_Words()
        {
            var trieHelper = new TrieHelper();

            var words = new HashSet<string> { "abc", "def" };

            var trie = trieHelper.MakeTrie(words);

            var rootNode = new TrieNode();

            var node2 = new TrieNode(rootNode);
            var node3 = new TrieNode(node2);
            var node4 = new TrieNode(node3);

            var node5 = new TrieNode(rootNode);
            var node6 = new TrieNode(node5);
            var node7 = new TrieNode(node6);

            rootNode.Children.Add('a', node2);
            rootNode.Children.Add('d', node5);

            node2.Children.Add('b', node3);
            node3.Children.Add('c', node4);

            node6.Children.Add('b', node6);
            node7.Children.Add('c', node6);

            node4.IsWord = true;
            node7.IsWord = true;

            trie.Should().BeSameAs(rootNode);
        }

        //[Test]
        public void Create_Trie_For_Same_Prefix_Words()
        {
            var trieHelper = new TrieHelper();

            var words = new HashSet<string> { "ab", "abc" };

            var trie = trieHelper.MakeTrie(words);

            var rootNode = new TrieNode();

            var node2 = new TrieNode(rootNode);
            var node3 = new TrieNode(node2);
            var node4 = new TrieNode(node3);

            rootNode.Children.Add('a', node2);

            node2.Children.Add('b', node3);
            node3.Children.Add('c', node4);

            node2.IsWord = true;
            node3.IsWord = true;

            trie.Should().BeSameAs(rootNode);
        }


    }
}
