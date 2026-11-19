namespace DKey.Algorithms.DataStructures.Graph;

/// <summary>
/// Traversal context for breadth-first search (usable for DFS as well):
/// records the parent and depth of every visited vertex, the visited set,
/// and a flag an algorithm sets to stop the traversal early.
/// </summary>
public class TraverseContext : GraphContext
{
    /// <summary>Parent vertex and depth for every visited vertex.</summary>
    public Dictionary<int, (int parent, int depth)> VertexInfo = new();
    /// <summary>Set of already visited vertices.</summary>
    public HashSet<int> Used;
    /// <summary>Set by the per-vertex action to stop the traversal early.</summary>
    public bool stopFlag;
    /// <summary>Depth of the current vertex as recorded in <see cref="VertexInfo"/>.</summary>
    public virtual int Depth => VertexInfo[CurrentVertex].depth;
    /// <summary>Parent of the current vertex as recorded in <see cref="VertexInfo"/>.</summary>
    public virtual int Parent => VertexInfo[CurrentVertex].parent;
    /// <summary>Builds the context over the given graph; reuses <paramref name="used"/> when
    /// provided (e.g. to keep visits from a previous traversal).</summary>
    public TraverseContext(List<int>[] graph, int currentVertex, HashSet<int>? used = null) : base(graph, currentVertex)
    {
        Used = used ?? new HashSet<int>();
    }
}
