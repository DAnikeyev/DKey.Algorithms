using DKey.Algorithms;

namespace DKey.CodeForces.Contest1955;

public class Solver1955F : MultiSolver
{
    public Solver1955F() : base( new Type[]{typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var a = seq[0];
        var b = seq[1];
        var c = seq[2];
        var d = seq[3];
        var ans = 0;
        ans += a / 2;
        ans += b / 2;
        ans += c / 2;
        ans += d / 2;
        a = a % 2;
        b = b % 2;
        c = c % 2;
        d = d % 2;
        if(a == b && b == c && c == 1)
            ans++;
        output.AddLine(ans);
    }
}