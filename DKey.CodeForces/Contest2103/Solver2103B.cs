using DKey.Algorithms;

namespace DKey.CodeForces.Contest2103;


/// <summary>
/// https://codeforces.com/contest/2103/problem/B
/// </summary>
public class Solver2103B : MultiSolver
{
    public Solver2103B() : base( new Type[]
    {
        typeof(List<int>),
        typeof(string),
    })
    {
    }

    public override void Solve(object[] objects)
    {
        var predata = (List<int>) objects[0];
        var n = predata[0];
        var cur = '0';
        var swaps = 0;
        var ones = 0;
        var data = (string) objects[1];
        foreach (var c in data)
        {
            if(c == cur)
            {
                continue;
            }
            if(c == '1')
            {
                ones++;
            }

            swaps++;
            cur = c;
        }

        var ans = data.Length;
        if (ones == 1)
            ans += 1;
        if (ones > 1)
            ans = ans+swaps - 2;
        Console.WriteLine(ans);
    }
}