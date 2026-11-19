namespace DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

/// <summary>
/// Traversal context for depth-first search: adds the parent stack that
/// tracks the path from the start vertex to the current vertex (used by the
/// DFS variants to detect exits and rebuild paths).
/// </summary>
public class DFSContext : TraverseContext
{
    /// <summary>Vertices on the current root-to-current path; the top is the current parent.</summary>
    public Stack<int> Parents = new();

    /// <summary>Depth of the current vertex (path length from the start vertex).</summary>
    public int TraverseDepth => Parents.Count;

    /// <summary>The vertex visited right before the current one on the current path.</summary>
    public int TraverseParent => Parents.Peek();

    /// <summary>Builds the DFS context over the given graph, starting at <paramref name="currentVertex"/>.</summary>
    public DFSContext(List<int>[] graph, int currentVertex, HashSet<int>? used = null) : base(graph, currentVertex, used)
    {
    }
}
