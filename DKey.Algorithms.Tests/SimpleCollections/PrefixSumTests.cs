using DKey.Algorithms.DataStructures.IntervalTree;

namespace DKey.Algorithms.Tests.SimpleCollections;

[TestFixture]
public class PrefixSumTests
{
    [Test]
    public void PrefixSum_SingleTestCase_CorrectResult()
    {
        IList<int> data = new List<int> { 1, 2, 3, 4, 5 };
        long[] expected = { 0, 1, 3, 6, 10, 15 };

        var result = DataSum.PrefixSum(data);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void SuffixSum_SingleTestCase_ExpectedResult()
    {
        IList<int> data = new List<int> { 1, 2, 3, 4, 5 };
        long[] expected = { 15, 14, 12, 9, 5, 0 };

        var result = DataSum.SuffixSum(data);

        Assert.AreEqual(expected, result);
    }
    
    [TestCase(new int[] { }, new long[] { 0 })]
    [TestCase(new[] { 1 }, new long[] { 0, 1 })]
    [TestCase(new[] { 1, 2 }, new long[] { 0, 1, 3 })]
    [TestCase(new[] { 1, 2, 3 }, new long[] { 0, 1, 3, 6 })]
    public void PrefixSum_MultipleTestCases_ExpectedResult(int[] data, long[] expected)
    {
        var result = DataSum.PrefixSum(data);

        Assert.AreEqual(expected, result);
    }

    [TestCase(new int[] { }, new long[] { 0 })]
    [TestCase(new[] { 1 }, new long[] { 1, 0 })]
    [TestCase(new[] { 1, 2 }, new long[] { 3, 2, 0 })]
    [TestCase(new[] { 1, 2, 3 }, new long[] { 6, 5, 3, 0 })]
    public void SuffixSum_MultipleTestCases_ExpectedResult(int[] data, long[] expected)
    {
        var result = DataSum.SuffixSum(data);

        Assert.AreEqual(expected, result);
    }
}