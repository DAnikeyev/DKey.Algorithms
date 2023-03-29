namespace DKey.Algorithms.DataStructures.Graph;

/// <summary>
/// Compatible with DFS.
/// </summary>
public class GraphContext : GraphContextBase
{
    public Stack<int> Parents;
    public HashSet<int> Used;
    public int CurrentVertex;
    public int Depth => Parents.Count();

    public GraphContext(List<int>[] graph, HashSet<int> used, int currentVertex) : base(graph)
    {
        Parents = new Stack<int>();
        CurrentVertex = currentVertex;
        Used = used;
    }
}