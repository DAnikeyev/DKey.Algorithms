using DKey.Algorithms;

namespace DKey.CodeForces.Contest2075;


/// <summary>
/// https://codeforces.com/contest/2075/problem/F
/// </summary>
public class Solver2075F : MultiSolver
{
    public Solver2075F() : base( new []{typeof(List<int>)})
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