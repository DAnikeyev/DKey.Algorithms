using DKey.Algorithms;
using DKey.Algorithms.Search;

namespace DKey.CodeForces.Contest1112;


/// <summary>
/// https://codeforces.com/contest/1112/problem/E
/// </summary>
public class Solver1112E : MultiSolver
{
    public Solver1112E() : base( new Type []{})
    {
    }

    public override void Solve(object[] objects)
    {
        var a = Console.ReadLine().ToCharArray();
        var b = Console.ReadLine().ToCharArray();
        var c = Console.ReadLine().ToCharArray();
        var result = FitMistakes(a, b, c);
        output.AddLine(result);
    }

    private int FitMistakes(char[] a, char[] b, char[] c)
    {
        var dp = new int[a.Length + 1, b.Length + 1];
        for(var i = 0;  i <= a.Length; i++)
        {
            for(var j = 0; j <= b.Length; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                var left = int.MaxValue / 2;
                var bot = int.MaxValue / 2;
                if(i > 0)
                    left = dp[i - 1, j] + Delta(a[i - 1], c[i+j - 1]);
                if(j > 0)
                    bot = dp[i, j - 1] + Delta(b[j - 1], c[i+j - 1]);
                dp[i, j] = Math.Min(left, bot);
            }
        }
        return dp[a.Length, b.Length];
    }

    private int Delta(char c, char c1)
    {
        return c == c1 ? 0 : 1;
    }
}