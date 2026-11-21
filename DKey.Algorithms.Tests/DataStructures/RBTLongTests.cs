using DKey.Algorithms.DataStructures.Ordered;

namespace DKey.Algorithms.Tests.DataStructures;

[TestFixture]
public class RBTLongTests
{
    [Test]
    public void EmptyTree_ReturnsExpectedCount()
    {
        var rbt = new RBTLong();
        ClassicAssert.AreEqual(0, rbt.Count);
    }

    [Test]
    public void OneElement_ReturnsExpectedCount()
    {
        var rbt = new RBTLong();
        var added = rbt.TryAdd(1L);
        ClassicAssert.AreEqual(1, rbt.Count);
        ClassicAssert.IsTrue(added);
    }

    [Test]
    public void TryAdd_ExpectedKeys_WithExtremeValues()
    {
        var rbt = new RBTLong();
        rbt.TryAdd(long.MaxValue);
        rbt.TryAdd(2L);
        rbt.TryAdd(long.MinValue);
        rbt.TryAdd(9L);
        rbt.TryAdd(3L);
        rbt.TryAdd(7L);
        rbt.TryAdd(4L);
        rbt.TryAdd(5L);
        rbt.TryAdd(10L);
        ClassicAssert.AreEqual(9, rbt.Count);
        ClassicAssert.AreEqual(
            rbt.Keys,
            new long[] { long.MinValue, 2, 3, 4, 5, 7, 9, 10, long.MaxValue });
    }

    [Test]
    public void TryAdd_DuplicateKey_IsRejected()
    {
        var rbt = new RBTLong();
        rbt.TryAdd(7L);
        var secondAdd = rbt.TryAdd(7L);
        ClassicAssert.IsFalse(secondAdd);
        ClassicAssert.AreEqual(1, rbt.Count);
    }

    [Test]
    public void TryDelete_ExpectKeys()
    {
        var rbt = new RBTLong();
        rbt.TryAdd(1L);
        rbt.TryAdd(2L);
        rbt.TryAdd(8L);
        rbt.TryAdd(7L);
        rbt.TryAdd(9L);
        rbt.TryAdd(3L);
        rbt.TryAdd(4L);
        rbt.TryAdd(5L);
        rbt.TryAdd(10L);
        rbt.TryDelete(8L);
        rbt.TryDelete(1L);
        rbt.TryDelete(10L);
        ClassicAssert.AreEqual(6, rbt.Count);
        ClassicAssert.AreEqual(rbt.Keys, new long[] { 2, 3, 4, 5, 7, 9 });
    }

    [Test]
    public void Search_ExpectedKeyAndIndex()
    {
        var rbt = new RBTLong();
        rbt.TryAdd(1L);
        rbt.TryAdd(2L);
        rbt.TryAdd(8L);
        rbt.TryAdd(9L);
        rbt.TryAdd(3L);
        rbt.TryAdd(7L);
        rbt.TryAdd(4L);
        rbt.TryAdd(5L);
        rbt.TryAdd(10L);
        var (index, item) = rbt.Search(8L, out var found);
        ClassicAssert.AreEqual(8L, item);
        ClassicAssert.IsTrue(found);
        ClassicAssert.AreEqual(6, index);
    }

    [Test]
    public void SearchNotAddedElement_ElementIsNotFound()
    {
        var rbt = new RBTLong();
        rbt.TryAdd(1L);
        rbt.TryAdd(2L);
        rbt.TryAdd(3L);
        rbt.Search(11L, out var found);
        ClassicAssert.IsFalse(found);
    }

    [Test]
    public void Search_EmptyTree_ElementIsNotFound()
    {
        var rbt = new RBTLong();
        rbt.Search(11L, out var found);
        ClassicAssert.IsFalse(found);
    }

    [Test]
    public void SearchWithDeletion_ElementNoLongerFound()
    {
        var rbt = new RBTLong();
        rbt.TryAdd(1L);
        rbt.TryAdd(2L);
        rbt.TryAdd(8L);
        rbt.TryAdd(9L);
        rbt.TryAdd(3L);
        rbt.TryAdd(7L);
        rbt.TryAdd(4L);
        rbt.TryAdd(5L);
        rbt.TryAdd(10L);
        var (index1, item1) = rbt.Search(8L, out var found1);
        ClassicAssert.IsTrue(found1);
        ClassicAssert.AreEqual(6, index1);
        ClassicAssert.AreEqual(8L, item1);
        rbt.TryDelete(8L);
        rbt.TryDelete(1L);
        rbt.TryDelete(10L);
        rbt.Search(8L, out var found2);
        ClassicAssert.IsFalse(found2);

        var (index3, item3) = rbt.Search(4L, out var found3);
        ClassicAssert.IsTrue(found3);
        ClassicAssert.AreEqual(2, index3);
        ClassicAssert.AreEqual(4L, item3);
    }

    [Test]
    public void LoadTest_NoExceptionThrown()
    {
        var rbt = new RBTLong();
        for (var i = 0L; i < 100000L; i++)
        {
            rbt.TryAdd(i);
        }
        ClassicAssert.AreEqual(100000, rbt.Count);
        for (var i = 0L; i < 100000L; i++)
        {
            rbt.TryDelete(i);
        }
        ClassicAssert.AreEqual(0, rbt.Count);
    }

    [Test]
    public void Contains_AllAddedElements_FoundAndDeleted_NotFound()
    {
        var rbt = new RBTLong();
        rbt.TryAdd(5L);
        rbt.TryAdd(15L);
        rbt.TryAdd(25L);
        ClassicAssert.IsTrue(rbt.Contains(15L));
        rbt.TryDelete(15L);
        ClassicAssert.IsFalse(rbt.Contains(15L));
        ClassicAssert.IsTrue(rbt.Contains(5L));
        ClassicAssert.IsTrue(rbt.Contains(25L));
    }
}
