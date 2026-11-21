namespace DKey.Algorithms.DataStructures.Graph.Misc;

/// <summary>
/// Dinic max-flow over directed capacitated edge lists: the flow value plus a
/// feasible per-edge flow. Multi-source / multi-sink networks are handled by
/// the caller with a super-source / super-sink of infinite capacity — kept out
/// of the API deliberately.
/// </summary>
public static class Dinic
{
    /// <summary>
    /// Maximum flow from <paramref name="source"/> to <paramref name="sink"/>.
    /// Parallel edges are kept as separate slots (each input edge gets its own
    /// flow entry); self-loops never carry flow. Edge flows satisfy the
    /// capacity constraints and conservation, and sum to the returned value.
    /// Complexity: O(V²E) time, O(V + E) memory.
    /// </summary>
    /// <param name="vertexCount">Vertices are 0..vertexCount-1.</param>
    /// <param name="edges">Directed edges with capacities (non-negative).</param>
    /// <param name="source">Flow origin; must differ from
    /// <paramref name="sink"/>.</param>
    /// <param name="sink">Flow target.</param>
    public static (long Flow, List<(int From, int To, long Flow)> EdgeFlows) MaxFlow(
        int vertexCount,
        IEnumerable<(int U, int V, int Capacity)> edges,
        int source,
        int sink)
    {
        var edgeList = edges.ToArray();
        var edgeCount = edgeList.Length * 2;
        var to = new int[edgeCount];
        var capacity = new long[edgeCount];
        var adjacency = new List<int>[vertexCount];
        for (var i = 0; i < vertexCount; i++)
        {
            adjacency[i] = new List<int>();
        }

        for (var i = 0; i < edgeList.Length; i++)
        {
            var (u, v, cap) = edgeList[i];
            to[2 * i] = v;
            capacity[2 * i] = cap;
            adjacency[u].Add(2 * i);
            to[2 * i + 1] = u;
            capacity[2 * i + 1] = 0;
            adjacency[v].Add(2 * i + 1);
        }

        var levels = new int[vertexCount];
        var iterator = new int[vertexCount];

        bool BuildLevels()
        {
            Array.Fill(levels, -1);
            levels[source] = 0;
            var queue = new Queue<int>();
            queue.Enqueue(source);
            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var edge in adjacency[vertex])
                {
                    if (capacity[edge] > 0 && levels[to[edge]] < 0)
                    {
                        levels[to[edge]] = levels[vertex] + 1;
                        queue.Enqueue(to[edge]);
                    }
                }
            }

            return levels[sink] >= 0;
        }

        long Send(int vertex, long amount)
        {
            if (vertex == sink)
            {
                return amount;
            }

            for (; iterator[vertex] < adjacency[vertex].Count; iterator[vertex]++)
            {
                var edge = adjacency[vertex][iterator[vertex]];
                var next = to[edge];
                if (capacity[edge] > 0 && levels[next] == levels[vertex] + 1)
                {
                    var sent = Send(next, Math.Min(amount, capacity[edge]));
                    if (sent > 0)
                    {
                        capacity[edge] -= sent;
                        capacity[edge ^ 1] += sent;
                        return sent;
                    }
                }
            }

            return 0;
        }

        long total = 0;
        while (BuildLevels())
        {
            Array.Fill(iterator, 0);
            long sent;
            while ((sent = Send(source, long.MaxValue)) > 0)
            {
                total += sent;
            }
        }

        var flows = new List<(int From, int To, long Flow)>(edgeList.Length);
        for (var i = 0; i < edgeList.Length; i++)
        {
            var used = edgeList[i].Capacity - capacity[2 * i];
            if (used > 0)
            {
                flows.Add((edgeList[i].U, edgeList[i].V, used));
            }
        }

        return (total, flows);
    }
}
