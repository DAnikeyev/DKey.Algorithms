using DKey.Algorithms.DataStructures.DisjointSet;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.DataStructures.DisjointSet;

[TestFixture]
public class UnionFindTests
{
    [Test]
    public void Singletons_StartDisconnected()
    {
        var uf = new UnionFind(5);

        ClassicAssert.AreEqual(5, uf.Count);
        for (var i = 0; i < 5; i++)
        {
            ClassicAssert.AreEqual(i, uf.Find(i), $"element {i} must be its own root");
            ClassicAssert.IsTrue(uf.AreConnected(i, i));
        }
    }

    [Test]
    public void ChainedUnions_FormOneComponent()
    {
        var uf = new UnionFind(6);
        var components = 6;

        for (var i = 0; i + 1 < 6; i++)
        {
            ClassicAssert.IsTrue(uf.Union(i, i + 1));
            ClassicAssert.AreEqual(--components, uf.Count);
        }

        for (var i = 0; i < 6; i++)
        {
            ClassicAssert.IsTrue(uf.AreConnected(0, i));
        }
    }

    [Test]
    public void Union_OfSameSet_ReturnsFalseAndKeepsCount()
    {
        var uf = new UnionFind(4);
        uf.Union(0, 1);
        uf.Union(1, 2);

        ClassicAssert.IsFalse(uf.Union(0, 2));
        ClassicAssert.IsFalse(uf.Union(1, 1));
        ClassicAssert.AreEqual(2, uf.Count);
    }

    [Test]
    public void TransitiveConnectivity_AfterArbitraryMerges()
    {
        var uf = new UnionFind(10);
        uf.Union(0, 3);
        uf.Union(3, 7);
        uf.Union(1, 9);

        ClassicAssert.IsTrue(uf.AreConnected(0, 7));
        ClassicAssert.IsTrue(uf.AreConnected(3, 7));
        ClassicAssert.IsFalse(uf.AreConnected(0, 1));
        ClassicAssert.IsFalse(uf.AreConnected(7, 9));
        ClassicAssert.AreEqual(7, uf.Count);
    }

    [Test]
    public void Find_CompressesPath_AllPointAtRoot()
    {
        var uf = new UnionFind(1000);
        for (var i = 0; i + 1 < 1000; i++)
        {
            uf.Union(i, i + 1);
        }

        var root = uf.Find(0);
        for (var i = 0; i < 1000; i++)
        {
            ClassicAssert.AreEqual(root, uf.Find(i));
        }
    }

    [Test]
    public void MillionRandomOps_MatchReferenceCounting()
    {
        const int size = 100_000;
        const int operations = 1_000_000;
        var uf = new UnionFind(size);
        var generator = ListGenerator.Instance(seed: 20261121);

        long merges = 0;
        for (var op = 0; op < operations; op++)
        {
            var x = generator.RandomList(1, 0, size - 1)[0];
            var y = generator.RandomList(1, 0, size - 1)[0];
            if (uf.Union(x, y))
            {
                merges++;
            }
        }

        ClassicAssert.AreEqual(size - merges, uf.Count);
        ClassicAssert.IsTrue(merges > 0);
    }
}
