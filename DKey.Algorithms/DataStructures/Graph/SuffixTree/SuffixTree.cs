namespace DKey.Algorithms.DataStructures.Graph.SuffixTree;

/// <summary>
/// Tree of suffixes for compact search of substrings.
/// </summary>
/// <typeparam name="T">Type of elements.</typeparam>
public class SuffixTree<T> where T : IComparable<T>
{

    #pragma warning disable CS8618
    public T[] Data;
    public int NodesSize;
    public SuffixNode<T>[] Nodes;
    
    public SuffixNode<T> Root;

    //Current longest suffix that is not leaf. Needed for efficient tree building.
    private Position CurrentLongestNonLeafSuffix;
    #pragma warning restore CS8618
    
    /// <summary>
    /// Build Suffix Tree from the list of T.
    /// </summary>
    /// <param name="data">list of T.</param>
    /// <param name="minElement">Element, which is less, than data.Min(). Tree might be broken, if this is not correctly provided.</param>
    /// <returns>Suffix Tree.</returns>
    public static SuffixTree<T> Build(IList<T> data, T minElement)
    {
        var tree = new SuffixTree<T>();
        tree.Data = new T[data.Count + 1];
        data.CopyTo(tree.Data, 0);
        tree.Data[^1] = minElement;
        tree.Root = new SuffixNode<T>(0, 0, 0, 0, 0);
        tree.Nodes = new SuffixNode<T>[2 * tree.Data.Length + 1];
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
    /// To add an item, for current iteration we must add all necessary splits and track CurrentLongestNonLeafSuffix.
    /// Necessary splits are splits for current non-leaf suffix and their prefixes.
    /// Those prefixes are calculated by traversing modified suffix link.
    /// If child with given element exists or root is reached we stop the loop, as splits are no longer necessary.
    /// </summary>
    private void AddSuffix(int index, T element)
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
    private void Split(Position position, T element, int dataIndex)
    {
        if (!IsVertex(position))
        {
            EdgeSplit(position, dataIndex);
        }
        var leafparent = Nodes[position.VertexIndex];
        var edgeLength = Data.Length - dataIndex;
        var newNode = new SuffixNode<T>(dataIndex - leafparent.Depth, leafparent.Depth+edgeLength, edgeLength, position.VertexIndex);
        Nodes[NodesSize] = newNode;
        leafparent.children[element] = NodesSize;
        NodesSize++;
        return;
    }
    
    private bool IsVertex(Position position) => Nodes[position.VertexIndex].ParentEdgeLength == position.ParentOffset;
    
    private void EdgeSplit(Position position, int dataOffset)
    {
        var currentVertex = Nodes[position.VertexIndex];
        var parent = Nodes[currentVertex.ParentIndex];
        
        var newNode = new SuffixNode<T>(dataOffset - position.ParentOffset - parent.Depth, parent.Depth+position.ParentOffset, position.ParentOffset, currentVertex.ParentIndex);
        Nodes[NodesSize] = newNode;
        var parentNextDataIndex = currentVertex.ParentNextDataIndex;
        var prevNextElement = Data[parentNextDataIndex + position.ParentOffset];
        newNode.children.Add(prevNextElement, position.VertexIndex);
        parent.children[Data[parentNextDataIndex]] = NodesSize;
        currentVertex.ParentIndex = NodesSize;
        currentVertex.ParentEdgeLength = currentVertex.Depth - newNode.Depth;
        position.Update(NodesSize, newNode.ParentEdgeLength);
        NodesSize++;
    }

    /// <summary>
    /// Get updated position after reading an item.
    /// If no valid position exists, returns false.
    /// </summary>
    internal bool TryGoDown(Position position, T element)
    {
        var vertex = Nodes[position.VertexIndex];
        if (position.ParentOffset < vertex.ParentEdgeLength)
        {
            if (element.Equals(Data[vertex.ParentNextDataIndex + position.ParentOffset]))
            {
                position.ParentOffset++;
                return true;
            }
            return false;
        }

        if (!vertex.children.TryGetValue(element, out var child)) return false;
        position.Update(child, 1);
        return true;

    }
    
    /// <summary>
    /// Supports getting suffix link from edges.
    /// </summary>
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
            if (currentVertex.children.TryGetValue(Data[nextDataOffset], out var child))
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
    public bool Contains(IEnumerable<T> data)
    {
        var position = new Position(0,0);
        foreach (T element in data)
        {
            if (!TryGoDown(position, element))
                return false;
        }
        return true;
    }

    /// <summary>
    /// Returns the longest common substring between srcdata and data.
    /// </summary>
    public (int srcOffset, int docOffset, int length) LongestCommonSubstring(IEnumerable<T> data)
    {
        var currentLength = 0;
        var currentIndex = -1;
        (int bestSrcOffset, int docOffset, int length) best = new();
        var position = new Position(0,0);
        foreach (T element in data)
        {
            currentIndex++;
            while (!TryGoDown(position, element) && currentLength > 0)
            {
                GoAnySuffixLink(position);
                currentLength--;
            }
            if(position.VertexIndex == 0)
                continue;

            currentLength++;
            if(currentLength>best.length)
                best = (Nodes[position.VertexIndex].Offset, currentIndex - currentLength + 1, currentLength);
        }
        return best;
    }
}