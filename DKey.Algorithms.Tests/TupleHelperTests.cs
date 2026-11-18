namespace DKey.Algorithms.Tests;

[TestFixture]
public class TupleHelperTests
{
    [Test]
    public void Swap_ExchangesItems()
    {
        var result = TupleHelper.Swap((1, 2));

        Assert.AreEqual(2, result.First);
        Assert.AreEqual(1, result.Second);
    }

    [Test]
    public void Swap_EqualItems_ReturnsEqualTuple()
    {
        var result = TupleHelper.Swap((5, 5));

        Assert.AreEqual(5, result.First);
        Assert.AreEqual(5, result.Second);
    }

    [Test]
    public void Swap_Twice_ReturnsOriginalTuple()
    {
        var original = (First: "left", Second: "right");

        var result = TupleHelper.Swap(TupleHelper.Swap(original));

        Assert.AreEqual(original, result);
    }

    [Test]
    public void SelectMax_FirstGreater_ReturnsOriginalOrder()
    {
        var result = TupleHelper.SelectMax((9, 2));

        Assert.AreEqual(9, result.First);
        Assert.AreEqual(2, result.Second);
    }

    [Test]
    public void SelectMax_SecondGreater_ReturnsSwappedOrder()
    {
        var result = TupleHelper.SelectMax((2, 9));

        Assert.AreEqual(9, result.First);
        Assert.AreEqual(2, result.Second);
    }

    [Test]
    public void SelectMax_EqualItems_ReturnsEqualTuple()
    {
        var result = TupleHelper.SelectMax((4, 4));

        Assert.AreEqual(4, result.First);
        Assert.AreEqual(4, result.Second);
    }

    [Test]
    public void SelectMax_WorksForStrings()
    {
        var result = TupleHelper.SelectMax(("apple", "banana"));

        Assert.AreEqual("banana", result.First);
        Assert.AreEqual("apple", result.Second);
    }

    [Test]
    public void TupleHelpler_Bridge_Forwards_To_TupleHelper()
    {
        // The misspelled type is an [Obsolete] forwarding bridge: it must keep
        // compiling and behave identically until the future major removes it.
#pragma warning disable CS0618
        Assert.That(TupleHelpler.Swap((1, 2)), Is.EqualTo(TupleHelper.Swap((1, 2))));
        Assert.That(TupleHelpler.SelectMax((2, 9)), Is.EqualTo(TupleHelper.SelectMax((2, 9))));
#pragma warning restore CS0618
    }
}
