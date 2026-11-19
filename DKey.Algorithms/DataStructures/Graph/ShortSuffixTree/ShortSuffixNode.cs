namespace DKey.Algorithms.DataStructures.Graph.ShortSuffixTree;

/// <summary>
/// A node of <see cref="ShortSuffixTree"/>: the same positional bookkeeping as
/// <see cref="SuffixTree.SuffixNode{T}"/>, with a fixed-size child table in
/// place of a dictionary (tokens must fit the table range).
/// </summary>
public struct ShortSuffixNode
{
    /// <summary>Index of the parent node.</summary>
    internal int ParentIndex;
    /// <summary>Depth of this node in elements from the root.</summary>
    internal int Depth;
    /// <summary>Index of this node's first element in the source data.</summary>
    internal int Offset;
    /// <summary>Length of the edge from the parent to this node.</summary>
    internal int ParentEdgeLength;
    /// <summary>Fixed-size table of child node indices, indexed by element.</summary>
    internal int[] Children = new int[32];
    /// <summary>Suffix link target node index; −1 while not yet set.</summary>
    internal int SuffixLink;
    /// <summary>Data index of the first element on the edge from the parent to this node.</summary>
    internal int ParentNextDataIndex => Offset + Depth - ParentEdgeLength;

    /// <summary>Creates a node with the given position bookkeeping; the suffix link defaults to unset.</summary>
    public ShortSuffixNode(int offset, int depth, int parentEdgeLength, int parentIndex, int suffixLink = -1)
    {
        Offset = offset;
        Depth = depth;
        ParentEdgeLength = parentEdgeLength;
        ParentIndex = parentIndex;
        SuffixLink = suffixLink;
    }
}
