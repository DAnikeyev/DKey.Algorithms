namespace DKey.Algorithms.DataStructures.Graph.ShortSuffixTree;

public struct ShortSuffixNode
{
    internal int ParentIndex;
    internal int Depth;
    internal int Offset;
    internal int ParentEdgeLength;
    internal int[] Children = new int[32];
    internal int SuffixLink;
    internal int ParentNextDataIndex => Offset + Depth - ParentEdgeLength;

    public ShortSuffixNode(int offset, int depth, int parentEdgeLength, int parentIndex, int suffixLink = -1)
    {
        Offset = offset;
        Depth = depth;
        ParentEdgeLength = parentEdgeLength;
        ParentIndex = parentIndex;
        SuffixLink = suffixLink;
    }
}