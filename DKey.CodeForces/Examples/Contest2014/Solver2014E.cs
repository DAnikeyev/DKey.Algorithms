using DKey.Algorithms;
using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Ordered;

namespace DKey.CodeForces.Examples.Contest2014;

/// <summary>
/// https://codeforces.com/contest/2014/problem/E
/// </summary>
public class Solver2014E : MultiSolver
{
    public Solver2014E() : base( new []{typeof(List<int>), typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var predata = (List<int>) objects[0];
        var n = predata[0];
        var m = predata[1];

        var horses = ((List<int>)objects[1]).ToHashSet();
        var edgesWithWeights = IOHelper.Read2dList(m);
        var edges = new List<(int, int)>();
        var weights = new Dictionary<(int, int), int>();
        foreach (var item in edgesWithWeights)
        {
            edges.Add((item[0] - 1, item[1] - 1));
            weights[(item[0] - 1, item[1] - 1)] = item[2];
            weights[(item[1] - 1, item[0] - 1)] = item[2];
        }

        var graph = GraphBuilder.Undirected(edges, n);
        var graphContext0 = new WeightedGraphContext(graph, 0, weights);
        var graphContextn = new WeightedGraphContext(graph, n - 1, weights);
        
        var result0 = DijkstraWithHorses(graphContext0, horses);
        var resultn = DijkstraWithHorses(graphContextn, horses);
        
        var opt = long.MaxValue;
        for (var i = 0; i < n; i++)
        {
            opt = Math.Min(opt, Math.Max(result0[i], resultn[i]));
        }
        output.AddLine(opt == long.MaxValue ? -1 : opt);
    }

    private long[] DijkstraWithHorses(WeightedGraphContext graphContext, HashSet<int> horses)
    {
        var graph = graphContext.Graph;
        var currentVertex = graphContext.CurrentVertex;
        var weights = graphContext.Weights;
        var size = graph.Length;
        var distances = new long[size];
        var horseDistances = new long[size];
        for (var i = 0; i < size; i++)
        {
            distances[i] = long.MaxValue;
            horseDistances[i] = long.MaxValue;
        }

        distances[currentVertex] = 0;
        var visited = new int[size][];
        for (var i = 0; i < size; i++)
        {
            visited[i] = new int[2];
        }

        var comparer = Comparer<(long, int, int)>.Create((a, b) =>
        {
            var result = a.Item1.CompareTo(b.Item1);
            if (result != 0) return result;
            result = a.Item2.CompareTo(b.Item2);
            return result != 0 ? result : a.Item3.CompareTo(b.Item3);
        });
        var queue = new RedBlackTree<(long, int, int)>(comparer);
        queue.TryAdd((0, currentVertex, 0));

        while (queue.Count > 0)
        {
            var (currentDistance, vertex, raid) = queue.Min;
            queue.TryDelete(queue.Min);

            if (visited[vertex][raid] != 0)
                continue;

            visited[vertex][raid] = 1;
            raid |= horses.Contains(vertex + 1) ? 1 : 0;

            foreach (var neighbor in graph[vertex])
            {
                var newDistance = currentDistance + weights[(vertex, neighbor)] * (2 - raid)/2;
                if (raid == 0 && newDistance < distances[neighbor])
                {
                    distances[neighbor] = newDistance;
                    queue.TryAdd((newDistance, neighbor, raid));
                }
                else if (raid == 1 && newDistance < horseDistances[neighbor])
                {
                    horseDistances[neighbor] = newDistance;
                    queue.TryAdd((newDistance, neighbor, raid));
                }
            }
        }

        var result = new long[size];
        for (var i = 0; i < size; i++)
        {
            result[i] = Math.Min(distances[i], horseDistances[i]);
        }

        return result;
    }
}