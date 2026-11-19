namespace DKey.Algorithms.DataStructures.Graph.SuffixTree;

/// <summary>
/// The current traversal position inside <see cref="SuffixTree{T}"/> while it
/// is being built: a node plus how far along the edge from its parent the
/// position sits.
/// </summary>
internal class Position
{
    /// <summary>Index of the node the position is at or below.</summary>
    internal int VertexIndex;

    //Edge from above can represent list of elements. If position is in the middle of the edge,
    //ParentOffset is equal to number of elements from parent of VertexIndex to the position.
    //Edge from above can represent list of elements. If position is in the middle of the edge,
    //ParentOffset is equal to number of elements from parent of VertexIndex to the position.
    internal int ParentOffset;

    /// <summary>Places the position at the given node, <paramref name="parentOffset"/> elements down its parent edge.</summary>
    public Position(int vertexIndex, int parentOffset)
    {
        VertexIndex = vertexIndex;
        ParentOffset = parentOffset;
    }

    /// <summary>Creates the root position.</summary>
    public Position()
    {
        VertexIndex = 0;
        ParentOffset = 0;
    }

    /// <summary>Returns a copy of the given position.</summary>
    public Position CopyFrom(Position other)
    {
        return new Position(other.VertexIndex, other.ParentOffset);
    }
    /// <summary>Resets the position to the root.</summary>
    public void Clear()
    {
        VertexIndex = 0;
        ParentOffset = 0;
    }
    /// <summary>Moves the position to the given node and edge offset.</summary>
    public void Update(int vertexIndex, int parentOffset)
    {
        VertexIndex = vertexIndex;
        ParentOffset = parentOffset;
    }
}
