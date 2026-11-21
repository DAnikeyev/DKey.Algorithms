using DKey.Algorithms.DataStructures;

namespace DKey.Algorithms.Tests.DataStructures;

[TestFixture]
public class SortedArrayTests
{
    [Test]
    public void EmptyArray_CountGreaterThanAnyX_ReturnsZero()
    {
        var sortedArray = new SortedArray<int>(new int[] { });
        ClassicAssert.AreEqual(0, sortedArray.CountGreaterThan(42));
        ClassicAssert.AreEqual(0, sortedArray.CountGreaterThan(int.MinValue));
    }

    [Test]
    public void SingleElement_XBelow_ReturnsOne()
    {
        var sortedArray = new SortedArray<int>(new[] { 5 });
        ClassicAssert.AreEqual(1, sortedArray.CountGreaterThan(4));
    }

    [Test]
    public void SingleElement_XAbove_ReturnsZero()
    {
        var sortedArray = new SortedArray<int>(new[] { 5 });
        ClassicAssert.AreEqual(0, sortedArray.CountGreaterThan(6));
    }

    [Test]
    public void SingleElement_XEqual_ReturnsZero()
    {
        var sortedArray = new SortedArray<int>(new[] { 5 });
        ClassicAssert.AreEqual(0, sortedArray.CountGreaterThan(5));
    }

    [Test]
    public void Duplicates_AllCopiesExcludedFromCount()
    {
        var sortedArray = new SortedArray<int>(new[] { 1, 2, 2, 2, 3 });
        ClassicAssert.AreEqual(1, sortedArray.CountGreaterThan(2));
    }

    [Test]
    public void DuplicatesAtEnd_AllCopiesExcludedFromCount()
    {
        var sortedArray = new SortedArray<int>(new[] { 1, 2, 2 });
        ClassicAssert.AreEqual(0, sortedArray.CountGreaterThan(2));
    }

    [Test]
    public void XNotPresent_BetweenElements_CountsUpperHalf()
    {
        var sortedArray = new SortedArray<int>(new[] { 1, 3, 5 });
        ClassicAssert.AreEqual(2, sortedArray.CountGreaterThan(2));
    }

    [Test]
    public void XSmallerThanAllElements_ReturnsLength()
    {
        var sortedArray = new SortedArray<int>(new[] { 10, 20, 30 });
        ClassicAssert.AreEqual(3, sortedArray.CountGreaterThan(1));
    }

    [Test]
    public void XLargerThanAllElements_ReturnsZero()
    {
        var sortedArray = new SortedArray<int>(new[] { 10, 20, 30 });
        ClassicAssert.AreEqual(0, sortedArray.CountGreaterThan(100));
    }

    [Test]
    public void XEqualsMaxElement_ReturnsZero()
    {
        var sortedArray = new SortedArray<int>(new[] { 10, 20, 30 });
        ClassicAssert.AreEqual(0, sortedArray.CountGreaterThan(30));
    }

    [Test]
    public void UnsortedInput_IsSortedOnConstruction()
    {
        // CountGreaterThan only behaves as asserted when the internal array is sorted,
        // so these probes verify the constructor sorted a reversed input.
        var sortedArray = new SortedArray<int>(new[] { 9, 7, 5, 3, 1 });
        ClassicAssert.AreEqual(4, sortedArray.CountGreaterThan(2));
        ClassicAssert.AreEqual(1, sortedArray.CountGreaterThan(7));
        ClassicAssert.AreEqual(0, sortedArray.CountGreaterThan(9));
        ClassicAssert.AreEqual(5, sortedArray.CountGreaterThan(0));
    }

    [Test]
    public void Constructor_DoesNotMutateInputArray()
    {
        var input = new[] { 3, 1, 2 };
        var sortedArray = new SortedArray<int>(input);
        ClassicAssert.AreEqual(new[] { 3, 1, 2 }, input);
        ClassicAssert.AreEqual(1, sortedArray.CountGreaterThan(2));
    }

    [Test]
    public void LongValues_CountGreaterThan_WorksWithExtremeValues()
    {
        var sortedArray = new SortedArray<long>(new[] { long.MinValue, 0, long.MaxValue });
        ClassicAssert.AreEqual(2, sortedArray.CountGreaterThan(long.MinValue));
        ClassicAssert.AreEqual(1, sortedArray.CountGreaterThan(0L));
        ClassicAssert.AreEqual(0, sortedArray.CountGreaterThan(long.MaxValue));
    }

    [Test]
    public void StringValues_CountGreaterThan_FollowsComparisonOrder()
    {
        var sortedArray = new SortedArray<string>(new[] { "banana", "apple", "fig" });
        ClassicAssert.AreEqual(2, sortedArray.CountGreaterThan("apple"));
        ClassicAssert.AreEqual(1, sortedArray.CountGreaterThan("banana"));
        ClassicAssert.AreEqual(0, sortedArray.CountGreaterThan("fig"));
    }
}
