namespace DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

public class DFSTools
{
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