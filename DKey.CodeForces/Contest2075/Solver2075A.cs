using DKey.Algorithms;

namespace DKey.CodeForces.Contest2075;


/// <summary>
/// https://codeforces.com/contest/2075/problem/A
/// </summary>
public class Solver2075A : MultiSolver
{
    public Solver2075A() : base( new []{typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var n = seq[0];
        var k = seq[1];
        var op = 1;
        var data = n - k;
        if (data % 2 == 1)
            data++;
        if (k % 2 == 1)
        {
            k--;
        }

        op += (data + k - 1) / k;
        output.AddLine(op);
    }
}