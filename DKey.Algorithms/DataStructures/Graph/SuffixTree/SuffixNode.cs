namespace DKey.Algorithms.DataStructures.Graph.SuffixTree;

public class SuffixNode<T> where T : IComparable<T>
{
    internal int ParentIndex;
    internal int Depth;
    internal int Offset;
    internal int ParentEdgeLength;
    internal Dictionary<T, int> children = new();
    internal int SuffixLink;
    internal int ParentNextDataIndex => Offset + Depth - ParentEdgeLength;

    public SuffixNode(int offset, int depth, int parentEdgeLength, int parentIndex, int suffixLink = -1)
    {
        Offset = offset;
        Depth = depth;
        ParentEdgeLength = parentEdgeLength;
        ParentIndex = parentIndex;
        SuffixLink = suffixLink;
    }
}
