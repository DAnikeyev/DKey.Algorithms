using DKey.Algorithms;
using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

namespace DKey.CodeForces.Sandbox;

public class SandboxSolver : Solver
{
    public SandboxSolver() : base( new []{typeof(int)})
    {
    }

    public override void Solve(object[] objects)
    {
        var roots = new HashSet<int>();
        var n = (int) objects[0];
        var edges = new List<(int, int)>();
        for (var i = 0; i < n; i++)
        {
            var parent = IOHelper.ReadInt();
            if (parent == -1)
            {
                roots.Add(i);
                continue;
            }

            edges.Add((parent - 1, i));
        }

        var graph = GraphBuilder.Directed(edges, n);

        var maxTraverseDepth = 0;
        foreach (var root in roots)
        {
            var context = new DFSContext(graph.G, root);
            DFS.IterativeWithExitAction(context, null, c =>
            {
                maxTraverseDepth = Math.Max(maxTraverseDepth, c.TraverseDepth);
            });
        }
        output.AddLine(maxTraverseDepth + 1);
    }
}