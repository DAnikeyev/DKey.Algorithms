using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.DataStructures.Graph.GenericAlgorithms;

[TestFixture]
public class TopologicalSortTests
{
    private static List<int>[] Build(params (int from, int to)[] edges)
    {
        return GraphBuilder.Directed(edges.ToList(), edges.Select(e => e.from).Concat(edges.Select(e => e.to)).DefaultIfEmpty(0).Max() + 1).G;
    }

    private static bool IsValidOrder(List<int>[] graph, int[] order)
    {
        if (order.Distinct().Count() != graph.Length)
        {
            return false;
        }

        var position = new int[graph.Length];
        for (var i = 0; i < order.Length; i++)
        {
            position[order[i]] = i;
        }

        for (var u = 0; u < graph.Length; u++)
        {
            foreach (var v in graph[u])
            {
                if (position[u] >= position[v])
                {
                    return false;
                }
            }
        }

        return true;
    }

    [Test]
    public void ChainDAG_ReturnsUniqueOrder()
    {
        var graph = Build((0, 1), (1, 2), (2, 3));

        var order = TopologicalSort.Get(graph);

        ClassicAssert.IsNotNull(order);
        ClassicAssert.IsTrue(new[] { 0, 1, 2, 3 }.SequenceEqual(order!));
    }

    [Test]
    public void DAG_EveryEdgePointsForward()
    {
        var graph = Build((5, 2), (5, 0), (4, 0), (4, 1), (2, 3), (3, 1));

        var order = TopologicalSort.Get(graph);

        ClassicAssert.IsNotNull(order);
        ClassicAssert.IsTrue(IsValidOrder(graph, order!), $"order [{string.Join(",", order!)}] violates an edge");
    }

    [Test]
    public void CyclicGraph_ReturnsNull()
    {
        var graph = Build((0, 1), (1, 2), (2, 0));

        ClassicAssert.IsNull(TopologicalSort.Get(graph));
    }

    [Test]
    public void SelfLoop_ReturnsNull()
    {
        var graph = Build((0, 0));

        ClassicAssert.IsNull(TopologicalSort.Get(graph));
    }

    [Test]
    public void SingleNode_ReturnsThatNode()
    {
        var graph = new List<int>[] { new() };

        var order = TopologicalSort.Get(graph);

        ClassicAssert.IsNotNull(order);
        ClassicAssert.IsTrue(new[] { 0 }.SequenceEqual(order!));
    }

    [Test]
    public void EmptyGraph_ReturnsEmptyOrder()
    {
        var order = TopologicalSort.Get(Array.Empty<List<int>>());

        ClassicAssert.IsNotNull(order);
        ClassicAssert.AreEqual(0, order!.Length);
    }

    [Test]
    public void DisconnectedDAG_ReturnsValidInterleavedOrder()
    {
        var graph = Build((0, 1), (2, 3), (4, 5));

        var order = TopologicalSort.Get(graph);

        ClassicAssert.IsNotNull(order);
        ClassicAssert.IsTrue(IsValidOrder(graph, order!));
    }

    [Test]
    public void RandomDAGs_AllProduceValidOrders()
    {
        var generator = ListGenerator.Instance(seed: 20261121);

        for (var attempt = 0; attempt < 25; attempt++)
        {
            var size = generator.RandomList(1, 2, 30)[0];
            // Build a random DAG: random order + forward-only edges — always acyclic.
            var permutation = generator.RandomPermutation(size);
            var position = new int[size];
            for (var i = 0; i < size; i++)
            {
                position[permutation[i]] = i;
            }

            var edges = new List<(int, int)>();
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    if (position[i] < position[j] && generator.RandomList(1, 0, 4)[0] == 0)
                    {
                        edges.Add((i, j));
                    }
                }
            }

            var graph = Build(edges.ToArray());
            var order = TopologicalSort.Get(graph);
            ClassicAssert.IsNotNull(order, $"attempt {attempt}: DAG must be sortable");
            ClassicAssert.IsTrue(IsValidOrder(graph, order!), $"attempt {attempt}: invalid order");
        }
    }
}
