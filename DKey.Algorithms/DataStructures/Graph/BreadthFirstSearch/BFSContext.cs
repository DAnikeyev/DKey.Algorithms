namespace DKey.Algorithms.DataStructures.Graph.BreadthFirstSearch;

public class BFSContext : GraphContext
{
    public Dictionary<int, (int parent, int depth)> VertexInfo = new();
    public HashSet<int> Used;
    public int Depth => VertexInfo[CurrentVertex].depth;
    public int Parent => VertexInfo[CurrentVertex].parent;
    public BFSContext(List<int>[] graph, HashSet<int> used, int currentVertex) : base(graph, currentVertex)
    {
        Used = used;
    }
}