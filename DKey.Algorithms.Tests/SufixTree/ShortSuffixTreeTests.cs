﻿using DKey.Algorithms.DataStructures.Graph.ShortSuffixTree;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.SufixTree;

[TestFixture]
public class ShortSuffixTreeTests
{
    [Test]
    public void Contains_IntTree_PositiveContainCheck0()
    {
        var data = new List<int> { 1, 2 };
        var tree = ShortSuffixTree.Build(data);

        Assert.IsTrue(tree.Contains(new List<int> { 2 }));
    }

    [Test]
    public void Contains_IntTree_PositiveContainCheck()
    {
        var data = new List<int> { 1, 2, 3, 1, 2, 3 };
        var tree = ShortSuffixTree.Build(data);

        Assert.IsTrue(tree.Contains(new List<int> { 1, 2, 3 }));
        Assert.IsTrue(tree.Contains(new List<int> { 2, 3, 1 }));
        Assert.IsTrue(tree.Contains(new List<int> { 3, 1, 2 }));
        Assert.IsTrue(tree.Contains(new List<int> { 1, 2 }));
    }

    [Test]
    public void Contains_IntTree_NegativeContainCheck()
    {
        var data = new List<int> { 1, 2, 3, 1, 2, 3 };
        var tree = ShortSuffixTree.Build(data);

        Assert.IsFalse(tree.Contains(new List<int> { 1, 2, 4 }));
        Assert.IsFalse(tree.Contains(new List<int> { 2, 3, 3 }));
        Assert.IsFalse(tree.Contains(new List<int> { 3, 1, 1 }));
        Assert.IsFalse(tree.Contains(new List<int> { 4, 2 }));
    }


    [Test]
    public void Contains_IntTreeRepeat_PositiveContainCheck()
    {
        var data = new List<int> { 1, 2, 2, 3 };
        var tree = ShortSuffixTree.Build(data);

        Assert.IsFalse(tree.Contains(new List<int> { 1, 2, 4 }));
    }

    [Test]
    public void VariousSizeTree_PositiveContainCheck([Values(1, 5, 10, 25, 100, 1000, 10000, 100000)] int value)
    {
        var data = ListGenerator.Instance(42).RandomList(value, 1, 5);
        var tree = ShortSuffixTree.Build(data);
        var ok = tree.Contains(new List<int> { 2, 4 });
        Assert.IsTrue(ok || value < 9999);
    }

    [Test]
    [Explicit]
    //64GBRAM for 250_000_000, 32GBRAM for 100_000_000
    public void BigBigTree_PositiveContainCheck([Values(1_000_000, 10_000_000, 50_000_000, 100_000_000, 250_000_000)] int value)
    {
        var data = ListGenerator.Instance(42).RandomList(value, 1, 6);
        var tree = ShortSuffixTree.Build(data);
        var ok = tree.Contains(new List<int> { 1, 2, 4 });
        Assert.IsTrue(ok || value < 9999);
    }

    [Test]
    public void SameStrings_LongestCommonSubstring_ReturnsFullString()
    {
        var s1 = "awsbwjevbasajdvabevbaweebviwbrviberb";
        var s2 = "awsbwjevbasajdvabevbaweebviwbrviberb";
        var tree = ShortSuffixTree.Build(s1.Select(x => (int)(x - 'a' + 1)).ToList());
        var lcs = tree.LongestCommonSubstring(s2.Select(x => (int)(x - 'a' + 1)));
        Assert.AreEqual((0, 0, s1.Length), lcs);
    }

    [Test]
    public void LongestCommonSubstring_ExpectedSubstring0()
    {
        var s1 = "aaaaaaaaaaaaaaaaaaaa";
        var s2 = "bababaaaaaaaaaaa";
        var tree = ShortSuffixTree.Build(s1.Select(x => (int)(x - 'a' + 1)).ToList());
        var lcs = tree.LongestCommonSubstring(s2.Select(x => (int)(x - 'a' + 1)));
        Assert.AreEqual((9, 5, s2.Length - 5), lcs);
    }

    [Test]
    public void LongestCommonSubstring_ExpectedSubstring1()
    {
        var s1 = "owocowocowocowowcwowowowb";
        var s2 = "owowowowowowo";
        var tree = ShortSuffixTree.Build(s1.Select(x => (int)(x - 'a' + 1)).ToList());
        var lcs = tree.LongestCommonSubstring(s2.Select(x => (int)(x - 'a' + 1)));
        Assert.AreEqual(s1.Substring(lcs.srcOffset, lcs.length), s2.Substring(lcs.docOffset, lcs.length));
        Assert.AreEqual("wowowow", s1.Substring(lcs.srcOffset, lcs.length));
    }

    [Test]
    public void LongestCommonSubstring_ExpectedSubstring2()
    {
        var s1 = ListGenerator.Instance(42).RandomString(10000, 5);
        var s2 = ListGenerator.Instance(42).RandomString(10000, 5);
        var tree = ShortSuffixTree.Build(s1.Select(x => (int)(x - 'a' + 1)).ToList());
        var lcs = tree.LongestCommonSubstring(s2.Select(x => (int)(x - 'a' + 1)));
        Assert.IsTrue(lcs.length > 4);
    }
}