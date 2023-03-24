using System.Collections.Specialized;

namespace DKey.Algorithms.DataStructures.Graph.SuffixTree;

internal class SuffixNode<T>  where T : IComparable
{
    public int ParentIndex;
    public int VertexIndex;
    public int Depth;
    public int Offset;
    public int ParentEdgeLength;
    public Dictionary<T, int> children = new Dictionary<T, int>();
    public int SuffixLink;
    public int nextDataIndex => Offset + Depth;
    public int parentNextDataIndex => Offset + Depth - ParentEdgeLength;

    public SuffixNode(int offset, int depth, int parentEdgeLength, int parentIndex, int vertexIndex, int suffixLink = -1)
    {
        Offset = offset;
        Depth = depth;
        ParentEdgeLength = parentEdgeLength;
        ParentIndex = parentIndex;
        VertexIndex = vertexIndex;
        SuffixLink = suffixLink;
    }
}