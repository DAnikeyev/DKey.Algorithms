namespace DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

public class DFSContext : GraphContext
{
    public Stack<int> Parents = new();
    public HashSet<int> Used;
    public int Depth => Parents.Count;
    public DFSContext(List<int>[] graph, HashSet<int> used, int currentVertex) : base(graph, currentVertex)
    {
        Used = used;
    }
}