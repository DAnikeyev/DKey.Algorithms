
using DKey.Algorithms.DataStructures.Graph.SuffixTree;

namespace DKey.Algorithms.DataStructures.Graph.ShortSuffixTree;

/// <summary>
/// Tree of suffixes for compact search of substrings.
/// About twice as fast as SuffixTree<T>, but require elements in data to be in range [1, 31].
/// Doesn't work for some alphabets, but English is fine ;)
/// Performance is elastic by the size of the children array in Node, feel free to change it if your data allows it.
/// It's possible to optimize Childrens further by counting distincts in data and mapping it to smaller interval,
/// but it takes time and memory, while helpful only in specific cases.
/// </summary>
public class ShortSuffixTree
{
    #pragma warning disable CS8618
    public int[] Data;
    public int NodesSize;
    public ShortSuffixNode[] Nodes;
    
    public ShortSuffixNode Root;

    //Current longest suffix that is not leaf. Needed for efficient tree building.
    private Position CurrentLongestNonLeafSuffix;
    
#pragma warning restore CS8618
    /// <summary>
    /// Build Suffix Tree from the list of T.
    /// </summary>
    /// <param name="data">list of T.</param>
    /// <param name="minChar">Element, which is less, than data.Min(). Tree might be broken, if this is not correctly provided.</param>
    /// <returns>Suffix Tree.</returns>
    public static ShortSuffixTree Build(IList<int> data, int minElement = 0)
    {
        var tree = new ShortSuffixTree();
        tree.Data = new int[data.Count + 1];
        data.CopyTo(tree.Data, 0);
        tree.Data[^1] = minElement;
        tree.Root = new ShortSuffixNode(0, 0, 0, 0, 0);
        tree.Nodes = new ShortSuffixNode[2 * tree.Data.Length + 1];
        tree.Nodes[0] = tree.Root;
        tree.NodesSize = 1;
        tree.CurrentLongestNonLeafSuffix = new Position(0, 0);
        var index = 0;
        foreach (var item in tree.Data)
        {
            tree.AddSuffix(index, item);
            index++;
        }
        return tree;
    }
    /// <summary>
    /// To add an item, for current iteration we must add
    /// all necessary splits and track CurrentLongestNonLeafSuffix
    /// Necessary splits are splits for current non-leaf suffix and their prefixes
    /// Those prefixes are calculated by going by modified suffix link.
    /// If child with given element exists or root is reached we stop the loop, as splits are no longer necessary.
    /// </summary>
    private void AddSuffix(int index, int element)
    {
        var prevIndex = -1;
        var current = CurrentLongestNonLeafSuffix;

        while (true)
        {
            if (TryGoDown(current, element))
            {
                CurrentLongestNonLeafSuffix = current;
                if (prevIndex != -1 && Nodes[prevIndex].SuffixLink == -1)
                    Nodes[prevIndex].SuffixLink = Nodes[current.VertexIndex].ParentIndex;
                break;
            }
 
            Split(current, element, index);
            if (prevIndex != -1 && Nodes[prevIndex].SuffixLink == -1)
                Nodes[prevIndex].SuffixLink = current.VertexIndex;
            prevIndex = current.VertexIndex;
            if (current.VertexIndex == 0)
            {
                CurrentLongestNonLeafSuffix = current;
                break;
            }

            GoAnySuffixLink(current);
        }
    }

    /// <summary>
    /// New leaf vertex is generated for new item.
    /// if position is in the middle of the edge, the vertex is also created for this position.
    /// </summary>
    private void Split(Position position, int element, int dataIndex)
    {
        if (!IsVertex(position))
        {
            EdgeSplit(position, dataIndex);
        }
        var depth = Nodes[position.VertexIndex].Depth;
        var edgeLength = Data.Length - dataIndex;
        Nodes[NodesSize] = new ShortSuffixNode(dataIndex - depth, depth+edgeLength, edgeLength, position.VertexIndex);
        Nodes[position.VertexIndex].Children[element] = NodesSize;
        NodesSize++;
        return;
    }
    
