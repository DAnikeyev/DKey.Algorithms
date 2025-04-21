using DKey.Algorithms;
using DKey.Algorithms.NumberTheory;

namespace DKey.CodeForces.Examples.Contest1992;

/// <summary>
/// https://codeforces.com/contest/1997/problem/F
/// </summary>
public class Solver1992F2 : Solver
{

    public Solver1992F2() : base( new Type[]{typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var maxVal = 57000;
        var mod = 998244353;
        var fibonacci = new List<int> {1, 1};
        for(var i = 2; i <= 25; i++)
            fibonacci.Add(fibonacci[i - 1] + fibonacci[i - 2]);
        var optim = Enumerable.Range(0, maxVal).Select(x => OptimalBitsCount(x, fibonacci)).ToList();
        var seq = (List<int>) objects[0];
        var n = seq[0];
        var x = seq[1];
        var m = seq[2];
        var arithmetic = new ModularArithmetics(mod, true, true);
        var dpArray = new int[n + 1, maxVal];
        dpArray[0, 0] = 1;
        for(var i = 0; i < x; i++)
        {
            for (var j = 0; j < n; j++)
            {
                var maxSubVal = j * fibonacci[i];
                for (var sum = 0; sum <= maxSubVal; sum++)
                {
                    var res = dpArray[j + 1, sum + fibonacci[i]] + dpArray[j, sum];
                    if (res >= mod)
                        res -= mod;
                    dpArray[j + 1, sum + fibonacci[i]] = res;
                }
            }
        }
        var answer = 0;
        for (var i = 0; i < maxVal; i++)
        {
            if (OptimalBitsCount(i, fibonacci) == m)
            {
                answer+=dpArray[n, i];
                if (answer >= mod)
                    answer -= mod;
            }
        }

        output.AddLine(answer);
    }

    private int OptimalBitsCount(int val, List<int> fibbonachi)
    {
        var result = 0;
        for(var i = 25; i >= 0; i--)
            if (fibbonachi[i] <= val)
            {
                val-=fibbonachi[i];
                result++;
            }
        return result;
    }
}