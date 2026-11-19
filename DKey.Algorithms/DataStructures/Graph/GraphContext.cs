namespace DKey.Algorithms.DataStructures.Graph;

/// <summary>
/// Base traversal context: an adjacency-list graph plus the vertex a graph
/// algorithm starts from. Concrete contexts (<see cref="TraverseContext"/>
/// and its descendants) add the bookkeeping their traversal fills in.
/// </summary>
public abstract class GraphContext
{
    /// <summary>Adjacency list: neighbours per vertex, [0..n-1] representation.</summary>
    public List<int>[] Graph;
    /// <summary>The vertex the traversal currently stands on; the start vertex at construction.</summary>
    public int CurrentVertex;

    /// <summary>Builds the context over the given graph, starting at <paramref name="currentVertex"/>.</summary>
    public GraphContext(List<int>[] graph, int currentVertex)
    {
        Graph = graph;
        CurrentVertex = currentVertex;
    }
}
