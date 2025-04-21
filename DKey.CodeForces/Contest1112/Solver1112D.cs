using DKey.Algorithms;

namespace DKey.CodeForces.Contest1112;


/// <summary>
/// https://codeforces.com/contest/1112/problem/D
/// </summary>
public class Solver1112D : MultiSolver
{
    public Solver1112D() : base( new Type[]{})
    {
    }

    public override void Solve(object[] objects)
    {
        var st = Console.ReadLine();
        var digits = ConvertStringToListOfDigits(st);
        for(var i = 0; i < digits.Count; i++)
        {
            var pos = FindBestFit(digits, i);
            for(var j = pos; j > i; j--)
            {
                (digits[j], digits[j - 1]) = (digits[j - 1], digits[j] - 1);
            }
        }
        output.AddLine(ConvertListToString(digits));
    }

    public static string ConvertListToString(List<int> input)
    {
        return string.Join("", input.Select(i => i.ToString()));
    }

    private int FindBestFit(List<int> digits, int i)
    {
        var curpos = i+1;
        var bestpos = i;
        var delta = 0;
        var bestdelta = digits[bestpos];
        while (curpos < digits.Count)
        {
            delta++;
            if(delta > 9)
                break;
            if (digits[curpos] - delta > bestdelta)
            {
                bestpos = curpos;
                bestdelta = digits[curpos] - delta;
            }
            curpos++;
        }

        return bestpos;
    }

    public static List<int> ConvertStringToListOfDigits(string input)
    {
        return input.Select(c => c - '0').ToList();
    }
}