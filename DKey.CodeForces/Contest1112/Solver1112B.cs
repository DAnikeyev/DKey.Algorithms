using DKey.Algorithms;

namespace DKey.CodeForces.Contest1112;


/// <summary>
/// https://codeforces.com/contest/1112/problem/B
/// </summary>
public class Solver1112B : MultiSolver
{
    public Solver1112B() : base( new []{typeof(List<int>), typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var predata = (List<int>) objects[0];
        var data = (List<int>) objects[1];
        if(data.Count == 1)
        {
            output.AddLine("YES");
            return;
        }
        var osum = 0l;
        var esum = 0l;
        var ocount = 0;
        var ecount = 0;
        for (var i = 0; i < data.Count; i += 2)
        {
            osum+=data[i];
            ocount++;
        }
        for (var i = 1; i < data.Count; i += 2)
        {
            esum+=data[i];
            ecount++;
        }
        var oavg = osum / ocount;
        var isGood = (oavg * ocount == osum && oavg * ecount == esum);
        output.AddAnswer(isGood, false);
    }
}