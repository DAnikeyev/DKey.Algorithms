namespace DKey.Algorithms.DataStructures.Graph;

public class GraphContext : GraphContextBase
{
    public Stack<int> Parents;
    
    public GraphContext(List<int>[] graph, int n, HashSet<int> used, int currentVertex) : base(graph, n, used, currentVertex)
    {
        Parents = new Stack<int>();
    }

    public override void Process()
    {
    }
}