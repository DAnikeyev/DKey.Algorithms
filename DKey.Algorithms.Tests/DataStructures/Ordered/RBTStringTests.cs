using DKey.Algorithms.DataStructures.Ordered;

namespace DKey.Algorithms.Tests.DataStructures.Ordered;

[TestFixture]
public class RBTStringTests
{
    [Test]
    public void EmptyTree_ReturnsExpectedCount()
    {
        var rbt = new RBTString();
        ClassicAssert.AreEqual(0, rbt.Count);
    }

    [Test]
    public void OneElement_ReturnsExpectedCount()
    {
        var rbt = new RBTString();
        var added = rbt.TryAdd("apple");
        ClassicAssert.AreEqual(1, rbt.Count);
        ClassicAssert.IsTrue(added);
    }

    [Test]
    public void TryAdd_ExpectedKeys_InLexicalOrder()
    {
        var rbt = new RBTString();
        rbt.TryAdd("cherry");
        rbt.TryAdd("apple");
        rbt.TryAdd("elderberry");
        rbt.TryAdd("banana");
        rbt.TryAdd("fig");
        rbt.TryAdd("date");
        ClassicAssert.AreEqual(6, rbt.Count);
        ClassicAssert.AreEqual(
            rbt.Keys,
            new[] { "apple", "banana", "cherry", "date", "elderberry", "fig" });
    }

    [Test]
    public void TryAdd_DuplicateKey_IsRejected()
    {
        var rbt = new RBTString();
        rbt.TryAdd("apple");
        var secondAdd = rbt.TryAdd("apple");
        ClassicAssert.IsFalse(secondAdd);
        ClassicAssert.AreEqual(1, rbt.Count);
    }

    [Test]
    public void TryDelete_ExpectKeys()
    {
        var rbt = new RBTString();
        rbt.TryAdd("cherry");
        rbt.TryAdd("apple");
        rbt.TryAdd("elderberry");
        rbt.TryAdd("banana");
        rbt.TryAdd("fig");
        rbt.TryDelete("apple");
        rbt.TryDelete("fig");
        ClassicAssert.AreEqual(3, rbt.Count);
        ClassicAssert.AreEqual(rbt.Keys, new[] { "banana", "cherry", "elderberry" });
    }

    [Test]
    public void Search_ExpectedKeyAndIndex()
    {
        var rbt = new RBTString();
        rbt.TryAdd("cherry");
        rbt.TryAdd("apple");
        rbt.TryAdd("elderberry");
        rbt.TryAdd("banana");
        rbt.TryAdd("fig");
        var (index, item) = rbt.Search("elderberry", out var found);
        ClassicAssert.AreEqual("elderberry", item);
        ClassicAssert.IsTrue(found);
        ClassicAssert.AreEqual(3, index);
    }

    [Test]
    public void SearchNotAddedElement_ElementIsNotFound()
    {
        var rbt = new RBTString();
        rbt.TryAdd("apple");
        rbt.TryAdd("banana");
        rbt.Search("watermelon", out var found);
        ClassicAssert.IsFalse(found);
    }

    [Test]
    public void Search_EmptyTree_ElementIsNotFound()
    {
        var rbt = new RBTString();
        rbt.Search("apple", out var found);
        ClassicAssert.IsFalse(found);
    }

    [Test]
    public void SearchWithDeletion_ElementNoLongerFound()
    {
        var rbt = new RBTString();
        rbt.TryAdd("cherry");
        rbt.TryAdd("apple");
        rbt.TryAdd("elderberry");
        rbt.TryAdd("banana");
        rbt.TryDelete("cherry");
        rbt.Search("cherry", out var foundAfterDelete);
        ClassicAssert.IsFalse(foundAfterDelete);

        var (index, item) = rbt.Search("banana", out var found);
        ClassicAssert.IsTrue(found);
        ClassicAssert.AreEqual(1, index);
        ClassicAssert.AreEqual("banana", item);
    }

    [Test]
    public void NumericStrings_OrderLexicallyNotNumerically()
    {
        var rbt = new RBTString();
        rbt.TryAdd("9");
        rbt.TryAdd("10");
        rbt.TryAdd("100");
        // "10" < "9" lexically: '1' sorts before '9'.
        ClassicAssert.AreEqual(rbt.Keys, new[] { "10", "100", "9" });
    }

    [Test]
    public void Contains_AllAddedElements_FoundAndDeleted_NotFound()
    {
        var rbt = new RBTString();
        rbt.TryAdd("apple");
        rbt.TryAdd("banana");
        ClassicAssert.IsTrue(rbt.Contains("banana"));
        rbt.TryDelete("banana");
        ClassicAssert.IsFalse(rbt.Contains("banana"));
        ClassicAssert.IsTrue(rbt.Contains("apple"));
    }

    [Test]
    public void LoadTest_NoExceptionThrown()
    {
        var rbt = new RBTString();
        for (var i = 0; i < 10000; i++)
        {
            rbt.TryAdd($"key-{i:D8}");
        }
        ClassicAssert.AreEqual(10000, rbt.Count);
        for (var i = 0; i < 10000; i++)
        {
            rbt.TryDelete($"key-{i:D8}");
        }
        ClassicAssert.AreEqual(0, rbt.Count);
    }
}
