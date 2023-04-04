namespace DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

public class DFSContext : TraverseContext
{
    public Stack<int> Parents = new();
    
    public int TraverseDepth => Parents.Count;
    
    public int TraverseParent => Parents.Peek();

    public DFSContext(List<int>[] graph, int currentVertex, HashSet<int>? used = null) : base(graph, currentVertex, used)
    {
    }
}