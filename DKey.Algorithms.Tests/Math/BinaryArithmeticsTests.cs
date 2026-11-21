using DKey.Algorithms.NumberTheory;

namespace DKey.Algorithms.Tests.Math;

[TestFixture]
public class BinaryArithmeticsTests
{
    [Test]
    public void CountBits_ReturnsExpectedValue()
    {
        ClassicAssert.AreEqual(3, BinaryArithmetics.CountPositiveBits(11));
    }

    [Test]
    public void ConvertFromInt_ReturnsExpectedValue()
    {
        var array = BinaryArithmetics.ConvertToBoolArray(11);
        ClassicAssert.IsTrue(array[0] && array[1] && array[3]);
        ClassicAssert.AreEqual(3, array.Count(x => x));
        ClassicAssert.AreEqual(11, BinaryArithmetics.ConvertToInt(array));
    }

    [Test]
    public void ConvertFromInt_ReturnExpectedValue()
    {
        var res = BinaryArithmetics.ConvertToBinaryReversedTrimmedList(11);
        ClassicAssert.AreEqual(res, new List<int> { 1, 0, 1, 1 });
    }

    [Test]
    public void ConvertToInt_ReturnExpectedValue()
    {
        var res = new List<int> { 1, 0, 1, 1 };

        ClassicAssert.AreEqual(11, BinaryArithmetics.ConvertToInt(res, true));
        ClassicAssert.AreEqual(11, BinaryArithmetics.ConvertToInt(res.Select(x => x == 1).ToArray(), true));
    }

    [Test]
    public void ConvertToBoolArray_ReturnExpectedValue()
    {
        var res = BinaryArithmetics.ConvertToBoolArray(11);

        ClassicAssert.AreEqual(11, BinaryArithmetics.ConvertToInt(res));
        ClassicAssert.AreEqual(11, BinaryArithmetics.ConvertToInt(res.Select(x => x ? 1 : 0).ToArray()));
    }
}
