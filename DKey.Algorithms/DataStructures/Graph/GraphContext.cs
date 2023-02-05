namespace DKey.Algorithms.DataStructures.Graph;

public abstract class GraphContext
{
    public List<int>[] Graph;
    public int N;
    public HashSet<int> Used;
    public int CurrentVertex;
    public LinkedList<int> ParentList;

    protected GraphContext(List<int>[] graph, int n, HashSet<int> used, int currentVertex)
    {
        Graph = graph;
        N = n;
        Used = used;
        CurrentVertex = currentVertex;
        ParentList = new LinkedList<int>();
    }

    public abstract void Process();
}