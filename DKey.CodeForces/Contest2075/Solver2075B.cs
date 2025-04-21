using DKey.Algorithms;

namespace DKey.CodeForces.Contest2075;


/// <summary>
/// https://codeforces.com/contest/2075/problem/B
/// </summary>
public class Solver2075B : MultiSolver
{
    public Solver2075B() : base( new []{typeof(List<int>), typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var predata = (List<int>) objects[0];
        var data = (List<int>) objects[1];
        var n = predata[0];
        var k = predata[1];
        if (k > 1)
        {
            data = data.OrderByDescending(x => x).ToList();
            var ans = 0l;
            foreach (var item in data.Take(k + 1))
            {
                ans+=item;
            }
            output.AddLine(ans);
        }
        else
        {
            var o1 = data[0] + data.Skip(1).Max();
            var o2 = data[^1] + data.SkipLast(1).Max();
            output.AddLine(Math.Max(o1, o2));
        }
    }
}