namespace DKey.Algorithms.DataStructures.Graph;

public abstract class GraphContextBase
{
    public List<int>[] Graph;

    protected GraphContextBase(List<int>[] graph)
    {
        Graph = graph;
    }
}