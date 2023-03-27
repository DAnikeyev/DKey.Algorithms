using DKey.Algorithms.NumberTheory;

namespace DKey.Algorithms.Tests;

public class BinaryArithmeticsTests
{
    [Test]
    public void T01_CNT()
    {
        Assert.AreEqual(3, BinaryArithmetics.CountPositiveBits(11));
    }
    
    [Test]
    public void T02_Convert()
    {
        var array = BinaryArithmetics.ConvertToBoolArray(11);
        Assert.IsTrue(array[0] && array[1] && array[3]);
        Assert.AreEqual(3, array.Count(x => x));
        Assert.AreEqual(11, BinaryArithmetics.ConvertToInt(array));
    }
}