namespace DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

/// <summary>
/// Convenience wrapper over <see cref="DFS"/>: collects the traversal into a
/// flat vertex list instead of a per-vertex action.
/// </summary>
public class DFSTools
{
    /// <summary>
    /// Runs an iterative DFS from the context's start vertex and returns the
    /// vertices in visit order; <paramref name="reverse"/> returns the exit
    /// order instead (children before parents). Complexity: O(V + E).
    /// </summary>
    public List<int> Traverse(DFSContext context, bool reverse = false)
    {
        var result = new List<int>();
        DFS.Iterative(context, x =>
        {
            result.Add(x.CurrentVertex);
        });
        if (reverse)
        {
            result.Reverse();
        }
        return result;
    }
}
