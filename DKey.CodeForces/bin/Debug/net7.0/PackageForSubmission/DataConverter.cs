namespace DKey.Algorithms.DataStructures.Graph;

/// <summary>
/// Basic graph converter.
/// </summary>
public static class DataConverter
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

    /// <summary>
    /// Build forwards and backwards graph.
    /// <summary>
    public static (List<int>[]G, List<int>[] GRev) BuildOrderedNeighboursList(List<(int, int)> edges, int n, bool isSameRepresentation = true)
    {
        var res = new List<int>[n];
        var resRev = new List<int>[n];
        for (var i = 0; i < n; i++)
        {
            res[i] = new List<int>();
            resRev[i] = new List<int>();
        }

        var shift = isSameRepresentation ? 0 : 1;
        foreach (var edge in edges)
        {
            res[edge.Item1 - shift].Add(edge.Item2 - shift);
            resRev[edge.Item2 - shift].Add(edge.Item1 - shift);
        }

        return (res,resRev);
    }
    
    /// <summary>
    /// Graph from representation of tree with root_index = 1, by list of parents for vertxes [2..n].
    /// </summary>
    public static List<int>[] BuildFromTreeIndexation(List<int> parents)
    {
        return BuildNeighboursList(parents.Select((x, i) => (x, i + 2)).ToList(), parents.Count + 1, false, false);
    }
}