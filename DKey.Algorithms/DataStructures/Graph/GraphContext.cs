namespace DKey.Algorithms.DataStructures.Graph;

public class GraphContext : GraphContextBase
{
    public int CurrentVertex;

    public GraphContext(List<int>[] graph, int currentVertex) : base(graph)
    {
        CurrentVertex = currentVertex;
    }
}