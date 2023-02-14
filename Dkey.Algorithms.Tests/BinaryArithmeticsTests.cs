using DKey.Algorithms.NumberTheory;

namespace Dkey.Algorithms.Tests;

public class BinaryArithmeticsTests
{
    [Test]
    public void T01_CNT()
    {
        Assert.AreEqual(3, BinaryArithmetics.CountPositiveBits(11));
    }
}