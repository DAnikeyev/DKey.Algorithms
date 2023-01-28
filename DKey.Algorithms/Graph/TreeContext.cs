namespace DKey.Algorithms.Graph;

public class TreeContext : GraphContext
{
    public TreeContext(List<int>[] graph, int n, HashSet<int> used, int currentVertex, int parent) : base(graph, n, used, currentVertex, parent)
    {
    }

    public override void Process()
    {
    }
}