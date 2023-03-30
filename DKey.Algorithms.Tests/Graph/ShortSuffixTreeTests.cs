using DKey.Algorithms.DataStructures.Graph.ShortSuffixTree;
using DKey.Algorithms.DataStructures.Graph.SuffixTree;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.Graph;

    public class ShortSuffixTreeTests
    {
        [Test]
        public void T01_Contains_IntTree_PositiveTests0()
        {
            var data = new List<int> { 1,2 };
            var tree = ShortSuffixTree.Build(data);

            Assert.IsTrue(tree.Contains(new List<int> { 2 }));
        }

        [Test]
        public void T02_Contains_IntTree_PositiveTests()
        {
            var data = new List<int> { 1, 2, 3, 1, 2, 3 };
            var tree = ShortSuffixTree.Build(data);

            Assert.IsTrue(tree.Contains(new List<int> { 1, 2, 3 }));
            Assert.IsTrue(tree.Contains(new List<int> { 2, 3, 1 }));
            Assert.IsTrue(tree.Contains(new List<int> { 3, 1, 2 }));
            Assert.IsTrue(tree.Contains(new List<int> { 1, 2 }));
        }

        [Test]
        public void T03_Contains_IntTree_NegativeTests()
        {
            var data = new List<int> { 1, 2, 3, 1, 2, 3 };
            var tree = ShortSuffixTree.Build(data);

            Assert.IsFalse(tree.Contains(new List<int> { 1, 2, 4 }));
            Assert.IsFalse(tree.Contains(new List<int> { 2, 3, 3 }));
            Assert.IsFalse(tree.Contains(new List<int> { 3, 1, 1 }));
            Assert.IsFalse(tree.Contains(new List<int> { 4, 2 }));
        }
        
        
        [Test]
        public void T04_Contains_IntTreeRepeat_PositiveTests()
        {
            var data = new List<int> { 1,2,2,3 };
            var tree = ShortSuffixTree.Build(data);

            Assert.IsFalse(tree.Contains(new List<int> { 1, 2, 4 }));
        }
        
        [Test]
        public void T05_VariousSizeTree([Values(1, 5, 10, 25, 100, 1000, 10000, 100000, 500000)]int value)
        {
            var data = ListGenerator.Instance().RandomList(value, 1, 5);
            var tree = ShortSuffixTree.Build(data);
            var ok = tree.Contains(new List<int> { 2, 4 });
            Assert.IsTrue(ok || value < 9999);
        }

        [Test]
        [Explicit]
        //64GBRAM for 250_000_000, 32GBRAM for 100_000_000
        public void T06_BigBigTree([Values(1_000_000, 10_000_000, 50_000_000, 100_000_000, 250_000_000)] int value)
        {
            var data = ListGenerator.Instance().RandomList(value, 1, 6);
            var tree = ShortSuffixTree.Build(data);
            var ok = tree.Contains(new List<int> { 1, 2, 4 });
            Assert.IsTrue(ok || value < 9999);
        }
    }