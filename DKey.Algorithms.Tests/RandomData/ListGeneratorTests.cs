using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.RandomData;

[TestFixture]
public class ListGeneratorTests
{
    [Test]
    public void Instance_CalledWithoutSeed_ReturnsCachedInstance()
    {
        var first = ListGenerator.Instance(123);
        var second = ListGenerator.Instance();
        var third = ListGenerator.Instance();

        Assert.That(first, Is.SameAs(second));
        Assert.That(second, Is.SameAs(third));
    }

    [Test]
    public void Instance_CalledWithSeed_ReplacesInstance()
    {
        var original = ListGenerator.Instance(321);
        var reseeded = ListGenerator.Instance(654);

        Assert.That(original, Is.Not.SameAs(reseeded));
    }

    [Test]
    public void RandomList_ReturnsRequestedLength_WithinInclusiveBounds()
    {
        var generator = ListGenerator.Instance(42);

        var list = generator.RandomList(500, -3, 7);

        ClassicAssert.AreEqual(500, list.Count);
        ClassicAssert.IsTrue(list.All(x => x >= -3 && x <= 7));
    }

    [Test]
    public void RandomList_LongOverload_ReturnsRequestedLength_WithinBounds()
    {
        var generator = ListGenerator.Instance(42);

        var list = generator.RandomList(500, -5L, 5L);

        ClassicAssert.AreEqual(500, list.Count);
        ClassicAssert.IsInstanceOf<List<long>>(list);
        ClassicAssert.IsTrue(list.All(x => x >= -5L && x <= 5L));
    }

    [Test]
    public void RandomList_SameSeed_ProducesIdenticalSequences()
    {
        var first = ListGenerator.Instance(99).RandomList(100, 0, 1000);
        var second = ListGenerator.Instance(99).RandomList(100, 0, 1000);

        ClassicAssert.AreEqual(first, second);
    }

    [Test]
    public void RandomPermutation_ReturnsAllIndicesExactlyOnce()
    {
        var generator = ListGenerator.Instance(7);

        var permutation = generator.RandomPermutation(100);

        ClassicAssert.AreEqual(100, permutation.Count);
        Assert.That(permutation, Is.EquivalentTo(Enumerable.Range(0, 100)));
    }

    [Test]
    public void Shuffle_PreservesElements()
    {
        var generator = ListGenerator.Instance(11);
        var data = Enumerable.Range(0, 100).ToList();

        generator.Shuffle(data);

        ClassicAssert.AreEqual(100, data.Count);
        Assert.That(data, Is.EquivalentTo(Enumerable.Range(0, 100)));
    }

    [Test]
    public void RandomString_HasRequestedLength_WithLettersFromRange()
    {
        var generator = ListGenerator.Instance(5);

        var value = generator.RandomString(200, 3);

        ClassicAssert.AreEqual(200, value.Length);
        ClassicAssert.IsTrue(value.All(c => c >= 'a' && c <= 'c'));
    }

    [Test]
    public void RandomKElements_ContainsExactlyKOnes()
    {
        var generator = ListGenerator.Instance(13);

        var sequence = generator.RandomKElements(100, 30);

        ClassicAssert.AreEqual(100, sequence.Count);
        ClassicAssert.AreEqual(30, sequence.Sum());
    }
}
