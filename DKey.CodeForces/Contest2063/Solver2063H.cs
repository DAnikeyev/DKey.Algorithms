using DKey.Algorithms;

namespace DKey.CodeForces.Contest2063;


/// <summary>
/// https://codeforces.com/contest/2063/problem/H
/// </summary>
public class Solver2063H : MultiSolver
{
    public Solver2063H() : base( new []{typeof(List<int>)})
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