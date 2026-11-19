using DKey.Algorithms.DataStructures.Graph.BreadthFirstSearch;

namespace DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;

/// <summary>
/// Shortest-path queries over unweighted graphs (BFS-based; every edge
/// costs 1).
/// </summary>
public class ShortestPath
{
    /// <summary>
    /// Shortest path between vertices u and v, returned as the vertex list
    /// from u to v inclusive.
    /// Returns null if there is no path. Complexity: O(V + E).
    /// </summary>
    public static List<int>? Get(List<int>[] Graph, int u, int v)
    {
        var context = new TraverseContext(Graph, v);
        BFS.Traverse(context, bfsContext => ShortestPathProcess(bfsContext, u));
        if (!context.stopFlag)
            return null;
        var path = new List<int>() { u };
        while (path.Last() != v)
            path.Add(context.VertexInfo[path.Last()].parent);
        return path;
    }
    private static void ShortestPathProcess(TraverseContext context, int destination)
    {
        if (context.CurrentVertex == destination)
            context.stopFlag = true;
    }
}
