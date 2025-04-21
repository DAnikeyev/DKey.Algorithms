using DKey.Algorithms;
using DKey.Algorithms.DataStructures.IntervalTree;

namespace DKey.CodeForces.Contest2103;


/// <summary>
/// https://codeforces.com/contest/2103/problem/C
/// </summary>
public class Solver2103C : MultiSolver
{
    public Solver2103C() : base( new Type[]
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
        var ups = data.Select(x => x > k? 1 : -1).ToList();
        var prefixSum = DataSum.PrefixSum(ups);
        var v1 = 0;
        var v2 = prefixSum[^1];
        var cond2 = v2 <= 0;
        var cond3 = prefixSum.Any(x => x >  v2 + 1 || x < v1 - 1);
        var firstDownTouch = 0;
        for(firstDownTouch = 1; firstDownTouch < prefixSum.Count(); firstDownTouch++)
        {
            if(prefixSum[firstDownTouch] == 0)
                break;
        }
        var lastUpTouch = prefixSum.Count() - 1;
        for(lastUpTouch = prefixSum.Count() - 2; lastUpTouch >= 0; lastUpTouch--)
        {
            if(prefixSum[lastUpTouch] == v2)
                break;
        }
        var cond1 = (firstDownTouch!=prefixSum.Count() && lastUpTouch != 0 && firstDownTouch < lastUpTouch);
        var cond4 = prefixSum.Count(x => x == v2) > 2 ||
                    prefixSum.Count(x => x == v1) > 2;
        output.AddAnswer(cond1 || cond2 || cond3 || cond4, false);
    }
}