using DKey.Algorithms;

namespace DKey.CodeForces.Contest1112;


/// <summary>
/// https://codeforces.com/contest/1112/problem/H
/// </summary>
public class Solver1112H : MultiSolver
{
    public Solver1112H() : base( new []{typeof(List<int>)})
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