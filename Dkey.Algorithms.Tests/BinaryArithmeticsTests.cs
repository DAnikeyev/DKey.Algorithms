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
    public void T02_ConvertFromInt()
    {
        var array = BinaryArithmetics.ConvertToBoolArray(11);
        Assert.IsTrue(array[0] && array[1] && array[3]);
        Assert.AreEqual(3, array.Count(x => x));
        Assert.AreEqual(11, BinaryArithmetics.ConvertToInt(array));
    }
    
    [Test]
    public void T03_ConvertFromInt()
    {
        var res = BinaryArithmetics.ConvertToBinaryReversedTrimmedList(11);
        CollectionAssert.AreEqual(res, new List<int>{1,0,1,1});
    }
    
    [Test]
    public void T04_ConvertToInt()
    {
        var res = new List<int> {1, 0, 1, 1};
        
        Assert.AreEqual(11, BinaryArithmetics.ConvertToInt(res, true));
        Assert.AreEqual(11, BinaryArithmetics.ConvertToInt(res.Select(x => x == 1).ToArray(), true));
    }
    
    [Test]
    public void T05_ConvertToInt()
    {
        var res = BinaryArithmetics.ConvertToBoolArray(11);
        
        Assert.AreEqual(11, BinaryArithmetics.ConvertToInt(res));
        Assert.AreEqual(11, BinaryArithmetics.ConvertToInt(res.Select(x => x ? 1 : 0).ToArray()));
    }
}