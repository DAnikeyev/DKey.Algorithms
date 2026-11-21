namespace DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;

/// <summary>
/// Topological sorting of directed graphs (Kahn's algorithm).
/// </summary>
public class TopologicalSort
{
    /// <summary>
    /// Returns a topological order of the directed graph (every edge u -> v
    /// places u before v), or null when the graph contains a cycle — the
    /// "no answer" convention used by <see cref="Cycle.Find"/> and
    /// <see cref="ShortestPath.Get"/>. A disconnected graph yields one valid
    /// order with the components interleaved. An empty graph returns an empty
    /// array. Complexity: O(V + E).
    /// </summary>
    /// <param name="graph">Adjacency list; graph[i] are the successors of vertex i.</param>
    public static int[]? Get(List<int>[] graph)
    {
        var size = graph.Length;
        var inDegree = new int[size];
        foreach (var successors in graph)
        {
            foreach (var next in successors)
            {
                inDegree[next]++;
            }
        }

        var queue = new Queue<int>();
        for (var i = 0; i < size; i++)
        {
            if (inDegree[i] == 0)
            {
                queue.Enqueue(i);
            }
        }

        var order = new int[size];
        var processed = 0;
        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();
            order[processed++] = vertex;
            foreach (var next in graph[vertex])
            {
                if (--inDegree[next] == 0)
                {
                    queue.Enqueue(next);
                }
            }
        }

        return processed == size ? order : null;
    }
}
