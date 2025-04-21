using DKey.Algorithms.ArgumentSearch;

namespace DKey.Algorithms.Tests.Search;

[TestFixture]
public class TernarySearchTests
{
    [Test]
    public void QuadraticFunction_FoundMinimumWithPrecision()
    {
        // Quadratic function with minimum at x = 3
        int QuadraticFunction(int x) => (x - 3) * (x - 3);

        var left = 0;
        var right = 10;

        var result = TernarySearch.GetIndex(left, right, QuadraticFunction);

        Assert.AreEqual(3, result);
    }

    [Test]
    public void ReverseLinearFunction_FoundMinimumWithPrecision()
    {
        // Reverse linear function with minimum at x = 9
        long ReverseLinearFunction(int x) => 10 - x;

        var left = 1;
        var right = 9;

        var result = TernarySearch.GetIndexLong(left, right, ReverseLinearFunction);

        Assert.AreEqual(9, result);
    }
    
    [Test]
    public void FlatLine_FoundMinimumWithPrecision()
    {
        // Reverse linear function with minimum at x = 9
        int ReverseLinearFunction(int x) => 10;

        var left = 1;
        var right = 9;

        var result = TernarySearch.GetIndex(left, right, ReverseLinearFunction);

        Assert.AreEqual(1, result);
    }
}