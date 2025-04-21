using DKey.Algorithms;

namespace DKey.CodeForces.Contest2103;


/// <summary>
/// https://codeforces.com/contest/2103/problem/B
/// </summary>
public class Solver2103A : MultiSolver
{
    public Solver2103A() : base( new Type[]
    {
        typeof(List<int>),
        typeof(List<int>),
    })
    {
    }

    public override void Solve(object[] objects)
    {
        var predata = (List<int>) objects[0];
        var n = predata[0];
        var data = (List<int>) objects[1];
        var res = data.Distinct().Count();
        output.AddLine(res);
    }
}