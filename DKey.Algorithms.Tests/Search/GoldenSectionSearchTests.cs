using DKey.Algorithms.ArgumentSearch;

namespace DKey.Algorithms.Tests.Search;

public class GoldenSectionSearchTests
{
    private const double Tolerance = 1e-6;

    [Test]
    public void T01_QuadraticFunction()
    {
        // Quadratic function with minimum at x = 3.0
        double QuadraticFunction(double x) => (x - 3.0) * (x - 3.0);

        double left = 0.0;
        double right = 10.0;

        double result = TernarySearch.GoldenSection(left, right, QuadraticFunction);

        Assert.AreEqual(3.0, result, Tolerance);
    }

    [Test]
    public void T02_AnotherFunction()
    {
        // Function with minimum at x = 2.0
        double CubicFunction(double x) => (x - 2.0) * (x - 2.0) * (x - 2.0) * (x - 2.0);

        double left = 0.0;
        double right = 5.0;

        double result = TernarySearch.GoldenSection(left, right, CubicFunction);

        Assert.AreEqual(2.0, result, Tolerance);
    }
}