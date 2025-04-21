using DKey.Algorithms.ArgumentSearch;

namespace DKey.Algorithms.Tests.Search;

[TestFixture]
public class GoldenSectionSearchTests
{
    private const double Tolerance = 1e-6;

    [Test]
    public void QuadraticFunction_FoundMinimumWithPrecision()
    {
        // Quadratic function with minimum at x = 3.0
        double QuadraticFunction(double x) => (x - 3.0) * (x - 3.0);

        var left = 0.0;
        var right = 10.0;

        var result = TernarySearch.GoldenSection(left, right, QuadraticFunction);

        Assert.AreEqual(3.0, result, Tolerance);
    }

    [Test]
    public void AnotherFunction_FoundMinimumWithPrecision()
    {
        // Function with minimum at x = 2.0
        double CubicFunction(double x) => (x - 2.0) * (x - 2.0) * (x - 2.0) * (x - 2.0);

        var left = 0.0;
        var right = 5.0;

        var result = TernarySearch.GoldenSection(left, right, CubicFunction);

        Assert.AreEqual(2.0, result, Tolerance);
    }
}