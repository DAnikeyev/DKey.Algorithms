using DKey.Algorithms.DataStructures;

namespace DKey.Algorithms.Tests.DataStructures;

[TestFixture]
public class SortedArrayTests
{
    [Test]
    public void EmptyArray_CountGreaterThanAnyX_ReturnsZero()
    {
        var sortedArray = new SortedArray<int>(new int[] { });
        Assert.AreEqual(0, sortedArray.CountGreaterThan(42));
        Assert.AreEqual(0, sortedArray.CountGreaterThan(int.MinValue));
    }

    [Test]
    public void SingleElement_XBelow_ReturnsOne()
    {
        var sortedArray = new SortedArray<int>(new[] { 5 });
        Assert.AreEqual(1, sortedArray.CountGreaterThan(4));
    }

    [Test]
    public void SingleElement_XAbove_ReturnsZero()
    {
        var sortedArray = new SortedArray<int>(new[] { 5 });
        Assert.AreEqual(0, sortedArray.CountGreaterThan(6));
    }

    [Test]
    public void SingleElement_XEqual_ReturnsZero()
    {
        var sortedArray = new SortedArray<int>(new[] { 5 });
        Assert.AreEqual(0, sortedArray.CountGreaterThan(5));
    }

    [Test]
    public void Duplicates_AllCopiesExcludedFromCount()
    {
        var sortedArray = new SortedArray<int>(new[] { 1, 2, 2, 2, 3 });
        Assert.AreEqual(1, sortedArray.CountGreaterThan(2));
    }

    [Test]
    public void DuplicatesAtEnd_AllCopiesExcludedFromCount()
    {
        var sortedArray = new SortedArray<int>(new[] { 1, 2, 2 });
        Assert.AreEqual(0, sortedArray.CountGreaterThan(2));
    }

    [Test]
    public void XNotPresent_BetweenElements_CountsUpperHalf()
    {
        var sortedArray = new SortedArray<int>(new[] { 1, 3, 5 });
        Assert.AreEqual(2, sortedArray.CountGreaterThan(2));
    }

    [Test]
    public void XSmallerThanAllElements_ReturnsLength()
    {
        var sortedArray = new SortedArray<int>(new[] { 10, 20, 30 });
        Assert.AreEqual(3, sortedArray.CountGreaterThan(1));
    }

    [Test]
    public void XLargerThanAllElements_ReturnsZero()
    {
        var sortedArray = new SortedArray<int>(new[] { 10, 20, 30 });
        Assert.AreEqual(0, sortedArray.CountGreaterThan(100));
    }

    [Test]
    public void XEqualsMaxElement_ReturnsZero()
    {
        var sortedArray = new SortedArray<int>(new[] { 10, 20, 30 });
        Assert.AreEqual(0, sortedArray.CountGreaterThan(30));
    }

    [Test]
    public void UnsortedInput_IsSortedOnConstruction()
    {
        // CountGreaterThan only behaves as asserted when the internal array is sorted,
        // so these probes verify the constructor sorted a reversed input.
        var sortedArray = new SortedArray<int>(new[] { 9, 7, 5, 3, 1 });
        Assert.AreEqual(4, sortedArray.CountGreaterThan(2));
        Assert.AreEqual(1, sortedArray.CountGreaterThan(7));
        Assert.AreEqual(0, sortedArray.CountGreaterThan(9));
        Assert.AreEqual(5, sortedArray.CountGreaterThan(0));
    }

    [Test]
    public void Constructor_DoesNotMutateInputArray()
    {
        var input = new[] { 3, 1, 2 };
        var sortedArray = new SortedArray<int>(input);
        CollectionAssert.AreEqual(new[] { 3, 1, 2 }, input);
        Assert.AreEqual(1, sortedArray.CountGreaterThan(2));
    }

    [Test]
    public void LongValues_CountGreaterThan_WorksWithExtremeValues()
    {
        var sortedArray = new SortedArray<long>(new[] { long.MinValue, 0, long.MaxValue });
        Assert.AreEqual(2, sortedArray.CountGreaterThan(long.MinValue));
        Assert.AreEqual(1, sortedArray.CountGreaterThan(0L));
        Assert.AreEqual(0, sortedArray.CountGreaterThan(long.MaxValue));
    }

    [Test]
    public void StringValues_CountGreaterThan_FollowsComparisonOrder()
    {
        var sortedArray = new SortedArray<string>(new[] { "banana", "apple", "fig" });
        Assert.AreEqual(2, sortedArray.CountGreaterThan("apple"));
        Assert.AreEqual(1, sortedArray.CountGreaterThan("banana"));
        Assert.AreEqual(0, sortedArray.CountGreaterThan("fig"));
    }
}
