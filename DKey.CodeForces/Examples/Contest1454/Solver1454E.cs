using DKey.Algorithms;
using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;
using DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;

namespace DKey.CodeForces.Examples.Contest1454;

/// <summary>
/// https://codeforces.com/contest/1454/problem/E
/// </summary>
public class Solver1454E : MultiSolver
{
    public Solver1454E() : base( new []{typeof(int), typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var n = (int) (objects[0]);
        
        //If path is through the edge of cycle => there is 2 path.
        //Let's take all pairs of vertices and count paths as if path always go through cycle and the substract extra.
        long res = (long)n * (n - 1);
        var graph = GraphBuilder.Undirected(IOHelper.Read2dList(n).Select(x => (x[0], x[1])).ToList(), n, false);
        var cycleSet = Cycle.Find(graph)?.ToHashSet();
        var cycleList = cycleSet?.ToList() ?? new List<int>();
        
        //For every subtree of cycle vertecis paths inside those trees are unique, but we count them twice.
        foreach (var c in cycleList)
        {
            cycleSet.Remove(c);
            long total = 0;
            var context = new DFSContext(graph, c, cycleSet);
            //Use DFS to count vertices in subtree.
            DFS.Iterative(context, x => total++);
            
            //Amount of paths in subtree.
            res -= (total * (total - 1)) / 2;
        }
        output.AddLine(res);
    }
}