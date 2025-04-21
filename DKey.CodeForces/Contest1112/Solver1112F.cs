using DKey.Algorithms;
using DKey.Algorithms.NumberTheory;

namespace DKey.CodeForces.Contest1112;


/// <summary>
/// https://codeforces.com/contest/1112/problem/F
/// </summary>
public class Solver1112F : MultiSolver
{
    public int[] logTable;
    public long[][] gcdST;

    public Solver1112F() : base( new []{typeof(List<int>), typeof(List<int>)})
    {
    }


    public override void Solve(object[] objects)
    {
        var predata = (List<int>) objects[0];
        var values = (List<int>) objects[1];
        var n = predata[0];
        var q = predata[1];
        var diffs = new long[n - 1];
        for (int i = 0; i + 1 < n; i++)
        {
            diffs[i] = values[i + 1] - values[i];
        }

        BuildLogTable(n);
        Precompute(diffs);
        var queries = IOHelper.Read2dList(q);
        var ans = new List<int>();
        foreach (var question in queries)
        {
            var l = question[0] - 1;
            var r = question[1] - 1;

            if (l == r)
            {
                ans.Add(0);
            }
            else
            {
                ans.Add((int)QueryGCD(l, r - 1));
            }
        }
        output.AddListLine(ans);
    }

    private void BuildLogTable(int n)
    {
        logTable = new int[n + 1];
        for (var i = 2; i <= n; i++)
        {
            logTable[i] = logTable[i / 2] + 1;
        }
    }

    private void Precompute(long[] diffs)
    {
        var n = diffs.Length;
        var kmax = logTable[n];
        gcdST = new long[kmax + 1][];
        for (var k = 0; k <= kmax; k++)
        {
            gcdST[k] = new long[n];
        }

        for (int i = 0; i < n; i++)
        {
            gcdST[0][i] = Math.Abs(diffs[i]);
        }

        var step = 1;
        for (var k = 1; k <= kmax; k++)
        {
            step <<= 1;
            var half = step >> 1;
            for (int i = 0; i + step - 1 < n; i++)
            {
                gcdST[k][i] = IntArithmetics.GCD(gcdST[k - 1][i], gcdST[k - 1][i + half]);
            }
        }
    }

    // Query GCD in diffs range [L..R] inclusive
    private long QueryGCD(int L, int R)
    {
        if (L > R)
        {
            return 0; // Single element => no diffs
        }
        var length = R - L + 1;
        var k = logTable[length];
        var step = 1 << k;
        return IntArithmetics.GCD(gcdST[k][L], gcdST[k][R - step + 1]);
    }
}