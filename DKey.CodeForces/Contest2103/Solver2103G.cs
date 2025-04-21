using DKey.Algorithms;

namespace DKey.CodeForces.Contest2103;


/// <summary>
/// https://codeforces.com/contest/2103/problem/G
/// </summary>
public class Solver2103G : MultiSolver
{
    public Solver2103G() : base( new Type[]
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
        var k = predata[1];
        var data = (List<int>) objects[1];
        output.AddLine(n);
    }
}