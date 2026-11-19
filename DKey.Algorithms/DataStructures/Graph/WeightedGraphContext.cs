namespace DKey.Algorithms.DataStructures.Graph;

/// <summary>
/// Traversal context for a weighted graph: the adjacency structure of
/// <see cref="GraphContext"/> plus the edge-weight table that weighted
/// algorithms (e.g. <c>Dijkstra</c>) read.
/// </summary>
public class WeightedGraphContext : GraphContext
{
    /// <summary>Edge weights keyed by (from, to) vertex pair.</summary>
    public Dictionary<(int, int), int> Weights;

    /// <summary>Wraps the adjacency list, the start vertex and the weight table.</summary>
    public WeightedGraphContext(List<int>[] graph, int currentVertex, Dictionary<(int, int), int> weights) : base(graph, currentVertex)
    {
        Weights = weights;
    }
}
