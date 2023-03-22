namespace DKey.Algorithms.DataStructures.Graph.SuffixTree;

/// <summary>
/// Not Ready Yet
/// </summary>
/// <typeparam name="T"></typeparam>
public class SuffixTree<T> where T : IComparable
{
    internal struct Position
    {
        internal int VertexIndex;
        
        //Edge from above can represent list of elements. If position is in the middle of the edge, ParentOffset is equal to number of elements from parent of VertexIndex to the position.
        internal int ParentOffset;
        
    }

    public List<T> Data;
    internal List<SuffixNode<T>> Nodes;
    
    internal SuffixNode<T> Root;
    internal SuffixNode<T> Active;

    //Current longest suffix that is not leaf. Needed for efficient tree building.
    private Position CurrentLongestNonLeafSuffix;
    
    public static SuffixTree<T> Build(List<T> data, T minChar)
    {
        var tree = new SuffixTree<T>();
        tree.Data = new List<T>(data);
        tree.Data.Append(minChar);
        tree.Root = new SuffixNode<T>(0, 0, null, 0, null);
        tree.Nodes = new List<SuffixNode<T>> {tree.Root};
        tree.CurrentLongestNonLeafSuffix = new Position {VertexIndex = 0, ParentOffset = 0};
        for (int i = 0; i < data.Count; i++)
        {
            tree.AddSuffix(i, data[i]);
        }
        return tree;
    }
    
    public bool AreEqual(T x, T y)
    {
        return EqualityComparer<T>.Default.Equals(x, y);
    }

    private void AddSuffix(int index, T element)
    {
        var vertex = Nodes[CurrentLongestNonLeafSuffix.VertexIndex];
        var innerPosition = Nodes[CurrentLongestNonLeafSuffix.ParentOffset];
        if (TryGoDown(CurrentLongestNonLeafSuffix, element, out var next))
        {
            CurrentLongestNonLeafSuffix = next.Value;
        }
        else
        {
            int prevSplit = -1;
            while (true)
            {
                //TODO: Here, maybe update pos instead of creating new
                var curSplit = Split(CurrentLongestNonLeafSuffix, element);
                if(TryGoSuffixLink(CurrentLongestNonLeafSuffix, out var nextpos))
                    
                //CurrentLongestNonLeafSuffix = 
                if(prevSplit != -1)
                Nodes[prevSplit].SuffixLink = curSplit;

            }

        }
    }

    internal int Split(Position position, T element)
    {
        throw new NotImplementedException();
    }

    internal bool TryGoDown(Position position, T element, out Position? NewPosition)
    {
        var vertex = Nodes[position.VertexIndex];
        var innerPosition = position.ParentOffset;
        if (innerPosition < vertex.ParentEdgeLength)
        {
            if (AreEqual(Data[vertex.ParentEdgeOffset + innerPosition + 1],element))
            {
                NewPosition = new Position()
                    {VertexIndex = position.VertexIndex, ParentOffset = position.ParentOffset + 1};
                return true;
            }
        }
        else
        {
            if (vertex.children.TryGetValue(element, out var child))
            {
                NewPosition = new Position() {VertexIndex = Nodes[child].Index, ParentOffset = 1};
                return true;
            }
        }

        NewPosition = null;
        return false;
    }
    
    
    internal bool TryGoSuffixLink(Position position, out Position? NewPosition)
    {
        NewPosition = null;
        return false;
    }

    public bool Contains(List<T> data)
    {
        return true;
    }
}