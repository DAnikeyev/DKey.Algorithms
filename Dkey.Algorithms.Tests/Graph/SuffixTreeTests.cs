using DKey.Algorithms.DataStructures.Graph.SuffixTree;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.Graph;

    public class SuffixTreeTests
    {
        
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
        
        
        [Test]
        public void Contains_IntTreeRepeat_PositiveTests()
        {
            var data = new List<int> { 1,2,2,3 };
            var tree = SuffixTree<int>.Build(data, int.MinValue);

            Assert.IsFalse(tree.Contains(new List<int> { 1, 2, 4 }));
        }
        
        
        [Test]
        public void Contains_CharTree_PositiveComplexTest()
        {
            var data = new List<char> { 'b', 'c', 'c', 'c', 'b', 'a', 'a', 'b', 'c', 'd' };
            var tree = SuffixTree<char>.Build(data, '\0');

            Assert.IsTrue(tree.Contains(new List<char> { 'c', 'b', 'a', 'a' }));
        }
        
        [Test]
        public void BigTree([Values(1, 5, 10, 25, 100, 1000, 10000, 100000, 500000)]int value)
        {
            var data = ListGenerator.Instance().RandomString(value, 3);
            var tree = SuffixTree<char>.Build(data.ToCharArray(), char.MinValue);
            var ok = tree.Contains("ba".ToCharArray());
            Assert.IsTrue(ok || value < 9999);

        }

        [Test]
        [Explicit]
        //64GBRAM for 250_000_000, 32GBRAM for 100_000_000
        public void BigBigTree([Values(1_000_000, 10_000_000, 50_000_000, 100_000_000, 250_000_000)] int value)
        {
            var data = ListGenerator.Instance().RandomString(value, 5);
            var tree = SuffixTree<char>.Build(data.ToCharArray(), char.MinValue);
            var ok = tree.Contains("bacd".ToCharArray());
            Assert.IsTrue(ok);
        }
    }