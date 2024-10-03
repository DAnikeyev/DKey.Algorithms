using DKey.Algorithms.NumberTheory;

namespace DKey.Algorithms.Tests.NumberTheory;

[TestFixture]
public class ArithmeticTests
{
    [Test]
    public void GCD_ReturnsExpectedValue()
    {
        Assert.AreEqual(12, IntArithmetics.GCD(144, 60));
    }
}