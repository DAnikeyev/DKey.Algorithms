using DKey.Algorithms.NumberTheory;

namespace Dkey.Algorithms.Tests;

public class ArithmeticTests
{
    [Test]
    public void T01_GCD()
    {
        Assert.AreEqual(12, IntArithmetics.GCD(144, 60));
    }
}