using DKey.Algorithms;

namespace DKey.CodeForces.Contest2075;


/// <summary>
/// https://codeforces.com/contest/2075/problem/H
/// </summary>
public class Solver2075H : MultiSolver
{
    public Solver2075H() : base( new []{typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var a = seq[0];
        var b = seq[1];
        output.AddLine(a+b);
    }
}