namespace DKey.Algorithms.DataStructures.Graph;
public class WeightedContext : GraphContext
{
    public int[] Weights;
    
    public WeightedContext(List<int>[] graph, HashSet<int> used, int currentVertex, int[] weights) : base(graph, used, currentVertex)
    {
        Weights = weights;
    }
}