using DKey.Algorithms.DataStructures.Graph.SuffixTree;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.SufixTree;

[TestFixture]
public class SuffixTreeTests
{
    [Test]
    public void IntTree_PositiveContainCheck0()
    {
        var data = new List<int> { 0, 1 };
        var tree = SuffixTree<int>.Build(data, int.MinValue);

        Assert.IsTrue(tree.Contains(new List<int> { 1 }));
    }


    [Test]
    public void CharTree_PositiveContainCheck()
    {
        var data = new List<char> { 'a', 'b', 'c', 'a', 'b', 'c' };
        var tree = SuffixTree<char>.Build(data, '\0');

        Assert.IsTrue(tree.Contains(new List<char> { 'a', 'b', 'c' }));
        Assert.IsTrue(tree.Contains(new List<char> { 'b', 'c', 'a' }));
        Assert.IsTrue(tree.Contains(new List<char> { 'c', 'a', 'b' }));
        Assert.IsTrue(tree.Contains(new List<char> { 'a', 'b' }));
    }

    [Test]
    public void CharTree_NegativeContainCheck()
    {
        var data = new List<char> { 'a', 'b', 'c', 'a', 'b', 'c' };
        var tree = SuffixTree<char>.Build(data, '\0');

        Assert.IsFalse(tree.Contains(new List<char> { 'a', 'b', 'd' }));
        Assert.IsFalse(tree.Contains(new List<char> { 'b', 'c', 'c' }));
        Assert.IsFalse(tree.Contains(new List<char> { 'c', 'a', 'a' }));
        Assert.IsFalse(tree.Contains(new List<char> { 'd', 'b' }));
    }

    [Test]
    public void IntTree_PositiveContainCheck1()
    {
        var data = new List<int> { 1, 2, 3, 1, 2, 3 };
        var tree = SuffixTree<int>.Build(data, int.MinValue);

        Assert.IsTrue(tree.Contains(new List<int> { 1, 2, 3 }));
        Assert.IsTrue(tree.Contains(new List<int> { 2, 3, 1 }));
        Assert.IsTrue(tree.Contains(new List<int> { 3, 1, 2 }));
        Assert.IsTrue(tree.Contains(new List<int> { 1, 2 }));
    }

    [Test]
    public void IntTree_NegativeContainCheck()
    {
        var data = new List<int> { 1, 2, 3, 1, 2, 3 };
        var tree = SuffixTree<int>.Build(data, int.MinValue);

        Assert.IsFalse(tree.Contains(new List<int> { 1, 2, 4 }));
        Assert.IsFalse(tree.Contains(new List<int> { 2, 3, 3 }));
        Assert.IsFalse(tree.Contains(new List<int> { 3, 1, 1 }));
        Assert.IsFalse(tree.Contains(new List<int> { 4, 2 }));
    }


    [Test]
    public void IntTreeWithRepetition_PositiveContainCheck()
    {
        var data = new List<int> { 1, 2, 2, 3 };
        var tree = SuffixTree<int>.Build(data, int.MinValue);

        Assert.IsFalse(tree.Contains(new List<int> { 1, 2, 4 }));
    }


    [Test]
    public void CharTree_ComplexContainCheck_PassAllAsserts()
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
        Assert.IsFalse(tree.Contains(new List<char> { 'd', 'd' }));
        Assert.IsFalse(tree.Contains(new List<char> { 'b', 'c', 'd', 'c' }));
        Assert.IsFalse(tree.Contains(new List<char> { 'c', 'c', 'c', 'b', 'a', 'a', 'b', 'c', 'c' }));
    }

    [Test]
    public void VariousSizeTrees_Contains_ba([Values(1, 5, 10, 25, 100, 1000, 10000, 100000)] int value)
    {
        var data = ListGenerator.Instance(42).RandomString(value, 5);
        var tree = SuffixTree<char>.Build(data.ToCharArray(), char.MinValue);
        var ok = tree.Contains("ba".ToCharArray());
        Assert.IsTrue(ok || value < 9999);

    }

    [Test]
    [Explicit]
    //64GBRAM for 250_000_000, 32GBRAM for 100_000_000
    public void BigBigTree_ThrowNoException([Values(1_000_000, 10_000_000, 50_000_000, 100_000_000, 250_000_000)] int value)
    {
        var data = ListGenerator.Instance(42).RandomString(value, 5);
        var tree = SuffixTree<char>.Build(data.ToCharArray(), char.MinValue);
        var ok = tree.Contains("bacd".ToCharArray());
        Assert.IsTrue(ok);
    }

    [Test]
    public void SameStrings_LongestCommonSubstring_ReturnsFullString()
    {
        var s1 = "awsbwjevbasajdvabevbaweebviwbrviberb";
        var s2 = "awsbwjevbasajdvabevbaweebviwbrviberb";
        var tree = SuffixTree<char>.Build(s1.ToCharArray(), char.MinValue);
        var lcs = tree.LongestCommonSubstring(s2);
        Assert.AreEqual((0, 0, s1.Length), lcs);
    }

    [Test]
    public void LongestCommonSubstring_ExpectedSubstring0()
    {
        var s1 = "aaaaaaaaaaaaaaaaaaaa";
        var s2 = "bababaaaaaaaaaaa";
        var tree = SuffixTree<char>.Build(s1.ToCharArray(), char.MinValue);
        var lcs = tree.LongestCommonSubstring(s2);
        Assert.AreEqual((9, 5, s2.Length - 5), lcs);
    }

    [Test]
    public void LongestCommonSubstring_ExpectedSubstring1()
    {
        var s1 = "owoAowoAowoAowowAwowowowb";
        var s2 = "owowowowowowo";
        var tree = SuffixTree<char>.Build(s1.ToCharArray(), char.MinValue);
        var lcs = tree.LongestCommonSubstring(s2);
        Assert.AreEqual(s1.Substring(lcs.srcOffset, lcs.length), s2.Substring(lcs.docOffset, lcs.length));
        Assert.AreEqual("wowowow", s1.Substring(lcs.srcOffset, lcs.length));
    }

    [Test]
    public void LongestCommonSubstring_ExpectedSubstring2()
    {
        var s1 = ListGenerator.Instance(42).RandomString(10000, 5);
        var s2 = ListGenerator.Instance(42).RandomString(10000, 5);
        var tree = SuffixTree<char>.Build(s1.ToCharArray(), char.MinValue);
        var lcs = tree.LongestCommonSubstring(s2);
        Assert.IsTrue(lcs.length > 4);
    }
}