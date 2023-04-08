using DKey.Algorithms.DataStructures.Graph;

namespace DKey.Algorithms.RandomData;

public class GraphGenerator
{

    private static readonly object Padlock = new object();
    private static GraphGenerator? _instance;
    private readonly Random _random;

    private GraphGenerator(int? seed)
    {
        _random = (seed is null) ? new Random() : new Random(seed.Value);
    }

    public static GraphGenerator Instance(int? seed = null)
    {
        lock (Padlock)
        {
            if (_instance is null || seed is not null)
            {
                _instance = new GraphGenerator(seed);
            }

            return _instance;
        }
    }

    /// <summary>
    /// Some of this takes O(n^2) time, be careful.
    /// </summary>
    public List<List<int>[]> GetVariousGraphes(int n)
    {
        return new List<List<int>[]>
        {
            RandomGilbert(n, 0.0),
            RandomGilbert(n, 0.1),
            RandomGilbert(n, 0.5),
            RandomGilbert(n, 0.9),
            RandomGilbert(n, 1),
            RandomErdesRenyi(n, 0),
            RandomErdesRenyi(n, n/2),
            RandomErdesRenyi(n, n*(int)(Math.Max(Double.Log(n), 1))),
            RandomErdesRenyi(n, n),
            RandomErdesRenyi(n, n * n / 3),
            RandomTree(n),
            RandomCycle(n),
            RandomCycle(n),
            RandomTree(n),
            RandomUnicyclic(n),
            RandomUnicyclic(n),
            RandomPartitionGraph(n, 1, 0.5),
            RandomPartitionGraph(n, 2, 0.1),
            RandomPartitionGraph(n, 2, 0.5),
            RandomPartitionGraph(n, 2, 0.9),
            RandomPartitionGraph(n, 2, 1),
            RandomPartitionGraph(n, 3, 0.5),
            RandomPartitionGraph(n, n / 2, 0.5),
            EyeOfTheUniverse,
        };
    }

    /// <summary>
    /// Random graph. Every pair of vertices is connected with probability p.
    /// </summary>
    public List<int>[] RandomGilbert(int n, double probability)
    {

        List<int>[] graph = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            graph[i] = new List<int>();
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (_random.NextDouble() < probability)
                {
                    graph[i].Add(j);
                    graph[j].Add(i);
                }
            }
        }

        return graph;
    }

    /// <summary>
    /// Random graph with fixed number of vertices and edges.
    /// </summary>
    public List<int>[] RandomErdesRenyi(int vertices, int edges)
    {
        var selected_edges = new List<(int, int)>();
        var alledges = vertices * (vertices - 1) / 2;
        var selection = ListGenerator.Instance().RandomKElements(alledges, edges);
        var index = 0;
        for (var i = 0; i < vertices; i++)
        {
            for (var j = i + 1; j < vertices; j++)
            {
                if (selection[index] == 1)
                {
                    selected_edges.Add((i, j));
                }

                index++;
            }
        }

        return GraphBuilder.Unordered(selected_edges, vertices);
    }
    
    public List<int>[] RandomCycle(int n)
    {
        var perm = ListGenerator.Instance().RandomPermutation(n);
        return GraphBuilder.Unordered(perm.Select((x,i) => (x, perm[(i+1)%n])).ToList().ToList(), n);
    }

    public List<int>[] RandomTree(int n)
    {
        List<int>[] graph = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            graph[i] = new List<int>();
        }

        for (int i = 1; i < n; i++)
        {
            int parent = _random.Next(i);
            graph[i].Add(parent);
            graph[parent].Add(i);
        }

        return graph;
    }

    public List<int>[] RandomUnicyclic(int n)
    {
        var tree = RandomTree(n - 1);
        var v1 = _random.Next(n - 1);
        var v2 = v1;
        while(v2 == v1)
            v2 = _random.Next(n - 1);

        Array.Resize(ref tree, n);
        tree[n - 1] = new List<int> {v1, v2};

        tree[v1].Add(n - 1);
        tree[v2].Add(n - 1);

        return tree;
    }

    public List<int>[] RandomPartitionGraph(int n, int parts, double probability)
    {
        var graph = new List<int>[n];
        for (var i = 0; i < n; i++)
        {
            graph[i] = new List<int>();
        }

        var sizes = new int[parts];
        for (var i = 0; i < parts; i++)
        {
            sizes[i] = n / parts;
            if (i < n % parts) sizes[i]++;
        }

        var start = 0;
        for (var i = 0; i < parts; i++)
        {
            var end = start + sizes[i];
            for (var j = start; j < end; j++)
            {
                for (var k = j + 1; k < end; k++)
                {
                    if (_random.NextDouble() < probability)
                    {
                        graph[j].Add(k);
                        graph[k].Add(j);
                    }
                }
            }

            start = end;
        }

        return graph;
    }
    
    public List<int>[] EyeOfTheUniverse => GraphBuilder.Unordered(
        new List<(int, int)>()
        {
            (1,5),
            (5,4),
            (9,6),
            (6,7),
            (7,10),
            (16,17),
            (17,12),
            (12,15),
            (15,14),
            (14,13)
        },
        18);
}