using System.Collections.Specialized;

namespace DKey.Algorithms.DataStructures.Graph.SuffixTree;

internal class SuffixNode<T>  where T : IComparable
{
    public int? ParentIndex;
    public int Index;
    public int Depth;
    public int ParentEdgeOffset;
    public int ParentEdgeLength;
    public Dictionary<T, int> children = new Dictionary<T, int>();
    public int? SuffixLink;
    public int SubstringOffset => ParentEdgeOffset + ParentEdgeLength - Depth;

    public SuffixNode(int parentEdgeOffset, int parentEdgeLength, int? parentIndex, int index, int? suffixLink)
    {
        ParentIndex = parentIndex;
        Index = index;
        SuffixLink = suffixLink;
        ParentEdgeOffset = parentEdgeOffset;
        ParentEdgeLength = parentEdgeLength;
    }
}