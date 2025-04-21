namespace DKey.Algorithms.DataStructures.Graph.Misc;

public class Dijkstra
{
    private static readonly Comparer<(long, int)> comparer = Comparer<(long, int)>.Create((a, b) =>
    {
        var result = a.Item1.CompareTo(b.Item1);
        return result == 0 ? a.Item2.CompareTo(b.Item2) : result;
    });
    
    public static long[] ToAll(WeightedGraphContext context)
    {
        var graph = context.Graph;
        var currentVertex = context.CurrentVertex;
        var weights = context.Weights;
        var size = graph.Length;
        var distances = new long[size];
        for (var i = 0; i < size; i++)
        {
            distances[i] = long.MaxValue;
        }

        distances[currentVertex] = 0;
        var queue = new SortedSet<(long, int)>(comparer);
        queue.Add((0, currentVertex));

        while (queue.Count > 0)
        {
            var (value, index) = queue.Min;
            queue.Remove(queue.Min);
            foreach (var neighbour in graph[index])
            {
                var newDistance = distances[index] + weights[(index, neighbour)];
                if (newDistance < distances[neighbour])
                {
                    distances[neighbour] = newDistance;
                    queue.Add((newDistance, neighbour));
                }
            }
        }
        
        return distances;
    }
    
    public static long[] ToAll(GraphContext context)
    {
        var graph = context.Graph;
        var currentVertex = context.CurrentVertex;
        var size = graph.Length;
        var distances = new long[size];
        for (var i = 0; i < size; i++)
        {
            distances[i] = long.MaxValue;
        }

        distances[currentVertex] = 0;
        var queue = new SortedSet<(long, int)>(comparer);
        queue.Add((0, currentVertex));

        while (queue.Count > 0)
        {
            var (value, index) = queue.Min;
            queue.Remove(queue.Min);
            foreach (var neighbour in graph[index])
            {
                var newDistance = distances[index] + 1;
                if (newDistance < distances[neighbour])
                {
                    distances[neighbour] = newDistance;
                    queue.Add((newDistance, neighbour));
                }
            }
        }
        
        return distances;
    }
}