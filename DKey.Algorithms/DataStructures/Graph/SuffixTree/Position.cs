namespace DKey.Algorithms.DataStructures.Graph.SuffixTree;

internal class Position
{
    internal int VertexIndex;
        
    //Edge from above can represent list of elements. If position is in the middle of the edge,
    //ParentOffset is equal to number of elements from parent of VertexIndex to the position.
    internal int ParentOffset;

    public Position(int vertexIndex, int parentOffset)
    {
        VertexIndex = vertexIndex;
        ParentOffset = parentOffset;
    }

    public Position()
    {
        VertexIndex = 0;
        ParentOffset = 0;
    }
    
    public Position CopyFrom(Position other)
    {
        return new Position(other.VertexIndex, other.ParentOffset);
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