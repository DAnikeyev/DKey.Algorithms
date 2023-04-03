using DKey.Algorithms;
using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

namespace DKey.CodeForces.Contest1805;

/// <summary>
/// https://codeforces.com/contest/1805/problem/D 
/// Use ContestSubmissionBuilder to generate submission as a single file.
/// </summary>
public class Solver1805D : Solver
{
    public int FurthestIndex1;
    public int FurthestDepth1;
    public int FurthestIndex2;
    public Solver1805D() : base( new []{typeof(int)})
    {
    }

    public override void Solve(object[] objects)
    {
        //Build a Graph from edges.
        var n = (int) objects[0];
        var edges = IOHelper.Read2dList(n-1).Select(x => (x[0] - 1, x[1] - 1)).ToList();
        var G = DataConverter.BuildNeighboursList(edges, n);
        
        // Find the diameter of the tree. In tree it's always furthest vertex from the furthest vertex from any vertex.
        var context0 = new DFSContext(G,  0);
        DFS.Iterative(context0, x => (FurthestIndex1, FurthestDepth1) = UpdateBest(x.CurrentVertex, x.Depth, FurthestIndex1, FurthestDepth1));
        var context1 = new DFSContext(G, FurthestIndex1);
        
        //Find the distance of each vertex from the ends of the diameter.
        var depth1 = new int[n];
        var depth2 = new int[n];
        DFS.Iterative(context1, x => depth1[x.CurrentVertex] = x.Depth);
        FurthestIndex2 = Array.IndexOf(depth1, depth1.Max());
        var context2 = new DFSContext(G,  FurthestIndex2);
        DFS.Iterative(context2, x => depth2[x.CurrentVertex] = x.Depth);
        
        //Each vertex is disjoint from the diameter and creates a new component when k > depth1 and k > depth2
        var depthV = new int[n];
        for (var i = 0; i < n; i++)
        {
            depthV[i] = Math.Max(depth1[i], depth2[i]);
        }
        
        //Sorted by at which moment the vertex is disjoint from the diameter. This let's us aggregate the answers fast.
        var time = depthV.OrderBy(x => x).ToList();
        var answer = 0;
        var answers = new List<int>();
        for (var i = 1; i <= n; i++)
        {
            while (answer < time.Count && time[answer] < i)
            {
                answer++;
            }
            answers.Add(Math.Min(answer + 1, n));
        }
        output.AddListLine(answers);
    }

    //Update the best vertex and depth.
    public (int, int) UpdateBest(int index, int depth, int curIndex, int curDepth)
    {
        return curDepth >= depth ? (curIndex, curDepth) : (index, depth);
    }
}