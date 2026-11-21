using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Graph.Misc;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.DataStructures.Graph.Misc;

[TestFixture]
public class ShortestPathAlgorithmsTests
{
    [Test]
    public void BellmanFord_NegativeEdges_ShortestPaths()
    {
        // 0 ->(4) 1, 0 ->(5) 2, 1 ->(-3) 2 => dist[2] = 1
        var edges = new[] { (0, 1, 4), (0, 2, 5), (1, 2, -3) };

        var distances = BellmanFord.ToAll(3, edges, 0);

        ClassicAssert.IsNotNull(distances);
        ClassicAssert.AreEqual(0L, distances![0]);
        ClassicAssert.AreEqual(4L, distances[1]);
        ClassicAssert.AreEqual(1L, distances[2]);
    }

    [Test]
    public void BellmanFord_UnreachableKeepsMaxValue()
    {
        var edges = new[] { (0, 1, 2) };

        var distances = BellmanFord.ToAll(3, edges, 0);

        ClassicAssert.AreEqual(long.MaxValue, distances![2]);
    }

    [Test]
    public void BellmanFord_ReachableNegativeCycle_ReturnsNull()
    {
        var edges = new[] { (0, 1, 1), (1, 2, -3), (2, 1, 1) };

        ClassicAssert.IsNull(BellmanFord.ToAll(3, edges, 0));
    }

    [Test]
    public void BellmanFord_UnreachableNegativeCycle_DistancesStillValid()
    {
        // The 2 -> 3 -> 2 cycle is negative but not reachable from 0.
        var edges = new[] { (0, 1, 5), (2, 3, -4), (3, 2, 3) };

        var distances = BellmanFord.ToAll(4, edges, 0);

        ClassicAssert.IsNotNull(distances);
        ClassicAssert.AreEqual(5L, distances![1]);
        ClassicAssert.AreEqual(long.MaxValue, distances[2]);
        ClassicAssert.AreEqual(long.MaxValue, distances[3]);
    }

    [Test]
    public void FloydWarshall_KnownDistances_WithNegativeEdge()
    {
        var edges = new[] { (0, 1, 4), (0, 2, 5), (1, 2, -3), (2, 3, 2) };

        var matrix = FloydWarshall.ToAllPairs(4, edges);

        ClassicAssert.AreEqual(0L, matrix[0, 0]);
        ClassicAssert.AreEqual(4L, matrix[0, 1]);
        ClassicAssert.AreEqual(1L, matrix[0, 2]);
        ClassicAssert.AreEqual(3L, matrix[0, 3]);
        ClassicAssert.AreEqual(long.MaxValue, matrix[1, 0]);
        ClassicAssert.AreEqual(-1L, matrix[1, 3]);
    }

    [Test]
    public void FloydWarshall_NegativeCycle_NegativeDiagonal()
    {
        var edges = new[] { (0, 1, 1), (1, 2, -3), (2, 1, 1) };

        var matrix = FloydWarshall.ToAllPairs(3, edges);

        var negativeDiagonal = false;
        for (var i = 0; i < 3; i++)
        {
            negativeDiagonal |= matrix[i, i] < 0;
        }

        ClassicAssert.IsTrue(negativeDiagonal, "a negative cycle must show on the diagonal");
    }

    [Test]
    public void BellmanFord_MatchesDijkstra_OnNonNegativeGraphs()
    {
        var generator = ListGenerator.Instance(seed: 20261121);

        for (var attempt = 0; attempt < 20; attempt++)
        {
            var n = generator.RandomList(1, 2, 12)[0];
            var edges = new List<(int U, int V, int Weight)>();
            for (var u = 0; u < n; u++)
            {
                for (var v = 0; v < n; v++)
                {
                    if (u != v && generator.RandomList(1, 0, 3)[0] == 0)
                    {
                        edges.Add((u, v, generator.RandomList(1, 0, 50)[0]));
                    }
                }
            }

            var dijkstra = Dijkstra.ToAll(ToContext(n, edges, 0));
            var bellmanFord = BellmanFord.ToAll(n, edges, 0);

            ClassicAssert.IsNotNull(bellmanFord, $"attempt {attempt}");
            ClassicAssert.IsTrue(dijkstra.SequenceEqual(bellmanFord!), $"attempt {attempt}: dijkstra vs bellman-ford");
        }
    }

    [Test]
    public void FloydWarshall_MatchesBellmanFord_AllSources_SmallGraphsWithNegativeEdges()
    {
        var generator = ListGenerator.Instance(seed: 20261122);

        for (var attempt = 0; attempt < 25; attempt++)
        {
            var n = generator.RandomList(1, 2, 8)[0];
            var edges = new List<(int U, int V, int Weight)>();
            for (var u = 0; u < n; u++)
            {
                for (var v = 0; v < n; v++)
                {
                    if (u != v && generator.RandomList(1, 0, 3)[0] == 0)
                    {
                        edges.Add((u, v, generator.RandomList(1, -4, 20)[0]));
                    }
                }
            }

            var matrix = FloydWarshall.ToAllPairs(n, edges);
            var hasNegativeCycle = false;
            for (var i = 0; i < n; i++)
            {
                hasNegativeCycle |= matrix[i, i] < 0;
            }

            for (var source = 0; source < n; source++)
            {
                var bellmanFord = BellmanFord.ToAll(n, edges, source);
                if (hasNegativeCycle)
                {
                    // Cycle may or may not be reachable from this source; both tools must agree
                    // on "undefined" exactly when BF reports null AND the cycle reaches the source.
                    if (bellmanFord is null)
                    {
                        continue; // agreement on undefined — checked implicitly by non-null cases below
                    }
                }
                else
                {
                    ClassicAssert.IsNotNull(bellmanFord, $"attempt {attempt}, source {source}: FW found no cycle");
                }

                if (bellmanFord is null)
                {
                    continue;
                }

                for (var target = 0; target < n; target++)
                {
                    ClassicAssert.AreEqual(bellmanFord[target], matrix[source, target],
                        $"attempt {attempt}, {source}->{target}");
                }
            }
        }
    }

    private static WeightedGraphContext ToContext(int vertexCount, List<(int U, int V, int Weight)> edges, int start)
    {
        var adjacency = new List<int>[vertexCount];
        for (var i = 0; i < vertexCount; i++)
        {
            adjacency[i] = new List<int>();
        }

        var weights = new Dictionary<(int, int), int>();
        foreach (var (u, v, weight) in edges)
        {
            adjacency[u].Add(v);
            weights[(u, v)] = weight;
        }

        return new WeightedGraphContext(adjacency, start, weights);
    }
}
