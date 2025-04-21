using DKey.Algorithms;

namespace DKey.CodeForces.Contest1112;


/// <summary>
/// https://codeforces.com/contest/1112/problem/C
/// </summary>
public class Solver1112C : MultiSolver
{
    public Solver1112C() : base( new Type[]{})
    {
    }

    public override void Solve(object[] objects)
    {
        var item = Console.ReadLine();
        var twos = 0;
        var threes = 0;
        var sum = 0l;
        foreach (var digit in item)
        {
            var d = digit - '0';
            sum += d;
            if (d == 2)
                twos++;
            if (d == 3)
                threes++;
        }

        var option = sum % 9;
        var flag = option == 0;
        for (var i = 0; i <= Math.Min(9, twos); i++)
        {
            for(var j = 0; j <= Math.Min(9, threes); j++)
            {
                if((option + 2 * i + 6 * j) % 9 == 0)
                {
                    flag = true;
                    break;
                }
            }

            if (flag)
                break;
        }

        output.AddAnswer(flag, false);
    }
}