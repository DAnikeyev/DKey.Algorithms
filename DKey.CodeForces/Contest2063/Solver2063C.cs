using DKey.Algorithms;
using DKey.Algorithms.DataStructures.Graph;

namespace DKey.CodeForces.Contest2063;


/// <summary>
/// https://codeforces.com/contest/2063/problem/C
/// </summary>
public class Solver2063C : MultiSolver
{
    public Solver2063C() : base( new []{typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var n = seq[0];
        var edges = IOHelper.Read2dList(n - 1);
        if(n == 2)
        {
            output.AddLine(0);
            return;
        }
        var graph = GraphBuilder.Undirected(edges.Select(x => (x[0], x[1])).ToList(), n, false);
        var hash = new HashSet<(int,int)>();
        foreach (var l in edges)
        {
            hash.Add((l[0] - 1, l[1] - 1));
            hash.Add((l[1] - 1, l[0] - 1));
        }

        var indexes = Enumerable.Range(0, n);
        var top2 = indexes.OrderByDescending(x => graph[x].Count).Take(2);
        var t1 = graph[top2.First()].Count();
        var t2 = graph[top2.Last()].Count();
        var top1v = indexes.Where(x => t1 == graph[x].Count).ToList();
        var top2v = indexes.Where(x => t2 == graph[x].Count).ToList();
        var bingo = 0;
        if (top1v.Count > 2)
            bingo = 1;
        else
        {
            foreach (var i in top1v)
            {
                foreach (var j in top2v)
                {
                    if (i == j)
                        continue;
                    if (!hash.Contains((i, j)))
                    {
                        bingo = 1;
                        break;
                    }

                }
                if(bingo == 1)
                    break;
            }
        }
        var comp = t1 - 1 + t2 - 1 + bingo;
        output.AddLine(comp);
    }
}