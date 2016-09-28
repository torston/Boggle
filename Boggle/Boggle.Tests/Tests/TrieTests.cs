using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Boggle.Tests
{
    [TestFixture]
    internal class TrieTests
    {
        [Test]
        public void create_trie_from_null_exception()
        {
            var trieHelper = new TrieHelper();

            trieHelper
                .Invoking(r => r.MakeTrie(null))
                .ShouldThrow<ArgumentException>()
                .WithMessage("Words hashset cannot be null");
        }

        [Test]
        public void create_tied_one_letter_one_child_node()
        {
            var trieHelper = new TrieHelper();
            var words = new HashSet<string> { "w" };

            var actual = trieHelper.MakeTrie(words);
            var expected = new TrieNode();
            var node = new TrieNode(expected);

            expected.Children.Add('w', node);

            node.IsWord = true;

            actual.ShouldBeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
        }

        [Test]
        public void crete_trie_from_word_three_nested_nodes()
        {
            var trieHelper = new TrieHelper();

            var words = new HashSet<string> { "abc" };

            var actual = trieHelper.MakeTrie(words);

            var expected = new TrieNode();
            var node = new TrieNode(expected);
            var node2 = new TrieNode(node);
            var node3 = new TrieNode(node2);

            expected.Children.Add('a', node);
            node.Children.Add('b', node2);
            node2.Children.Add('c', node3);

            node3.IsWord = true;

            actual.ShouldBeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
        }

        [Test]
        public void create_trie_three_words_three_children()
        {
            var trieHelper = new TrieHelper();

            var words = new HashSet<string> { "a", "b", "c" };

            var actual = trieHelper.MakeTrie(words);

            var expected = new TrieNode();
            var node2 = new TrieNode(expected);
            var node3 = new TrieNode(expected);
            var node4 = new TrieNode(expected);

            expected.Children.Add('a', node2);
            expected.Children.Add('b', node3);
            expected.Children.Add('c', node4);

            node2.IsWord = true;
            node3.IsWord = true;
            node4.IsWord = true;

            actual.ShouldBeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
        }

        [Test]
        public void create_trie_two_words_three_nested_children()
        {
            var trieHelper = new TrieHelper();

            var words = new HashSet<string> { "abc", "abc" };

            var actual = trieHelper.MakeTrie(words);

            var expected = new TrieNode();
            var node2 = new TrieNode(expected);
            var node3 = new TrieNode(node2);
            var node4 = new TrieNode(node3);

            expected.Children.Add('a', node2);
            node2.Children.Add('b', node3);
            node3.Children.Add('c', node4);

            node4.IsWord = true;

            actual.ShouldBeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
        }

        [Test]
        public void create_trie_two_words_three_nested_children_per_word()
        {
            var trieHelper = new TrieHelper();

            var words = new HashSet<string> { "abc", "def" };

            var actual = trieHelper.MakeTrie(words);

            var expected = new TrieNode();

            var node2 = new TrieNode(expected);
            var node3 = new TrieNode(node2);
            var node4 = new TrieNode(node3);

            var node5 = new TrieNode(expected);
            var node6 = new TrieNode(node5);
            var node7 = new TrieNode(node6);

            expected.Children.Add('a', node2);
            expected.Children.Add('d', node5);

            node2.Children.Add('b', node3);
            node3.Children.Add('c', node4);

            node5.Children.Add('e', node6);
            node6.Children.Add('f', node7);

            node4.IsWord = true;
            node7.IsWord = true;

            actual.ShouldBeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
        }

        [Test]
        public void create_tree_same_prefix_words_nested()
        {
            var trieHelper = new TrieHelper();

            var words = new HashSet<string> { "ab", "abc" };

            var actual = trieHelper.MakeTrie(words);

            var expected = new TrieNode();

            var node2 = new TrieNode(expected);
            var node3 = new TrieNode(node2);
            var node4 = new TrieNode(node3);

            expected.Children.Add('a', node2);

            node2.Children.Add('b', node3);
            node3.Children.Add('c', node4);

            node3.IsWord = true;
            node4.IsWord = true;

            actual.ShouldBeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
        }

        [Test]
        public void happy_path()
        {
            var trieHelper = new TrieHelper();

            var words = new HashSet<string> { "ab", "abc", "d" };

            var actual = trieHelper.MakeTrie(words);

            var expected = new TrieNode();

            var node2 = new TrieNode(expected);
            var node3 = new TrieNode(node2);
            var node4 = new TrieNode(node3);
            var node5 = new TrieNode(expected);

            expected.Children.Add('a', node2);
            expected.Children.Add('d', node5);

            node2.Children.Add('b', node3);
            node3.Children.Add('c', node4);

            node3.IsWord = true;
            node4.IsWord = true;
            node5.IsWord = true;

            actual.ShouldBeEquivalentTo(expected, o => o.IgnoringCyclicReferences());
        }


    }
}
