using DKey.Algorithms;

namespace DKey.CodeForces.Contest2095;


/// <summary>
/// https://codeforces.com/contest/2095/problem/F
/// </summary>
public class Solver2095F : Solver
{
    public Solver2095F() : base( new []{typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var a = seq[0];
        var b = seq[1];
        long answer = 0;
        answer = (long)12 * a;
        answer += (long)14 * a * b;
        answer += Math.Abs(a - b);
        answer += (a - 3 * (long)b) * (long)b;
        answer += 2;
        output.AddLine(answer);
    }
}