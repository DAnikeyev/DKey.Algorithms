using DKey.Algorithms.DataStructures.DisjointSet;
using DKey.Algorithms.DataStructures.Heap;

namespace DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;

/// <summary>
/// Minimum spanning tree of an undirected weighted graph, by edge list:
/// Kruskal (sort + union-find) and Prim (indexed heap). Both return the total
/// weight and the chosen edges; null when the graph is disconnected (no
/// spanning tree exists) — the library's no-answer convention.
/// </summary>
public static class MinimumSpanningTree
{
    /// <summary>
    /// A spanning-tree edge: endpoints as given in the input plus its weight.
    /// </summary>
    public readonly record struct Edge(int From, int To, int Weight);

    /// <summary>
    /// Kruskal's algorithm: edges sorted by weight, joined greedily through
    /// <see cref="UnionFind"/>. Self-loops are skipped (their union is a
    /// no-op); among parallel edges the cheapest wins via sorting.
    /// Complexity: O(E log E) time, O(V + E) memory.
    /// </summary>
    /// <param name="vertexCount">Vertices are 0..vertexCount-1; 0 and 1 give
    /// an empty tree of weight 0.</param>
    /// <param name="edges">Undirected weighted edges.</param>
    /// <returns>The MST (total weight + edges), or null when
    /// disconnected.</returns>
    public static (long Weight, List<Edge> Edges)? Kruskal(int vertexCount, IEnumerable<(int U, int V, int Weight)> edges)
    {
        var sorted = edges.Select(e => new Edge(e.U, e.V, e.Weight)).ToList();
        sorted.Sort((a, b) => a.Weight.CompareTo(b.Weight));

        var unionFind = new UnionFind(vertexCount);
        var tree = new List<Edge>(Math.Max(0, vertexCount - 1));
        long total = 0;
        foreach (var edge in sorted)
        {
            if (tree.Count == vertexCount - 1)
            {
                break;
            }

            if (unionFind.Union(edge.From, edge.To))
            {
                tree.Add(edge);
                total += edge.Weight;
            }
        }

        return tree.Count == Math.Max(0, vertexCount - 1) ? (total, tree) : null;
    }

    /// <summary>
    /// Prim's algorithm from vertex 0 over an adjacency built from the edge
    /// list, using the handle-tracked <see cref="BinaryHeap{T}"/> as the
    /// priority queue (keys decrease via
    /// <see cref="BinaryHeap{T}.TryDecreaseKey"/>). Self-loops never improve a
    /// key; among parallel edges the cheapest relaxation survives.
    /// Complexity: O((V + E) log V) time, O(V + E) memory.
    /// </summary>
    /// <param name="vertexCount">Vertices are 0..vertexCount-1; 0 and 1 give
    /// an empty tree of weight 0.</param>
    /// <param name="edges">Undirected weighted edges.</param>
    /// <returns>The MST (total weight + edges), or null when
    /// disconnected.</returns>
    public static (long Weight, List<Edge> Edges)? Prim(int vertexCount, IEnumerable<(int U, int V, int Weight)> edges)
    {
        if (vertexCount <= 1)
        {
            return (0, new List<Edge>());
        }

        var adjacency = new List<(int To, int Weight)>[vertexCount];
        for (var i = 0; i < vertexCount; i++)
        {
            adjacency[i] = new List<(int, int)>();
        }

        foreach (var (u, v, weight) in edges)
        {
            adjacency[u].Add((v, weight));
            adjacency[v].Add((u, weight));
        }

        var comparer = Comparer<(long Key, int Vertex)>.Create((a, b) =>
        {
            var result = a.Key.CompareTo(b.Key);
            return result == 0 ? a.Vertex.CompareTo(b.Vertex) : result;
        });
        var heap = new BinaryHeap<(long Key, int Vertex)>(comparer);
        var handles = new int[vertexCount];
        var keys = new long[vertexCount];
        var inTree = new bool[vertexCount];
        var bestEdge = new Edge[vertexCount];
        for (var i = 0; i < vertexCount; i++)
        {
            keys[i] = long.MaxValue;
            handles[i] = heap.Push((long.MaxValue, i));
        }

        heap.TryDecreaseKey(handles[0], (0, 0));

        var tree = new List<Edge>(vertexCount - 1);
        long total = 0;
        while (heap.Count > 0)
        {
            var (key, vertex) = heap.Pop();
            if (key == long.MaxValue)
            {
                return null; // remaining vertices unreachable — disconnected
            }

            inTree[vertex] = true;
            if (vertex != 0)
            {
                tree.Add(bestEdge[vertex]);
                total += key;
            }

            foreach (var (to, weight) in adjacency[vertex])
            {
                if (!inTree[to] && weight < keys[to])
                {
                    keys[to] = weight;
                    bestEdge[to] = new Edge(vertex, to, weight);
                    heap.TryDecreaseKey(handles[to], (weight, to));
                }
            }
        }

        return (total, tree);
    }
}
