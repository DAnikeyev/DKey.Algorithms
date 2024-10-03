using DKey.Algorithms.NumberTheory;

namespace DKey.Algorithms.Tests.Math;

[TestFixture]
public class BinaryArithmeticsTests
{
    [Test]
    public void CountBits_ReturnsExpectedValue()
    {
        Assert.AreEqual(3, BinaryArithmetics.CountPositiveBits(11));
    }
    
    [Test]
    public void ConvertFromInt_ReturnsExpectedValue()
    {
        var array = BinaryArithmetics.ConvertToBoolArray(11);
        Assert.IsTrue(array[0] && array[1] && array[3]);
        Assert.AreEqual(3, array.Count(x => x));
        Assert.AreEqual(11, BinaryArithmetics.ConvertToInt(array));
    }
    
    [Test]
    public void ConvertFromInt_ReturnExpectedValue()
    {
        var res = BinaryArithmetics.ConvertToBinaryReversedTrimmedList(11);
        CollectionAssert.AreEqual(res, new List<int>{1,0,1,1});
    }
    
    [Test]
    public void ConvertToInt_ReturnExpectedValue()
    {
        var res = new List<int> {1, 0, 1, 1};
        
        Assert.AreEqual(11, BinaryArithmetics.ConvertToInt(res, true));
        Assert.AreEqual(11, BinaryArithmetics.ConvertToInt(res.Select(x => x == 1).ToArray(), true));
    }
    
    [Test]
    public void ConvertToBoolArray_ReturnExpectedValue()
    {
        var res = BinaryArithmetics.ConvertToBoolArray(11);
        
        Assert.AreEqual(11, BinaryArithmetics.ConvertToInt(res));
        Assert.AreEqual(11, BinaryArithmetics.ConvertToInt(res.Select(x => x ? 1 : 0).ToArray()));
    }
}