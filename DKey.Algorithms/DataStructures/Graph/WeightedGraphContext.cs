namespace DKey.Algorithms.DataStructures.Graph;

public class WeightedGraphContext : GraphContext
{
    public Dictionary<(int, int), int> Weights;
    
    public WeightedGraphContext(List<int>[] graph, int currentVertex, Dictionary<(int, int), int> weights) : base(graph, currentVertex)
    {
        Weights = weights;
    }
}