using DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

namespace DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;

public class Cycle
{
    /// <summary>
    /// Detects if there is a cycle in an undirected graph
    /// </summary>
    public static bool Exists(List<int>[] graph)
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
    /// Find any cycle in DFS starting from start.
    /// Returns null if there is no cycle.
    /// </summary>
    public static List<int>? Find(List<int>[] Graph, int start = 0)
    {
        var context = new DFSContext(Graph, start);
        List<int>? answer = null;
        DFS.Iterative(context, dfsContext => ProcessVertex(dfsContext, start, ref answer));
        if (!context.stopFlag)
            return null;
        return answer;
    }

    private static void ProcessVertex(DFSContext context, int start, ref List<int>? answer)
    {
        if(answer != null)
            return;
        var doubleVisited = context.Graph[context.CurrentVertex].FirstOrDefault(x => context.Used.Contains(x) && x != context.TraverseParent, -1);
        if (doubleVisited != -1)
        {   
            context.stopFlag = true;
            answer = new List<int>(){context.CurrentVertex};
            var v = context.CurrentVertex;
            while (v != doubleVisited)
            {
                v = context.VertexInfo[v].parent;
                answer.Add(v);
            }
        }
    }
}