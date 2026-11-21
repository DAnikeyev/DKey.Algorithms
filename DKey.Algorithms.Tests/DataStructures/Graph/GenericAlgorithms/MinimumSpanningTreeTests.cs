using DKey.Algorithms.DataStructures.DisjointSet;
using DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.DataStructures.Graph.GenericAlgorithms;

[TestFixture]
public class MinimumSpanningTreeTests
{
    private static readonly List<(int U, int V, int Weight)> ClassicGraph = new()
    {
        (0, 1, 4), (0, 2, 3), (1, 2, 2), (1, 3, 5), (2, 3, 7),
    };

    [Test]
    public void KnownGraph_BothAlgorithms_AgreeWithHandComputedMST()
    {
        // MST: (1,2,2) + (0,2,3) + (1,3,5) = 10
        var kruskal = MinimumSpanningTree.Kruskal(4, ClassicGraph);
        var prim = MinimumSpanningTree.Prim(4, ClassicGraph);

        ClassicAssert.IsNotNull(kruskal);
        ClassicAssert.IsNotNull(prim);
        ClassicAssert.AreEqual(10L, kruskal!.Value.Weight);
        ClassicAssert.AreEqual(10L, prim!.Value.Weight);
        ClassicAssert.AreEqual(3, kruskal.Value.Edges.Count);
        ClassicAssert.AreEqual(3, prim.Value.Edges.Count);
    }

    [Test]
    public void ParallelEdges_CheapestIsChosen()
    {
        var edges = new List<(int, int, int)>
        {
            (0, 1, 10), (0, 1, 2), (1, 2, 1),
        };

        var kruskal = MinimumSpanningTree.Kruskal(3, edges);
        var prim = MinimumSpanningTree.Prim(3, edges);

        ClassicAssert.AreEqual(3L, kruskal!.Value.Weight);
        ClassicAssert.AreEqual(3L, prim!.Value.Weight);
    }

    [Test]
    public void NegativeWeights_Work()
    {
        var edges = new List<(int, int, int)> { (0, 1, -5), (1, 2, 3), (0, 2, -1) };

        var kruskal = MinimumSpanningTree.Kruskal(3, edges);
        var prim = MinimumSpanningTree.Prim(3, edges);

        ClassicAssert.AreEqual(-6L, kruskal!.Value.Weight);
        ClassicAssert.AreEqual(-6L, prim!.Value.Weight);
    }

    [Test]
    public void Disconnected_BothReturnNull()
    {
        var edges = new List<(int, int, int)> { (0, 1, 1), (2, 3, 1) };

        ClassicAssert.IsNull(MinimumSpanningTree.Kruskal(4, edges));
        ClassicAssert.IsNull(MinimumSpanningTree.Prim(4, edges));
    }

    [Test]
    public void EmptyAndSingleton_VertexSets()
    {
        var empty = MinimumSpanningTree.Kruskal(0, Array.Empty<(int, int, int)>());
        var single = MinimumSpanningTree.Kruskal(1, Array.Empty<(int, int, int)>());
        var primSingle = MinimumSpanningTree.Prim(1, Array.Empty<(int, int, int)>());

        ClassicAssert.AreEqual(0L, empty!.Value.Weight);
        ClassicAssert.AreEqual(0L, single!.Value.Weight);
        ClassicAssert.AreEqual(0L, primSingle!.Value.Weight);
        ClassicAssert.AreEqual(0, single.Value.Edges.Count);
    }

    [Test]
    public void ResultFormsSpanningTree()
    {
        var generator = ListGenerator.Instance(seed: 20261121);
        var edges = new List<(int, int, int)>();
        for (var u = 0; u < 20; u++)
        {
            for (var v = u + 1; v < 20; v++)
            {
                if (generator.RandomList(1, 0, 2)[0] < 2)
                {
                    edges.Add((u, v, generator.RandomList(1, -20, 50)[0]));
                }
            }
        }

        var result = MinimumSpanningTree.Kruskal(20, edges);

        ClassicAssert.IsNotNull(result);
        ClassicAssert.AreEqual(19, result!.Value.Edges.Count);
        var unionFind = new UnionFind(20);
        foreach (var edge in result.Value.Edges)
        {
            ClassicAssert.IsTrue(unionFind.Union(edge.From, edge.To), "tree edge closes a cycle");
        }

        ClassicAssert.AreEqual(1, unionFind.Count, "tree edges do not connect all vertices");
    }

    [Test]
    public void RandomGraphs_BothAlgorithmsMatchBruteForce()
    {
        var generator = ListGenerator.Instance(seed: 20261122);

        for (var attempt = 0; attempt < 30; attempt++)
        {
            var vertexCount = generator.RandomList(1, 2, 7)[0];
            var edges = new List<(int U, int V, int Weight)>();
            for (var i = 0; i < vertexCount; i++)
            {
                for (var j = i + 1; j < vertexCount; j++)
                {
                    if (generator.RandomList(1, 0, 2)[0] < 2)
                    {
                        edges.Add((i, j, generator.RandomList(1, -10, 40)[0]));
                    }
                }
            }

            var expected = BruteForceMst(vertexCount, edges);
            var kruskal = MinimumSpanningTree.Kruskal(vertexCount, edges);
            var prim = MinimumSpanningTree.Prim(vertexCount, edges);

            if (expected is null)
            {
                ClassicAssert.IsNull(kruskal, $"attempt {attempt}: brute force says disconnected");
                ClassicAssert.IsNull(prim, $"attempt {attempt}: brute force says disconnected");
            }
            else
            {
                ClassicAssert.IsNotNull(kruskal, $"attempt {attempt}: brute force found a tree");
                ClassicAssert.IsNotNull(prim, $"attempt {attempt}: brute force found a tree");
                ClassicAssert.AreEqual(expected.Value, kruskal!.Value.Weight, $"attempt {attempt}: kruskal vs brute force");
                ClassicAssert.AreEqual(expected.Value, prim!.Value.Weight, $"attempt {attempt}: prim vs brute force");
            }
        }
    }

    /// <summary>
    /// Reference MST: enumerate all (n-1)-subsets of edges, keep the cheapest
    /// one that connects everything. Only viable for small graphs.
    /// </summary>
    private static long? BruteForceMst(int vertexCount, List<(int U, int V, int Weight)> edges)
    {
        if (vertexCount <= 1)
        {
            return 0;
        }

        long? best = null;
        var chosen = new int[vertexCount - 1];
        void Choose(int start, int depth)
        {
            if (depth == chosen.Length)
            {
                var unionFind = new UnionFind(vertexCount);
                long total = 0;
                foreach (var index in chosen)
                {
                    unionFind.Union(edges[index].U, edges[index].V);
                    total += edges[index].Weight;
                }

                if (unionFind.Count == 1 && (best is null || total < best.Value))
                {
                    best = total;
                }

                return;
            }

            for (var i = start; i < edges.Count; i++)
            {
                chosen[depth] = i;
                Choose(i + 1, depth + 1);
            }
        }

        Choose(0, 0);
        return best;
    }
}
