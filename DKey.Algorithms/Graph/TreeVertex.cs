namespace DKey.Algorithms.Graph;

public class TreeVertex
{
    public int parentIndex;
    public int index;
    public HashSet<int> children;

    public TreeVertex(int parentIndex, int index, HashSet<int> children)
    {
        this.parentIndex = parentIndex;
        this.index = index;
        this.children = children;
    }
}