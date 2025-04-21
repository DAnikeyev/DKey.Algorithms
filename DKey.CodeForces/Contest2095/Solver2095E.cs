using DKey.Algorithms;

namespace DKey.CodeForces.Contest2095;


/// <summary>
/// https://codeforces.com/contest/2095/problem/E
/// </summary>
public class Solver2095E : MultiSolver
{
    public Solver2095E() : base( new []{typeof(List<int>)})
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