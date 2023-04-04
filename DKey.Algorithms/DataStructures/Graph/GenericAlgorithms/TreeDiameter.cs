using DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

namespace DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;

public class TreeDiameter
{
    /// <summary>
    /// Find the diameter of the tree
    /// </summary>
    public static (int v1, int v2, int diameterLength) Get(List<int>[] Graph)
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
}