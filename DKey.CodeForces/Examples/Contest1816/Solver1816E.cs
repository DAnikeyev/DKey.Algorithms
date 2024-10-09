using DKey.Algorithms;
using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Graph.BreadthFirstSearch;

namespace DKey.CodeForces.Examples.Contest1816;

/// <summary>
/// https://codeforces.com/contest/1816/problem/E
/// </summary>
public class Solver1816E : MultiSolver
{
    public Solver1816E() : base( new Type[]{typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var n = seq[0];
        var m = seq[1];
        var edges = IOHelper.Read2dList(m).Select(x => (x[0] - 1, x[1] - 1)).ToList();
        var (forwardGraph, backwardGraph) = GraphBuilder.Directed(edges, n);
        var context = new TraverseContext(backwardGraph, 0);
        var depth = new int[n];
        
        //BFS depth defines how many times sequence can have this number (proved by induction).
        //It's true for 1, and if child has more then d occurencies, its parent has to have at least d-1.
        BFS.Traverse(context, ctx =>depth[ctx.CurrentVertex] = ctx.Depth + 1);
        
        //if no path from 1 to x, then sequence 1 x x x x x x ... is valid.
        if(depth.Any(x => x == 0))
            output.AddLine("INFINITE");
        else
        {
            output.AddLine("FINITE");
            /* Lets construct iteratively string for vertexes up too depth k.
            If a b c are vertexes of depth 1, and d e f are vertexes of depth 2.
            We will have
            1
            abc 1+abc
            def abc+def 1+abc+def
            ...
            To satisfy condition of inclusion for every vertex.            
            Let's construct this in reverse order to avoid moving items in the list.*/
            var vertexByDepth = new List<List<int>>();
            for (var i = 0; i <= n; i++)
                vertexByDepth.Add(new List<int>());

            for (var i = 0; i < n; i++)
                vertexByDepth[depth[i]].Add(i);
            var result = new List<List<int>>();
            for (var i = 1; i <= n; i++)
            {
                foreach (var block in result)
                    block.AddRange(vertexByDepth[i]);

                result.Add(new List<int>(vertexByDepth[i]));
            }
            result.Reverse();
            var answer = result.SelectMany(x => x).Select(x => x+1).ToList();
            output.AddLine(answer.Count);
            output.AddListLine(answer);

        }
    }
}