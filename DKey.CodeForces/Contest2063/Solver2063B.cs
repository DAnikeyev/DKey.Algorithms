using DKey.Algorithms;

namespace DKey.CodeForces.Contest2063;


/// <summary>
/// https://codeforces.com/contest/2063/problem/B
/// </summary>
public class Solver2063B : MultiSolver
{
    public Solver2063B() : base( new []{typeof(List<int>), typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var n = seq[0];
        var l = seq[1];
        var r = seq[2];
        var data = (List<int>) objects[1];
        var pre = data.Take(l-1).OrderBy(x => x).ToList();
        var inside = data.Skip(l-1).Take(r-l+1).OrderByDescending(x => x).ToList();
        var post = data.Skip(r).OrderBy(x => x).ToList();
        long delta1 = 0;
        var i = 0;
        while (true)
        {
            if(i>=pre.Count || i>=inside.Count)
                break;
            if (pre[i] < inside[i])
            {
                delta1 += inside[i] - pre[i];
            }
            else
            {
                break;
            }

            i++;
        }
        long delta2 = 0;
        i = 0;
        while (true)
        {
            if(i>=post.Count || i>=inside.Count)
                break;
            if (post[i] < inside[i])
            {
                delta2 += inside[i] - post[i];
            }
            else
            {
                break;
            }

            i++;
        }
        output.AddLine(inside.Sum(x => (long)(x)) - Math.Max(delta1,delta2));
    }
}