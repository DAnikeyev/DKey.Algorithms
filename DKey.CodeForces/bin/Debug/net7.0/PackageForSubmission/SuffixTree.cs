using System.Collections;

namespace DKey.Algorithms.DataStructures.Graph.SuffixTree;

/// <summary>
/// Tree of suffixes for compact search of substrings.
/// </summary>
/// <typeparam name="T">Type of elements.</typeparam>
public class SuffixTree<T> where T : IComparable<T>
{
    internal struct Position
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
    }

    public T[] Data;
    internal List<SuffixNode<T>> Nodes;
    
    internal SuffixNode<T> Root;

    //Current longest suffix that is not leaf. Needed for efficient tree building.
    private Position CurrentLongestNonLeafSuffix;
    
    /// <summary>
    /// Build Suffix Tree from the list of T.
    /// </summary>
    /// <param name="data">list of T.</param>
    /// <param name="minChar">Element, which is less, than data.Min(). Tree might be broken, if this is not correctly provided.</param>
    /// <returns>Suffix Tree.</returns>
    public static SuffixTree<T> Build(IList<T> data, T minElement)
    {
        var tree = new SuffixTree<T>();
        tree.Data = new T[data.Count + 1];
        data.CopyTo(tree.Data, 0);
        tree.Data[^1] = minElement;
        tree.Root = new SuffixNode<T>(0, 0, 0, 0, 0, 0);
        tree.Nodes = new List<SuffixNode<T>> {tree.Root};
        tree.CurrentLongestNonLeafSuffix = new Position {VertexIndex = 0, ParentOffset = 0};
        var index = 0;
        foreach (var item in tree.Data)
        {
            tree.AddSuffix(index, item);
            index++;
        }
        return tree;
    }
    private bool AreEqual(T x, T y)
    {
        return EqualityComparer<T>.Default.Equals(x, y);
    }

    /// <summary>
    /// To add an item, for current iteration we must add
    /// all necessary splits and track CurrentLongestNonLeafSuffix
    /// Necessary splits are splits for current non-leaf suffix and their prefixes
    /// Those prefixes are calculated by going by modified suffix link.
    /// If child with given element exists or root is reached we stop the loop, as splits are no longer necessary.
    /// </summary>
    private void AddSuffix(int index, T element)
    {
        var prevIndex = -1;
        var current = CurrentLongestNonLeafSuffix;

        while (true)
        {
            if (TryGoDown(current, element, out var newPosition))
            {
                CurrentLongestNonLeafSuffix = newPosition.Value;
                if (prevIndex != -1 && Nodes[prevIndex].SuffixLink == -1)
                    Nodes[prevIndex].SuffixLink = Nodes[newPosition.Value.VertexIndex].ParentIndex;
                break;
            }
 
            var createdVertex = Split(current, element, index);
            if (prevIndex != -1 && Nodes[prevIndex].SuffixLink == -1)
                Nodes[prevIndex].SuffixLink = createdVertex.VertexIndex;
            prevIndex = createdVertex.VertexIndex;
            current = createdVertex;
            if (current.VertexIndex == 0)
            {
                CurrentLongestNonLeafSuffix = current;
                break;
            }

            current = GoAnySuffixLink(current);
        }
    }


    /// <summary>
    /// New leaf vertex is generated for new item.
    /// if position is in the middle of the edge, the vertex is also created for this position.
    /// </summary>
    private Position Split(Position position, T element, int dataIndex)
    {
        var leafParentPosition = position;
        if (!IsVertex(position))
        {
            leafParentPosition = EdgeSplit(position, dataIndex);
        }
        var newNodeIndex = Nodes.Count;
        var leafparent = Nodes[leafParentPosition.VertexIndex];
        var edgeLength = Data.Length - dataIndex;
        var newNode = new SuffixNode<T>(dataIndex - leafparent.Depth, leafparent.Depth+edgeLength, edgeLength, leafParentPosition.VertexIndex, newNodeIndex);
        Nodes.Add(newNode);
        leafparent.children[element] = newNodeIndex;
        return leafParentPosition;
    }

    private T NextEdgeSymbol(Position position)
    {
        var currentVertex = Nodes[position.VertexIndex];
        return Data[currentVertex.Offset + currentVertex.Depth - currentVertex.ParentEdgeLength + position.ParentOffset + 1];
    }

    private bool IsVertex(Position position) => Nodes[position.VertexIndex].ParentEdgeLength == position.ParentOffset;
    
    private Position EdgeSplit(Position position, int dataOffset)
    {
        var currentVertex = Nodes[position.VertexIndex];
        var parent = Nodes[currentVertex.ParentIndex];
        
        var newNodeIndex = Nodes.Count;
        var newNode = new SuffixNode<T>(dataOffset - position.ParentOffset - parent.Depth, parent.Depth+position.ParentOffset, position.ParentOffset, currentVertex.ParentIndex, newNodeIndex);
        Nodes.Add(newNode);
        var parentNextDataIndex = currentVertex.ParentNextDataIndex;
        var prevNextElement = Data[parentNextDataIndex + position.ParentOffset];
        newNode.children.Add(prevNextElement, position.VertexIndex);
        parent.children[Data[parentNextDataIndex]] = newNodeIndex;
        currentVertex.ParentIndex = newNodeIndex;
        currentVertex.ParentEdgeLength = currentVertex.Depth - newNode.Depth;
        return new Position(newNodeIndex, newNode.ParentEdgeLength);
    }

    /// <summary>
    /// Get updated position after reading an item.
    /// If no valid position exists, returns false.
    /// </summary>
    internal bool TryGoDown(Position position, T element, out Position? newPosition)
    {
        newPosition = null;
        var vertex = Nodes[position.VertexIndex];
        var innerPosition = position.ParentOffset;
        if (innerPosition < vertex.ParentEdgeLength)
        {
            if (AreEqual(Data[vertex.ParentNextDataIndex + innerPosition], element))
            {
                newPosition = position with {ParentOffset = innerPosition + 1};
                return true;
            }
            return false;
        }
        if (vertex.children.TryGetValue(element, out var child))
        {
            newPosition = new Position() {VertexIndex = Nodes[child].VertexIndex, ParentOffset = 1};
            return true;
        }

        newPosition = null;
        return false;
    }
    
    /// <summary>
    /// Supports getting suffix link from edges.
    /// <summary>
    internal Position GoAnySuffixLink(Position position)
    {
        if (TryGoVertexSuffixLink(position, out var sfxPosition))
        {
            return sfxPosition;
        }

        var shift = position.ParentOffset;
        if (position.VertexIndex == 0)
            return new Position(0, 0);
        var currentChild = Nodes[position.VertexIndex];
        var parent = Nodes[currentChild.ParentIndex];
        var nextDataOffset = currentChild.ParentNextDataIndex;
        if (parent.VertexIndex == 0)
            if (parent.VertexIndex == 0) // Add this condition
            {
                nextDataOffset += 1;
                shift -= 1;
                if(shift <= 0)
                    return new Position(0, 0);
            }

        var currentVertex = Nodes[parent.SuffixLink];
        while (true)
        {
            if (currentVertex.children.TryGetValue(Data[nextDataOffset], out var child))
            {
                currentVertex = Nodes[child];
                var edgeLength = currentVertex.ParentEdgeLength;
                if ( edgeLength >= shift)
                {
                    return new Position(child, shift);
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
    internal bool TryGoVertexSuffixLink(Position position, out Position newPosition)
    {
        var vertex = Nodes[position.VertexIndex];
        if (vertex.ParentEdgeLength == position.ParentOffset && vertex.SuffixLink != -1)
        {
            var newIndex = vertex.SuffixLink;
            var newPos = Nodes[newIndex].ParentEdgeLength;
            newPosition = new Position(newIndex, newPos);
            return true;
        }
        newPosition = new Position();
        return false;
    }

    /// <summary>
    /// Does srcdata contain data.
    /// </summary>
    public bool Contains(IEnumerable<T> data)
    {
        var position = new Position { VertexIndex = 0, ParentOffset = 0 };
        foreach (T element in data)
        {
            if (!TryGoDown(position, element, out var newPosition))
                return false;
            position = newPosition.Value;
        }
        return true;
    }
}