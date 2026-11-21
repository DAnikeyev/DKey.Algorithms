using DKey.Algorithms.DataStructures.Graph.Misc;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.DataStructures.Graph.Misc;

[TestFixture]
public class DinicTests
{
    [Test]
    public void ClassicNetwork_HandComputedFlow()
    {
        // CLRS-style network: s=0, t=5, max flow 23.
        var edges = new List<(int, int, int)>
        {
            (0, 1, 16), (0, 2, 13), (1, 3, 12), (2, 1, 4), (2, 4, 14),
            (3, 2, 9), (3, 5, 20), (4, 3, 7), (4, 5, 4),
        };

        var (flow, edgeFlows) = Dinic.MaxFlow(6, edges, 0, 5);

        ClassicAssert.AreEqual(23L, flow);
        ClassicAssert.GreaterOrEqual(edgeFlows.Count, 1);
    }

    [Test]
    public void SingleEdge_FlowEqualsCapacity()
    {
        var (flow, edgeFlows) = Dinic.MaxFlow(2, new[] { (0, 1, 17) }, 0, 1);

        ClassicAssert.AreEqual(17L, flow);
        ClassicAssert.AreEqual(1, edgeFlows.Count);
        ClassicAssert.AreEqual(17L, edgeFlows[0].Flow);
    }

    [Test]
    public void ParallelEdges_KeptSeparate_SumUp()
    {
        var edges = new[] { (0, 1, 3), (0, 1, 5) };

        var (flow, edgeFlows) = Dinic.MaxFlow(2, edges, 0, 1);

        ClassicAssert.AreEqual(8L, flow);
        ClassicAssert.AreEqual(2, edgeFlows.Count);
        Assert.That(edgeFlows.Select(e => e.Flow), Is.EquivalentTo(new[] { 3L, 5L }));
    }

    [Test]
    public void DisconnectedSourceSink_ZeroFlow()
    {
        var (flow, edgeFlows) = Dinic.MaxFlow(4, new[] { (0, 1, 5), (2, 3, 5) }, 0, 3);

        ClassicAssert.AreEqual(0L, flow);
        ClassicAssert.AreEqual(0, edgeFlows.Count);
    }

    [Test]
    public void ZeroCapacityEdge_CarriesNothing()
    {
        var (flow, _) = Dinic.MaxFlow(2, new[] { (0, 1, 0) }, 0, 1);

        ClassicAssert.AreEqual(0L, flow);
    }

    [Test]
    public void EdgeFlows_RespectCapacityAndConservation()
    {
        var generator = ListGenerator.Instance(seed: 20261121);
        var edges = new List<(int U, int V, int Capacity)>();
        for (var u = 0; u < 8; u++)
        {
            for (var v = 0; v < 8; v++)
            {
                if (u != v && generator.RandomList(1, 0, 3)[0] == 0)
                {
                    edges.Add((u, v, generator.RandomList(1, 1, 15)[0]));
                }
            }
        }

        var (flow, edgeFlows) = Dinic.MaxFlow(8, edges, 0, 7);

        var capacitySum = new Dictionary<(int, int), long>();
        foreach (var (u, v, capacity) in edges)
        {
            capacitySum[(u, v)] = capacitySum.GetValueOrDefault((u, v)) + capacity;
        }

        var perEdge = new Dictionary<(int, int), long>();
        foreach (var (from, to, used) in edgeFlows)
        {
            perEdge[(from, to)] = perEdge.GetValueOrDefault((from, to)) + used;
        }

        foreach (var ((u, v), used) in perEdge)
        {
            ClassicAssert.LessOrEqual(used, capacitySum[(u, v)],
                $"edge {u}->{v}: aggregate parallel flow exceeds capacity sum");
        }

        var balance = new long[8];
        foreach (var (from, to, used) in edgeFlows)
        {
            balance[from] -= used;
            balance[to] += used;
        }

        for (var vertex = 1; vertex < 7; vertex++)
        {
            ClassicAssert.AreEqual(0L, balance[vertex], $"vertex {vertex} breaks conservation");
        }

        ClassicAssert.AreEqual(-flow, balance[0]);
        ClassicAssert.AreEqual(flow, balance[7]);
    }

    [Test]
    public void RandomGraphs_FlowMatchesMinCutBruteForce()
    {
        var generator = ListGenerator.Instance(seed: 20261122);

        for (var attempt = 0; attempt < 30; attempt++)
        {
            var n = generator.RandomList(1, 2, 8)[0];
            var source = 0;
            var sink = n - 1;
            var edges = new List<(int U, int V, int Capacity)>();
            for (var u = 0; u < n; u++)
            {
                for (var v = 0; v < n; v++)
                {
                    if (u != v && generator.RandomList(1, 0, 2)[0] == 0)
                    {
                        edges.Add((u, v, generator.RandomList(1, 0, 20)[0]));
                    }
                }
            }

            var (flow, _) = Dinic.MaxFlow(n, edges, source, sink);
            var expected = BruteForceMinCut(n, edges, source, sink);

            ClassicAssert.AreEqual(expected, flow, $"attempt {attempt}: flow vs min-cut");
        }
    }

    /// <summary>
    /// Reference by max-flow = min-cut: enumerate subsets S containing the
    /// source but not the sink; the answer is the minimum capacity crossing
    /// S -> complement. Only viable for small graphs.
    /// </summary>
    private static long BruteForceMinCut(int n, List<(int U, int V, int Capacity)> edges, int source, int sink)
    {
        long best = long.MaxValue;
        for (var mask = 0; mask < (1 << n); mask++)
        {
            if ((mask & (1 << source)) == 0 || (mask & (1 << sink)) != 0)
            {
                continue;
            }

            long cut = 0;
            foreach (var (u, v, capacity) in edges)
            {
                if ((mask & (1 << u)) != 0 && (mask & (1 << v)) == 0)
                {
                    cut += capacity;
                }
            }

            best = Math.Min(best, cut);
        }

        return best == long.MaxValue ? 0 : best;
    }
}
