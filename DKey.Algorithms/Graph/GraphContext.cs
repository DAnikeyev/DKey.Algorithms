namespace DKey.Algorithms.Graph;

public abstract class GraphContext
{
    public List<int>[] Graph;
    public int N;
    public HashSet<int> Used;
    public int CurrentVertex;
    public int Parent;

    protected GraphContext(List<int>[] graph, int n, HashSet<int> used, int currentVertex, int parent)
    {
        Graph = graph;
        N = n;
        Used = used;
        CurrentVertex = currentVertex;
        Parent = parent;
    }

    public abstract void Process();
}