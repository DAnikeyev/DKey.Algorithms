namespace DKey.Algorithms.DataStructures.Graph.BreadthFirstSearch;

public class BFSContext : GraphContext
{
    public Dictionary<int, (int parent, int depth)> VertexInfo = new();
    public HashSet<int> Used;
    public bool stopFlag;
    public int Depth => VertexInfo[CurrentVertex].depth;
    public int Parent => VertexInfo[CurrentVertex].parent;
    public BFSContext(List<int>[] graph, int currentVertex, HashSet<int>? used = null) : base(graph, currentVertex)
    {
        Used = used ?? new HashSet<int>();
    }
}