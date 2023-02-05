namespace DKey.Algorithms.DataStructures.Graph;

/// <summary>
/// Basic graph actions.
/// </summary>
public static class Helper
{
    
    /// <summary>
    /// Adjacent list to per-vertex list converter;
    /// </summary>
    /// <param name="edges">List of adjacent vertexes in [0..n-1] or [1..n] representation.</param>
    /// <param name="n">Number of vertexes.</param>
    /// <param name="isSameRepresentation">Are vertexes in edges from 0 to n-1.</param>
    /// <returns>List of Neigbours for every vertex in [0..n - 1] vertex representation.</returns>
    public static List<int>[] BuildNeighboursList(List<(int, int)> edges, int n, bool isSameRepresentation = true, bool ordered = false)
    {
        var res = new List<int>[n];
        for (var i = 0; i < n; i++)
        {
            res[i] = new List<int>();
        }

        var shift = isSameRepresentation ? 0 : 1;
        foreach (var edge in edges)
        {
            res[edge.Item1 - shift].Add(edge.Item2 - shift);
            res[edge.Item2 - shift].Add(edge.Item1 - shift);
        }

        if (ordered)
        {
            for(var i = 0; i < n; i++)
                res[i] = res[i].OrderBy(x => x).ToList();
        }

        return res;
    }

    public static void DFS(GraphContext context, Action<GraphContext>? action = default)
    {
        if (context.Used.Contains(context.CurrentVertex))
            return;
        action?.Invoke(context);
        context.Used.Add(context.CurrentVertex);
        context.ParentList.AddLast(context.CurrentVertex);
        foreach (var adjacent in context.Graph[context.CurrentVertex])
        {
            context.CurrentVertex = adjacent;
            DFS(context, action);
        }
        context.ParentList.RemoveLast();
    }

}