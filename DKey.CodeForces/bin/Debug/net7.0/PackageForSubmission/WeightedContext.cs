namespace DKey.Algorithms.DataStructures.Graph;
public class WeightedContext : GraphContext
{
    public int[] Weights;
    
    public WeightedContext(List<int>[] graph, int n, HashSet<int> used, int currentVertex, int[] weights) : base(graph, n, used, currentVertex)
    {
        Weights = weights;
    }
}