using DKey.Algorithms;

namespace DKey.CodeForces.Contest1955;

public class Solver1955C : MultiSolver
{
    public Solver1955C() : base( new Type[]{typeof(List<long>), typeof(List<long>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq1 = (List<long>) objects[0];
        var n = seq1[0];
        var k = seq1[1];
        var seq = (List<long>) objects[1];
        int lindex = 0;
        int rindex = (int)n - 1;
        while (k > 0)
        {
            if (lindex >= rindex)
                break;
            var turns = Math.Min(seq[lindex], seq[rindex]);
            var delta = Math.Min(turns*2, k);
            seq[lindex] -= (delta+1)/2;
            seq[rindex] -= delta / 2;
            if (seq[lindex] == 0)
                lindex++;
            if (seq[rindex] == 0)
                rindex--;
            k -= delta;
        }

        if (lindex == rindex && k >= seq[lindex])
            rindex--;
        output.AddLine(Math.Max(0, n - (rindex - lindex + 1)));
    }
    
}