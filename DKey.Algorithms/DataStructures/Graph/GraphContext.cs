namespace DKey.Algorithms.DataStructures.Graph;

public abstract class GraphContext
{
    public List<int>[] Graph;
    public int CurrentVertex;

    public GraphContext(List<int>[] graph, int currentVertex)
    {
        Graph = graph;
        CurrentVertex = currentVertex;
    }
}