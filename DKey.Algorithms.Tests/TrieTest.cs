using DKey.Algorithms.DataStructures.Graph.PrefixTrie;

namespace DKey.Algorithms.Tests;

public class TrieTest
{
    [TestFixture]
    public class TrieTests
    {
        private Trie _trie;

        [SetUp]
        public void Setup()
        {
            _trie = new Trie();
        }

        [Test]
        public void T01_SearchPrefix_GivenNonexistentPrefix_ReturnsFalse()
        {
            
            var words = new List<string> { "hello", "world", "foo", "bar" };
            _trie.Build(words);
            var result = _trie.SearchPrefix("baz");
            Assert.That(result, Is.False);
        }

        [Test]
        public void T02_SearchPrefix_GivenExistingPrefix_ReturnsTrue()
        {
            var words = new List<string> { "hello", "world", "foo", "bar" };
            _trie.Build(words);
            var result = _trie.SearchPrefix("h");
            Assert.That(result, Is.True);
        }

        [Test]
        public void T03_SearchLongestPrefix_GivenInput_ReturnsLongestPrefix()
        {
            var words = new List<string> { "hello", "world", "foo", "bar" };
            _trie.Build(words);
            var result = _trie.SearchLongestPrefix("foobar");
            Assert.That(result, Is.EqualTo("foo"));
        }

        [Test]
        public void T04_SearchLongestPrefix_GivenInputWithoutPrefix_ReturnsNull()
        {
            var words = new List<string> { "hello", "world", "foo", "bar" };
            _trie.Build(words);
            var result = _trie.SearchLongestPrefix("baz");
            Assert.That(result, Is.Null);
        }

        [Test]
        public void T05_SearchLongestPrefix_GivenInputWithMultiplePrefixes_ReturnsLongestPrefix()
        {
            var words = new List<string> { "hello", "world", "foobar", "foo" };
            _trie.Build(words);
            var result = _trie.SearchLongestPrefix("foobaz");
            Assert.That(result, Is.EqualTo("foo"));
        }
    }
}