using DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

namespace DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;

public class ConnectedComponents
{
    /// <summary>
    /// Finds connected components in graph
    /// </summary>
    public static List<List<int>> Get(List<int>[] graph)
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
}