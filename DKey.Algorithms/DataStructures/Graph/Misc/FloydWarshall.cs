namespace DKey.Algorithms.DataStructures.Graph.Misc;

/// <summary>
/// Floyd-Warshall all-pairs shortest paths over a directed edge list — the
/// O(n³)/O(n²) choice for dense small graphs; a negative cycle exists iff
/// some diagonal entry ends up negative.
/// </summary>
public static class FloydWarshall
{
    /// <summary>
    /// The full n x n distance matrix: d[i,i] = 0, d[i,j] = shortest
    /// i -> j distance or <see cref="long.MaxValue"/> when unreachable.
    /// A negative value on the diagonal marks a negative cycle through that
    /// vertex (other entries in the affected component are then not
    /// meaningful). Parallel edges collapse to the cheapest.
    /// Memory: O(n²) — intended for small n only. Complexity: O(n³) time.
    /// </summary>
    /// <param name="vertexCount">Vertices are 0..vertexCount-1.</param>
    /// <param name="edges">Directed weighted edges.</param>
    public static long[,] ToAllPairs(int vertexCount, IEnumerable<(int U, int V, int Weight)> edges)
    {
        var distances = new long[vertexCount, vertexCount];
        for (var i = 0; i < vertexCount; i++)
        {
            for (var j = 0; j < vertexCount; j++)
            {
                distances[i, j] = i == j ? 0 : long.MaxValue;
            }
        }

        foreach (var (u, v, weight) in edges)
        {
            if (weight < distances[u, v])
            {
                distances[u, v] = weight;
            }
        }

        for (var k = 0; k < vertexCount; k++)
        {
            for (var i = 0; i < vertexCount; i++)
            {
                if (distances[i, k] == long.MaxValue)
                {
                    continue;
                }

                for (var j = 0; j < vertexCount; j++)
                {
                    if (distances[k, j] == long.MaxValue)
                    {
                        continue;
                    }

                    var candidate = distances[i, k] + distances[k, j];
                    if (candidate < distances[i, j])
                    {
                        distances[i, j] = candidate;
                    }
                }
            }
        }

        return distances;
    }
}
