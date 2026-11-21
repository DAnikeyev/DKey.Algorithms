namespace DKey.Algorithms.DataStructures.Graph.Misc;

/// <summary>
/// Bellman-Ford single-source shortest paths over directed edge lists —
/// handles negative weights and detects negative cycles reachable from the
/// source (the two things <see cref="Dijkstra"/> cannot do).
/// </summary>
public static class BellmanFord
{
    /// <summary>
    /// Shortest distances from <paramref name="start"/> over the directed
    /// edge list; unreachable vertices keep <see cref="long.MaxValue"/>.
    /// Returns null when a negative cycle is reachable from
    /// <paramref name="start"/> (no well-defined distances). Negative cycles
    /// unreachable from the source do not affect the result.
    /// Complexity: O(V·E) time, O(V + E) memory.
    /// </summary>
    /// <param name="vertexCount">Vertices are 0..vertexCount-1.</param>
    /// <param name="edges">Directed weighted edges (u -> v); parallel edges
    /// allowed — the cheapest effectively wins.</param>
    /// <param name="start">Source vertex.</param>
    public static long[]? ToAll(int vertexCount, IEnumerable<(int U, int V, int Weight)> edges, int start)
    {
        var edgeList = edges.ToArray();
        var distances = new long[vertexCount];
        for (var i = 0; i < vertexCount; i++)
        {
            distances[i] = long.MaxValue;
        }

        distances[start] = 0;

        for (var iteration = 0; iteration < vertexCount; iteration++)
        {
            var changed = false;
            foreach (var (u, v, weight) in edgeList)
            {
                if (distances[u] == long.MaxValue || distances[u] + weight >= distances[v])
                {
                    continue;
                }

                if (iteration == vertexCount - 1)
                {
                    return null; // still improving after V-1 full rounds: reachable negative cycle
                }

                distances[v] = distances[u] + weight;
                changed = true;
            }

            if (!changed)
            {
                break;
            }
        }

        return distances;
    }
}
