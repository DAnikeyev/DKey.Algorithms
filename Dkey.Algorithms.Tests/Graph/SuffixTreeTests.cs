using DKey.Algorithms.DataStructures.Graph.SuffixTree;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.Graph;

    public class SuffixTreeTests
    {
        [Test]
        public void T01_Contains_IntTree_PositiveTests0()
        {
            var data = new List<int> { 0,1 };
            var tree = SuffixTree<int>.Build(data, int.MinValue);

            Assert.IsTrue(tree.Contains(new List<int> { 1 }));
        }

        
        [Test]
        public void T02_Contains_CharTree_PositiveTests()
        {
            var data = new List<char> { 'a', 'b', 'c', 'a', 'b', 'c' };
            var tree = SuffixTree<char>.Build(data, '\0');

            Assert.IsTrue(tree.Contains(new List<char> { 'a', 'b', 'c' }));
            Assert.IsTrue(tree.Contains(new List<char> { 'b', 'c', 'a' }));
            Assert.IsTrue(tree.Contains(new List<char> { 'c', 'a', 'b' }));
            Assert.IsTrue(tree.Contains(new List<char> { 'a', 'b' }));
        }

        [Test]
        public void T03_Contains_CharTree_NegativeTests()
        {
            var data = new List<char> { 'a', 'b', 'c', 'a', 'b', 'c' };
            var tree = SuffixTree<char>.Build(data, '\0');

            Assert.IsFalse(tree.Contains(new List<char> { 'a', 'b', 'd' }));
            Assert.IsFalse(tree.Contains(new List<char> { 'b', 'c', 'c' }));
            Assert.IsFalse(tree.Contains(new List<char> { 'c', 'a', 'a' }));
            Assert.IsFalse(tree.Contains(new List<char> { 'd', 'b' }));
        }

        [Test]
        public void T04_Contains_IntTree_PositiveTests()
        {
            var data = new List<int> { 1, 2, 3, 1, 2, 3 };
            var tree = SuffixTree<int>.Build(data, int.MinValue);

            Assert.IsTrue(tree.Contains(new List<int> { 1, 2, 3 }));
            Assert.IsTrue(tree.Contains(new List<int> { 2, 3, 1 }));
            Assert.IsTrue(tree.Contains(new List<int> { 3, 1, 2 }));
            Assert.IsTrue(tree.Contains(new List<int> { 1, 2 }));
        }

        [Test]
        public void T05_Contains_IntTree_NegativeTests()
        {
            var data = new List<int> { 1, 2, 3, 1, 2, 3 };
            var tree = SuffixTree<int>.Build(data, int.MinValue);

            Assert.IsFalse(tree.Contains(new List<int> { 1, 2, 4 }));
            Assert.IsFalse(tree.Contains(new List<int> { 2, 3, 3 }));
            Assert.IsFalse(tree.Contains(new List<int> { 3, 1, 1 }));
            Assert.IsFalse(tree.Contains(new List<int> { 4, 2 }));
        }
        
        
        [Test]
        public void T06_Contains_IntTreeRepeat_PositiveTests()
        {
            var data = new List<int> { 1,2,2,3 };
            var tree = SuffixTree<int>.Build(data, int.MinValue);

            Assert.IsFalse(tree.Contains(new List<int> { 1, 2, 4 }));
        }
        
        
        [Test]
        public void T07_Contains_CharTree_ComplexTest()
        {
            var data = new List<char> { 'b', 'c', 'c', 'c', 'b', 'a', 'a', 'b', 'c', 'd' };
            var tree = SuffixTree<char>.Build(data, '\0');

            Assert.IsTrue(tree.Contains(new List<char> { 'c', 'b', 'a', 'a' }));
            Assert.IsTrue(tree.Contains(new List<char> { 'a', 'a' }));
            Assert.IsTrue(tree.Contains(new List<char> { 'c', 'c', 'c' }));
            Assert.IsTrue(tree.Contains(new List<char> { 'c', 'c', 'c', 'b' }));
            Assert.IsTrue(tree.Contains(new List<char> { 'b', 'c', 'c', 'c', 'b', 'a', 'a', 'b', 'c', 'd' }));
            Assert.IsTrue(tree.Contains(new List<char> { 'c', 'c', 'c', 'b', 'a', 'a', 'b', 'c' }));
            
            Assert.IsFalse(tree.Contains(new List<char> { 'c', 'c', 'c', 'c' }));
            Assert.IsFalse(tree.Contains(new List<char> { 'd', 'd'}));
            Assert.IsFalse(tree.Contains(new List<char> { 'b', 'c', 'd', 'c'}));
            Assert.IsFalse(tree.Contains(new List<char> { 'c', 'c', 'c', 'b', 'a', 'a', 'b', 'c', 'c' }));
        }
        
        [Test]
        public void T08_VariousSizeTree([Values(1, 5, 10, 25, 100, 1000, 10000, 100000)]int value)
        {
            var data = ListGenerator.Instance().RandomString(value, 5);
            var tree = SuffixTree<char>.Build(data.ToCharArray(), char.MinValue);
            var ok = tree.Contains("ba".ToCharArray());
            Assert.IsTrue(ok || value < 9999);

        }

        [Test]
        [Explicit]
        //64GBRAM for 250_000_000, 32GBRAM for 100_000_000
        public void T09_BigBigTree([Values(1_000_000, 10_000_000, 50_000_000, 100_000_000, 250_000_000)] int value)
        {
            var data = ListGenerator.Instance(42).RandomString(value, 5);
            var tree = SuffixTree<char>.Build(data.ToCharArray(), char.MinValue);
            var ok = tree.Contains("bacd".ToCharArray());
            Assert.IsTrue(ok);
        }

        [Test]
        public void T10_LongestCommonSubstring()
        {
            var s1 = "awsbwjevbasajdvabevbaweebviwbrviberb";
            var s2 = "awsbwjevbasajdvabevbaweebviwbrviberb";
            var tree = SuffixTree<char>.Build(s1.ToCharArray(), char.MinValue);
            var lcs = tree.LongestCommonSubstring(s2);
            Assert.AreEqual((0,0,s1.Length), lcs);
        }
        
        [Test]
        public void T11_LongestCommonSubstring()
        {
            var s1 = "aaaaaaaaaaaaaaaaaaaa";
            var s2 = "bababaaaaaaaaaaa";
            var tree = SuffixTree<char>.Build(s1.ToCharArray(), char.MinValue);
            var lcs = tree.LongestCommonSubstring(s2);
            Assert.AreEqual((9,5,s2.Length - 5), lcs);
        }
        
        [Test]
        public void T12_LongestCommonSubstring()
        {
            var s1 = "owoAowoAowoAowowAwowowowb";
            var s2 = "owowowowowowo";
            var tree = SuffixTree<char>.Build(s1.ToCharArray(), char.MinValue);
            var lcs = tree.LongestCommonSubstring(s2);
            Assert.AreEqual(s1.Substring(lcs.srcOffset, lcs.length), s2.Substring(lcs.docOffset, lcs.length));
            Assert.AreEqual(s1.Substring(lcs.srcOffset, lcs.length), "wowowow");
        }
        
        [Test]
        public void T13_LongestCommonSubstring()
        {
            var s1 = ListGenerator.Instance(42).RandomString(10000, 5);
            var s2 = ListGenerator.Instance(42).RandomString(10000, 5);
            var tree = SuffixTree<char>.Build(s1.ToCharArray(), char.MinValue);
            var lcs = tree.LongestCommonSubstring(s2);
            Assert.IsTrue(lcs.length > 4);
        }
    }