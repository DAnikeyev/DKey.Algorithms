using DKey.Algorithms;
using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;
using DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;

namespace DKey.CodeForces.Examples.Contest1805;

/// <summary>
/// https://codeforces.com/contest/1805/problem/D 
/// </summary>
public class Solver1805D : Solver
{
    public Solver1805D() : base( new []{typeof(int)})
    {
    }

    public override void Solve(object[] objects)
    {
        //Build a Graph from edges.
        var n = (int) objects[0];
        var edges = IOHelper.Read2dList(n-1).Select(x => (x[0] - 1, x[1] - 1)).ToList();
        var G = GraphBuilder.Undirected(edges, n);
        
        //Find the distance of each vertex from the ends of the diameter.
        var (v1, v2, length) = TreeDiameter.Get(G);
        var depth1 = new int[n];
        var depth2 = new int[n];
        DFS.Iterative(new DFSContext(G,  v1), x => depth1[x.CurrentVertex] = x.Depth);
        DFS.Iterative(new DFSContext(G,  v2), x => depth2[x.CurrentVertex] = x.Depth);
        
        //Each vertex is disjoint from the diameter and creates a new component when k > depth1 and k > depth2
        var depthV = new int[n];
        for (var i = 0; i < n; i++)
            depthV[i] = Math.Max(depth1[i], depth2[i]);
        
        //Sorted by at which moment the vertex is disjoint from the diameter. This let's us aggregate the answers fast.
        var time = depthV.OrderBy(x => x).ToList();
        var answer = 0;
        var answers = new List<int>();
        for (var i = 1; i <= n; i++)
        {
            while (answer < time.Count && time[answer] < i)
                answer++;
            answers.Add(Math.Min(answer + 1, n));
        }
        output.AddListLine(answers);
    }
}