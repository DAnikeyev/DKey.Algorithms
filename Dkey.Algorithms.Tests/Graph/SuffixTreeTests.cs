using DKey.Algorithms.DataStructures.Graph.SuffixTree;

namespace Dkey.Algorithms.Tests.Graph;

    public class SuffixTreeTests
    {
        
        [Test]
        public void Contains_IntTree_PositiveTests0()
        {
            var data = new List<int> { 0,1 };
            var tree = SuffixTree<int>.Build(data, int.MinValue);

            Assert.IsTrue(tree.Contains(new List<int> { 1 }));
        }

        
        [Test]
        public void Contains_CharTree_PositiveTests()
        {
            var data = new List<char> { 'a', 'b', 'c', 'a', 'b', 'c' };
            var tree = SuffixTree<char>.Build(data, '\0');

            Assert.IsTrue(tree.Contains(new List<char> { 'a', 'b', 'c' }));
            Assert.IsTrue(tree.Contains(new List<char> { 'b', 'c', 'a' }));
            Assert.IsTrue(tree.Contains(new List<char> { 'c', 'a', 'b' }));
            Assert.IsTrue(tree.Contains(new List<char> { 'a', 'b' }));
        }

        [Test]
        public void Contains_CharTree_NegativeTests()
        {
            var data = new List<char> { 'a', 'b', 'c', 'a', 'b', 'c' };
            var tree = SuffixTree<char>.Build(data, '\0');

            Assert.IsFalse(tree.Contains(new List<char> { 'a', 'b', 'd' }));
            Assert.IsFalse(tree.Contains(new List<char> { 'b', 'c', 'c' }));
            Assert.IsFalse(tree.Contains(new List<char> { 'c', 'a', 'a' }));
            Assert.IsFalse(tree.Contains(new List<char> { 'd', 'b' }));
        }

        [Test]
        public void Contains_IntTree_PositiveTests()
        {
            var data = new List<int> { 1, 2, 3, 1, 2, 3 };
            var tree = SuffixTree<int>.Build(data, int.MinValue);

            Assert.IsTrue(tree.Contains(new List<int> { 1, 2, 3 }));
            Assert.IsTrue(tree.Contains(new List<int> { 2, 3, 1 }));
            Assert.IsTrue(tree.Contains(new List<int> { 3, 1, 2 }));
            Assert.IsTrue(tree.Contains(new List<int> { 1, 2 }));
        }

        [Test]
        public void Contains_IntTree_NegativeTests()
        {
            var data = new List<int> { 1, 2, 3, 1, 2, 3 };
            var tree = SuffixTree<int>.Build(data, int.MinValue);

            Assert.IsFalse(tree.Contains(new List<int> { 1, 2, 4 }));
            Assert.IsFalse(tree.Contains(new List<int> { 2, 3, 3 }));
            Assert.IsFalse(tree.Contains(new List<int> { 3, 1, 1 }));
            Assert.IsFalse(tree.Contains(new List<int> { 4, 2 }));
        }
    }