namespace DKey.Algorithms.Tests;

[TestFixture]
public class TupleHelperTests
{
    [Test]
    public void Swap_ExchangesItems()
    {
        var result = TupleHelpler.Swap((1, 2));

        Assert.AreEqual(2, result.First);
        Assert.AreEqual(1, result.Second);
    }

    [Test]
    public void Swap_EqualItems_ReturnsEqualTuple()
    {
        var result = TupleHelpler.Swap((5, 5));

        Assert.AreEqual(5, result.First);
        Assert.AreEqual(5, result.Second);
    }

    [Test]
    public void Swap_Twice_ReturnsOriginalTuple()
    {
        var original = (First: "left", Second: "right");

        var result = TupleHelpler.Swap(TupleHelpler.Swap(original));

        Assert.AreEqual(original, result);
    }

    [Test]
    public void SelectMax_FirstGreater_ReturnsOriginalOrder()
    {
        var result = TupleHelpler.SelectMax((9, 2));

        Assert.AreEqual(9, result.First);
        Assert.AreEqual(2, result.Second);
    }

    [Test]
    public void SelectMax_SecondGreater_ReturnsSwappedOrder()
    {
        var result = TupleHelpler.SelectMax((2, 9));

        Assert.AreEqual(9, result.First);
        Assert.AreEqual(2, result.Second);
    }

    [Test]
    public void SelectMax_EqualItems_ReturnsEqualTuple()
    {
        var result = TupleHelpler.SelectMax((4, 4));

        Assert.AreEqual(4, result.First);
        Assert.AreEqual(4, result.Second);
    }

    [Test]
    public void SelectMax_WorksForStrings()
    {
        var result = TupleHelpler.SelectMax(("apple", "banana"));

        Assert.AreEqual("banana", result.First);
        Assert.AreEqual("apple", result.Second);
    }
}
