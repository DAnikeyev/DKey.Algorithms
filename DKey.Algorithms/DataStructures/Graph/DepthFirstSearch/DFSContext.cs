namespace DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

public class DFSContext : GraphContext
{
    public Stack<int> Parents = new();
    public HashSet<int> Used;
    public bool stopFlag;
    public int Depth => Parents.Count;
    public DFSContext(List<int>[] graph, int currentVertex, HashSet<int>? used = null) : base(graph, currentVertex)
    {
        Used = used ?? new HashSet<int>();
    }
}