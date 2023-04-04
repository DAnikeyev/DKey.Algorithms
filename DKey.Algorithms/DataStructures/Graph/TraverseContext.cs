namespace DKey.Algorithms.DataStructures.Graph;

public class TraverseContext : GraphContext
{
    public Dictionary<int, (int parent, int depth)> VertexInfo = new();
    public HashSet<int> Used;
    public bool stopFlag;
    public virtual int Depth => VertexInfo[CurrentVertex].depth;
    public virtual int Parent => VertexInfo[CurrentVertex].parent;
    public TraverseContext(List<int>[] graph, int currentVertex, HashSet<int>? used = null) : base(graph, currentVertex)
    {
        Used = used ?? new HashSet<int>();
    }
}