namespace DKey.Algorithms.DataStructures.Graph;

public class TreeVertex
{
    public int ParentIndex;
    public int Index;
    public HashSet<int> Children;

    public TreeVertex(int parentIndex, int index, HashSet<int> children)
    {
        this.ParentIndex = parentIndex;
        this.Index = index;
        this.Children = children;
    }
}