namespace DKey.Algorithms.DataStructures.Graph.ShortSuffixTree;

internal class ShortPosition
{
    internal int VertexIndex;
        
    //Edge from above can represent list of elements. If position is in the middle of the edge,
    //ParentOffset is equal to number of elements from parent of VertexIndex to the position.
    internal int ParentOffset;

    public ShortPosition(int vertexIndex, int parentOffset)
    {
        VertexIndex = vertexIndex;
        ParentOffset = parentOffset;
    }

    public ShortPosition()
    {
        VertexIndex = 0;
        ParentOffset = 0;
    }
    
    public ShortPosition CopyFrom(ShortPosition other)
    {
        return new ShortPosition(other.VertexIndex, other.ParentOffset);
    }
    public void Clear()
    {
        VertexIndex = 0;
        ParentOffset = 0;
    }
    public void Update(int vertexIndex, int parentOffset)
    {
        VertexIndex = vertexIndex;
        ParentOffset = parentOffset;
    }
}