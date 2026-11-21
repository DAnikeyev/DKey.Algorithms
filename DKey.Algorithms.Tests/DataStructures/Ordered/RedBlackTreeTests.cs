using DKey.Algorithms.DataStructures.Ordered;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.DataStructures.Ordered;

[TestFixture]
public class RedBlackTreeTests
{
    [Test]
    public void EmptyTree_ReturnsExpectedCount()
    {
        var rbt = new RBTInt();
        ClassicAssert.AreEqual(0, rbt.Count);
    }

    [Test]
    public void OneElement_ReturnsExpectedCount()
    {
        var rbt = new RBTInt();
        var added = rbt.TryAdd(1);
        ClassicAssert.AreEqual(1, rbt.Count);
        ClassicAssert.IsTrue(added);
    }

    [Test]
    public void TryAdd_ExpectedKeys()
    {
        var rbt = new RBTInt();
        rbt.TryAdd(1);
        rbt.TryAdd(2);
        rbt.TryAdd(8);
        rbt.TryAdd(9);
        rbt.TryAdd(3);
        rbt.TryAdd(7);
        rbt.TryAdd(4);
        rbt.TryAdd(5);
        rbt.TryAdd(10);
        ClassicAssert.AreEqual(9, rbt.Count);
        ClassicAssert.AreEqual(rbt.Keys, new[] { 1, 2, 3, 4, 5, 7, 8, 9, 10 });
    }

    [Test]
    public void TryDelete_ExpectKeys()
    {
        var rbt = new RBTInt();
        rbt.TryAdd(1);
        rbt.TryAdd(2);
        rbt.TryAdd(8);
        rbt.TryAdd(7);
        rbt.TryAdd(9);
        rbt.TryAdd(3);
        rbt.TryAdd(7);
        rbt.TryAdd(4);
        rbt.TryAdd(7);
        rbt.TryAdd(5);
        rbt.TryAdd(10);
        rbt.TryDelete(8);
        rbt.TryDelete(1);
        rbt.TryDelete(10);
        ClassicAssert.AreEqual(6, rbt.Count);
        ClassicAssert.AreEqual(rbt.Keys, new[] { 2, 3, 4, 5, 7, 9 });
    }

    [Test]
    public void Search_ExpectedKeyAndIndex()
    {
        var rbt = new RBTInt();
        rbt.TryAdd(1);
        rbt.TryAdd(2);
        rbt.TryAdd(8);
        rbt.TryAdd(9);
        rbt.TryAdd(3);
        rbt.TryAdd(7);
        rbt.TryAdd(4);
        rbt.TryAdd(5);
        rbt.TryAdd(10);
        var (index, item) = rbt.Search(8, out var found);
        ClassicAssert.AreEqual(8, item);
        ClassicAssert.IsTrue(found);
        ClassicAssert.AreEqual(6, index);
    }

    [Test]
    public void SearchNotAddedElement_ElementIsNotFound()
    {
        var rbt = new RBTInt();
        rbt.TryAdd(1);
        rbt.TryAdd(2);
        rbt.TryAdd(8);
        rbt.TryAdd(9);
        rbt.TryAdd(3);
        rbt.TryAdd(8);
        rbt.TryAdd(7);
        rbt.TryAdd(4);
        rbt.TryAdd(8);
        rbt.TryAdd(5);
        rbt.TryAdd(10);
        rbt.Search(11, out var found);
        ClassicAssert.IsFalse(found);
    }

    [Test]
    public void Search_EmptyTree_ElementIsNotFound()
    {
        var rbt = new RBTInt();
        rbt.Search(11, out var found);
        ClassicAssert.IsFalse(found);
    }

    [Test]
    public void SearchWithDeletion_ElementNoLongerFound()
    {
        var rbt = new RBTInt();
        rbt.TryAdd(1);
        rbt.TryAdd(2);
        rbt.TryAdd(8);
        rbt.TryAdd(9);
        rbt.TryAdd(3);
        rbt.TryAdd(7);
        rbt.TryAdd(4);
        rbt.TryAdd(5);
        rbt.TryAdd(10);
        var (index1, item1) = rbt.Search(8, out var found1);
        ClassicAssert.IsTrue(found1);
        ClassicAssert.AreEqual(6, index1);
        ClassicAssert.AreEqual(8, item1);
        rbt.TryDelete(8);
        rbt.TryDelete(1);
        rbt.TryDelete(10);
        rbt.Search(8, out var found2);
        ClassicAssert.IsFalse(found2);

        var (index3, item3) = rbt.Search(4, out var found3);
        ClassicAssert.IsTrue(found3);
        ClassicAssert.AreEqual(2, index3);
        ClassicAssert.AreEqual(4, item3);
    }

    [Test]
    public void LoadTest_NoExceptionThrown()
    {
        var rbt = new RBTInt();
        for (var i = 0; i < 100000; i++)
        {
            rbt.TryAdd(i);
        }
        ClassicAssert.AreEqual(100000, rbt.Count);
        for (var i = 0; i < 100000; i++)
        {
            rbt.TryDelete(i);
        }
        ClassicAssert.AreEqual(0, rbt.Count);
    }

    [Test]
    public void RandomAddedTest_NoExceptionThrown()
    {
        var randomData = ListGenerator.Instance(42).RandomList(10000, 1, 10000);
        var rbt = new RBTInt();
        foreach (var item in randomData)
        {
            rbt.TryAdd(item);
        }

        ListGenerator.Instance().Shuffle(randomData);
        Assert.That(rbt.Count, Is.Not.EqualTo(0));
        foreach (var item in randomData)
        {
            ClassicAssert.IsTrue(rbt.Contains(item));
        }
        foreach (var item in randomData)
        {
            rbt.TryDelete(item);
        }
        ClassicAssert.AreEqual(0, rbt.Count);
    }
}
