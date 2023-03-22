namespace DKey.Algorithms.DataStructures.Graph;

public abstract class GraphContextBase
{
    public List<int>[] Graph;
    public int N;
    public HashSet<int> Used;
    public int CurrentVertex;

    protected GraphContextBase(List<int>[] graph, int n, HashSet<int> used, int currentVertex)
    {
        Graph = graph;
        N = n;
        Used = used;
        CurrentVertex = currentVertex;
    }

    public abstract void Process();
}