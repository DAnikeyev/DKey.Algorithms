using DKey.Algorithms;

namespace DKey.CodeForces.Contest2063;


/// <summary>
/// https://codeforces.com/contest/2063/problem/A
/// </summary>
public class Solver2063A : MultiSolver
{
    public Solver2063A() : base( new []{typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var a = seq[0];
        var b = seq[1];
        if(b == a && b == 1)
            output.AddLine(1);
        else
        {
            output.AddLine(b - a);
        }
    }
}