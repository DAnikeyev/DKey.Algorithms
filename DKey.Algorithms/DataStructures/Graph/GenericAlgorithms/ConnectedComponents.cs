using DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

namespace DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;

/// <summary>
/// Connected-component decomposition of an undirected graph.
/// </summary>
public class ConnectedComponents
{
    /// <summary>
    /// Finds the connected components of the graph, each returned as the list
    /// of its vertex indices. Complexity: O(V + E).
    /// </summary>
    public static List<List<int>> Get(List<int>[] graph)
    {
        List<List<int>> components = new();

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
}
