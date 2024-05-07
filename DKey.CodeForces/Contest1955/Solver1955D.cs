using DKey.Algorithms;
using DKey.Algorithms.IEnumerableExtensions;

namespace DKey.CodeForces.Contest1955;

public class Solver1955D : MultiSolver
{
    public Solver1955D() : base( new Type[]{typeof(List<int>), typeof(List<int>), typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var n = seq[0];
        var m = seq[1];
        var k = seq[2];
        var a = (List<int>) objects[1];
        var b = (List<int>) objects[2];
        var aCount = a.Take(m).ToArray().ToCountDictionary(x => x);
        var bCount = b.ToCountDictionary(x => x);
        var answ = 0;
        var sum = 0;
        foreach (var bpair in bCount)
        {
            sum+=Math.Min(aCount.GetValueOrDefault(bpair.Key),bpair.Value);
        }
        if(sum>=k)
        {
            answ++;
        }

        for (var i = 1; i + m - 1< n; i++)
        {
            aCount[a[i-1]]--;
            if(Math.Min(aCount.GetValueOrDefault(a[i-1]), bCount.GetValueOrDefault(a[i-1])) < (Math.Min(aCount.GetValueOrDefault(a[i-1]) + 1, bCount.GetValueOrDefault(a[i-1]))))
            {
                sum--;
            }
            aCount.AddToCountDictionary(a[i+m-1], x=>x);
            if(Math.Min(aCount.GetValueOrDefault(a[i+m-1]) - 1, bCount.GetValueOrDefault(a[i+m-1])) < (Math.Min(aCount.GetValueOrDefault(a[i+m-1]), bCount.GetValueOrDefault(a[i+m-1]))))
            {
                sum++;
            }
            answ+=sum>=k?1:0;
        }

        output.AddLine(answ);
    }
}