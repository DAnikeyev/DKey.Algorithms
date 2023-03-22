namespace DKey.Algorithms.DataStructures.Graph;

public class TreeVertex
{
    public int ParentIndex;
    public int Index;
    public List<int>? Children;

    public TreeVertex()
    {
    }

    public TreeVertex(int parentIndex, int index, List<int> children)
    {
        this.ParentIndex = parentIndex;
        this.Index = index;
        this.Children = children;
    }
}