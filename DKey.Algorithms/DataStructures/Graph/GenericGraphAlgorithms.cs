using DKey.Algorithms.DataStructures.Graph.BreadthFirstSearch;
using DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

namespace DKey.Algorithms.DataStructures.Graph;

public class GenericGraphAlgorithms
{
    /// <summary>
    /// Finds connected components in an undirected graph
    /// </summary>
    public static List<List<int>> ConnectedComponents(List<int>[] graph)
    {
        List<List<int>> components = new ();

        var context = new DFSContext(graph, 0);
        for (int i = 0; i < graph.Length; i++)
        {
            if (!context.Used.Contains(i))
            {
                context.CurrentVertex = i;
                var component = new List<int>();
                DFS.Iterative(context, x => component.Add(x.CurrentVertex));
                components.Add(component);
            }
        }
        return components;
    }
    
    /// <summary>
    /// Detects if there is a cycle in an undirected graph
    /// </summary>
    public static bool ContainsCycle(List<int>[] graph)
    {
        var context = new DFSContext(graph, 0);
        for (int i = 0; i < graph.Length; i++)
        {
            if (!context.Used.Contains(i))
            {
                context.CurrentVertex = i;
                var component = new List<int>();
                DFS.Iterative(context, x => component.Add(x.CurrentVertex));
                if(component.Sum(x => graph[x].Count)!= component.Count*2 - 2)
                    return true;
            }
        }
        return false;
    }
    
    /// <summary>
    /// Find the diameter of the tree
    /// </summary>
    public static (int v1, int v2, int diameterLength) TreeDiameter(List<int>[] Graph)
    {
        int furthestIndex1 = 0;
        int furthestDepth1 = 0;
        int furthestIndex2 = 0;
        int furthestDepth2 = 0;
        // In tree it's always furthest vertex from the furthest vertex from any vertex.
        var context0 = new DFSContext(Graph, 0);
        DFS.Iterative(context0, x => (furthestIndex1, furthestDepth1) = UpdateDeepest(x.CurrentVertex, x.Depth, furthestIndex1, furthestDepth1));
        var context1 = new DFSContext(Graph, furthestIndex1);
        DFS.Iterative(context1, x => (furthestIndex2, furthestDepth2) = UpdateDeepest(x.CurrentVertex, x.Depth, furthestIndex2, furthestDepth2));
        return (furthestIndex1, furthestIndex2, furthestDepth2);
    }

    private static (int furthestIndex1, int furthestDepth1) UpdateDeepest(int curIndex, int curDepth, int index, int depth)
    {
        return curDepth >= depth ? (curIndex, curDepth) : (index, depth);
    }

    /// <summary>
    /// Shortest path between vertices u and v.
    /// Returns null if there is no path.
    /// </summary>
    public static List<int>? ShortestPath(List<int>[] Graph, int u, int v)
    {
        var context = new BFSContext(Graph, v);
        BFS.Traverse(context, bfsContext => ShortestPathProcess(bfsContext, u));
        if (!context.stopFlag)
            return null;
        var path = new List<int>(){u};
        while(path.Last()!= v)
            path.Add(context.VertexInfo[path.Last()].parent);
        return path;
    }

    private static void ShortestPathProcess(BFSContext context, int destination)
    {
        if (context.CurrentVertex == destination)
            context.stopFlag = true;
    }
}