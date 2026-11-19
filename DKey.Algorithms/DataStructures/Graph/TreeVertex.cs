namespace DKey.Algorithms.DataStructures.Graph;

/// <summary>
/// A vertex of <see cref="TreeGraph"/>: its index, parent and children.
/// </summary>
public class TreeVertex
{
    /// <summary>Index of the parent vertex, −1 for the root.</summary>
    public int ParentIndex;
    /// <summary>Index of this vertex in the graph.</summary>
    public int Index;
    /// <summary>Indices of the child vertices.</summary>
    public List<int> Children;

    /// <summary>Creates a vertex with no children.</summary>
    public TreeVertex()
    {
        Children = new List<int>();
    }

    /// <summary>Creates a vertex with the given parent, index and children.</summary>
    public TreeVertex(int parentIndex, int index, List<int> children)
    {
        this.ParentIndex = parentIndex;
        this.Index = index;
        this.Children = children;
    }
}
