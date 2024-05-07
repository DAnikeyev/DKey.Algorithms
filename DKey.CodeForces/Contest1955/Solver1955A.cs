using DKey.Algorithms;

namespace DKey.CodeForces.Contest1955;

public class Solver1955A : MultiSolver
{
    public Solver1955A() : base( new Type[]{typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var n = seq[0];
        var a = seq[1];
        var b = seq[2];
        long sum = 0;
        sum+=n/2*(Math.Min(2*a,b));
        sum+=(n-n/2*2)*a;
        output.AddLine(sum);
    }
}