    private bool IsVertex(Position position) => Nodes[position.VertexIndex].ParentEdgeLength == position.ParentOffset;
    
    private void EdgeSplit(Position position, int dataOffset)
    {
        var currentVertex = Nodes[position.VertexIndex];
        var parent = Nodes[currentVertex.ParentIndex];
        
        var newNode = new ShortSuffixNode(dataOffset - position.ParentOffset - parent.Depth, parent.Depth+position.ParentOffset, position.ParentOffset, currentVertex.ParentIndex);
        Nodes[NodesSize] = newNode;
        var parentNextDataIndex = currentVertex.ParentNextDataIndex;
        var prevNextElement = Data[parentNextDataIndex + position.ParentOffset];
        newNode.Children[prevNextElement] = position.VertexIndex;
        Nodes[currentVertex.ParentIndex].Children[Data[parentNextDataIndex]] = NodesSize;
        Nodes[position.VertexIndex].ParentIndex = NodesSize;
        Nodes[position.VertexIndex].ParentEdgeLength = currentVertex.Depth - newNode.Depth;
        position.Update(NodesSize, newNode.ParentEdgeLength);
        NodesSize++;
    }

    /// <summary>
    /// Get updated position after reading an item.
    /// If no valid position exists, returns false.
    /// </summary>
    internal bool TryGoDown(Position position, int element)
    {
        var vertex = Nodes[position.VertexIndex];
        if (position.ParentOffset < vertex.ParentEdgeLength)
        {
            if (element == Data[vertex.ParentNextDataIndex + position.ParentOffset])
            {
                position.ParentOffset++;
                return true;
            }
            return false;
        }

        var child = vertex.Children[element];
        if (child == 0) return false;
        position.Update(child, 1);
        return true;

    }
    
    /// <summary>
    /// Supports getting suffix link from edges.
    /// <summary>
    internal void GoAnySuffixLink(Position position)
    {
        if (TryGoVertexSuffixLink(position))
        {
            return;
        }

        var shift = position.ParentOffset;
        if (position.VertexIndex == 0)
        {
            position.Clear();
            return;
        }
        var currentChild = Nodes[position.VertexIndex];
        var parent = Nodes[currentChild.ParentIndex];
        var nextDataOffset = currentChild.ParentNextDataIndex;
        if (currentChild.ParentIndex == 0)
        {
            nextDataOffset += 1;
            shift -= 1;
            if(shift <= 0)
            {
                position.Clear();
                return;
            }
        }

        var currentVertex = Nodes[parent.SuffixLink];
        while (true)
        {
            
            var child = currentVertex.Children[Data[nextDataOffset]];
             if (child != 0)
            {
                currentVertex = Nodes[child];
                var edgeLength = currentVertex.ParentEdgeLength;
                if ( edgeLength >= shift)
                {
                    position.Update(child, shift);
                    return;
                }

                shift -= edgeLength;
                nextDataOffset += edgeLength;
                continue;
            }

            throw new Exception("Should be unreachable");
        }
    }
    
    /// <summary>
    /// Get a suffix link for vertex.
    /// </summary>
    internal bool TryGoVertexSuffixLink(Position position)
    {
        var vertex = Nodes[position.VertexIndex];
        if (vertex.ParentEdgeLength == position.ParentOffset && vertex.SuffixLink != -1)
        {
            var newIndex = vertex.SuffixLink;
            var newPos = Nodes[newIndex].ParentEdgeLength;
            position.Update(newIndex, newPos);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Does srcdata contain data.
    /// </summary>
    public bool Contains(IEnumerable<int> data)
    {
        var position = new Position(0,0);
        foreach (var element in data)
        {
            if (!TryGoDown(position, element))
                return false;
        }
        return true;
    }
}