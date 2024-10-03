using DKey.Algorithms;
using DKey.Algorithms.NumberTheory;

namespace DKey.CodeForces.Examples.Contest1984;

/// <summary>
/// https://codeforces.com/contest/1984/problem/D
/// </summary>
public class Solver1984D : MultiSolver
{
    public Solver1984D() : base( new Type[]{typeof(string)})
    {
    }

    public override void Solve(object[] objects)
    {
        var data = (string) objects[0];
        var noaindex = new List<int>();
        for (var i = 0; i < data.Length; i++)
        {
            if (data[i] != 'a')
            {
                noaindex.Add(i);
            }
        }

        var length = noaindex.Count;
        if (length == 0) 
        {
            output.AddLine(data.Length - 1);
            return;
        }
        var divisors = PrimeArithmetics.GetAllDividers(length);
        long total = 0;
        foreach (var divisor in divisors)
        {
            var start = noaindex[0];
            var end = noaindex[divisor - 1];
            var candidate = data.Substring(start, end - start + 1);
            total += ProcessCandidate(data, candidate);
        }

        output.AddLine(total);
    }

    private long ProcessCandidate(string data, string candidate)
    {
        var spaces = new List<int>();
        var currentSpace = 0;
        var inside = false;
        var insideDepth = 0;
        for (var i = 0; i < data.Length; i++)
        {
            if (inside && insideDepth == candidate.Length)
            {
                inside = false;
                insideDepth = 0;
            }

            if (!inside && data[i] == 'a')
                currentSpace++;
            if (!inside && data[i] != 'a')
            {
                spaces.Add(currentSpace);
                currentSpace = 0;
                inside = true;
            }

            if (inside && insideDepth != candidate.Length)
            {
                if(data[i]!= candidate[insideDepth])
                    return 0;
                insideDepth++;
            }
        }
        spaces.Add(currentSpace);
        long c = long.MaxValue;
        for(var i = 1; i < spaces.Count - 1; i++)
        {
            c = Math.Min(c, spaces[i]);
        }

        long a = Math.Min(c, spaces[0]);
        long b = Math.Min(c, spaces[^1]);
        c = Math.Min(a + b, c);
        long delta = a + b - c;
        return (a+1) * (b+1) - ((delta)*(delta+1))/2;
    }
}