namespace DKey.Algorithms.DataStructures.Graph.SuffixTree;

/// <summary>
/// A node of <see cref="SuffixTree{T}"/>: positional bookkeeping (offset and
/// depth in the source data), the parent-edge length, children keyed by the
/// token that leads to them, and the suffix link used while building.
/// </summary>
/// <typeparam name="T">Type of elements.</typeparam>
public class SuffixNode<T> where T : IComparable<T>
{
    /// <summary>Index of the parent node (0 = root is its own child pointer).</summary>
    internal int ParentIndex;
    /// <summary>Depth of this node in elements from the root.</summary>
    internal int Depth;
    /// <summary>Index of this node's first element in the source data.</summary>
    internal int Offset;
    /// <summary>Length of the edge from the parent to this node.</summary>
    internal int ParentEdgeLength;
    /// <summary>Child nodes keyed by the token that leads to them.</summary>
    internal Dictionary<T, int> children = new();
    /// <summary>Suffix link target node index; −1 while not yet set.</summary>
    internal int SuffixLink;
    /// <summary>Data index of the first element on the edge from the parent to this node.</summary>
    internal int ParentNextDataIndex => Offset + Depth - ParentEdgeLength;

    /// <summary>Creates a node with the given position bookkeeping; the suffix link defaults to unset.</summary>
    public SuffixNode(int offset, int depth, int parentEdgeLength, int parentIndex, int suffixLink = -1)
    {
        Offset = offset;
        Depth = depth;
        ParentEdgeLength = parentEdgeLength;
        ParentIndex = parentIndex;
        SuffixLink = suffixLink;
    }
}
