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
        public void T05_VariousSizeTree([Values(1, 5, 10, 25, 100, 1000, 10000, 100000)]int value)
        {
            var data = ListGenerator.Instance(42).RandomList(value, 1, 5);
            var tree = ShortSuffixTree.Build(data);
            var ok = tree.Contains(new List<int> { 2, 4 });
            Assert.IsTrue(ok || value < 9999);
        }

        [Test]
        [Explicit]
        //64GBRAM for 250_000_000, 32GBRAM for 100_000_000
        public void T06_BigBigTree([Values(1_000_000, 10_000_000, 50_000_000, 100_000_000, 250_000_000)] int value)
        {
            var data = ListGenerator.Instance(42).RandomList(value, 1, 6);
            var tree = ShortSuffixTree.Build(data);
            var ok = tree.Contains(new List<int> { 1, 2, 4 });
            Assert.IsTrue(ok || value < 9999);
        }
        
        [Test]
        public void T07_LongestCommonSubstring()
        {
            var s1 = "awsbwjevbasajdvabevbaweebviwbrviberb";
            var s2 = "awsbwjevbasajdvabevbaweebviwbrviberb";
            var tree = ShortSuffixTree.Build(s1.Select(x => (int)(x - 'a'+ 1)).ToList());
            var lcs = tree.LongestCommonSubstring(s2.Select(x => (int)(x - 'a'+ 1)));
            Assert.AreEqual((0,0,s1.Length), lcs);
        }
        
        [Test]
        public void T08_LongestCommonSubstring()
        {
            var s1 = "aaaaaaaaaaaaaaaaaaaa";
            var s2 = "bababaaaaaaaaaaa";
            var tree = ShortSuffixTree.Build(s1.Select(x => (int)(x - 'a'+ 1)).ToList());
            var lcs = tree.LongestCommonSubstring(s2.Select(x => (int)(x - 'a'+ 1)));
            Assert.AreEqual((9,5,s2.Length - 5), lcs);
        }
        
        [Test]
        public void T09_LongestCommonSubstring()
        {
            var s1 = "owocowocowocowowcwowowowb";
            var s2 = "owowowowowowo";
            var tree = ShortSuffixTree.Build(s1.Select(x => (int)(x - 'a'+ 1)).ToList());
            var lcs = tree.LongestCommonSubstring(s2.Select(x => (int)(x - 'a'+ 1)));
            Assert.AreEqual(s1.Substring(lcs.srcOffset, lcs.length), s2.Substring(lcs.docOffset, lcs.length));
            Assert.AreEqual(s1.Substring(lcs.srcOffset, lcs.length), "wowowow");
        }
        
        [Test]
        public void T10_LongestCommonSubstring()
        {
            var s1 = ListGenerator.Instance(42).RandomString(10000, 5);
            var s2 = ListGenerator.Instance(42).RandomString(10000, 5);
            var tree = ShortSuffixTree.Build(s1.Select(x => (int)(x - 'a'+ 1)).ToList());
            var lcs = tree.LongestCommonSubstring(s2.Select(x => (int)(x - 'a'+ 1)));
            Assert.IsTrue(lcs.length > 4);
        }
    }