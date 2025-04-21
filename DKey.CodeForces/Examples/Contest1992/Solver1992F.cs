using DKey.Algorithms;
using DKey.Algorithms.NumberTheory;

namespace DKey.CodeForces.Examples.Contest1992;

/// <summary>
/// https://codeforces.com/contest/1997/problem/F
/// </summary>
public class Solver1992F : MultiSolver
{
    
    public Solver1992F() : base( new []{typeof(List<int>), typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var n = seq[0];
        var x = seq[1];
        var data = (List<int>)objects[1];
        var dividers = PrimeArithmetics.GetAllDividers(x).Select(v => (long)v).ToList();
        var dict = dividers.ToDictionary(v => (long)v, v => false);
        var answer = 1;
        dict[1] = true;
        var flag = false;
        for(var i = 0; i < n; i++)
        {
            if (!dividers.Contains(data[i]))
                continue;
            foreach (var div in dict.Where(v => v.Value).Select(v => v.Key * data[i]).ToList())
            {
                if(dividers.Contains(div))
                    dict[div] = true;
                if(div == x)
                    flag = true;
            }
            if (flag)
            {
                dict = dividers.ToDictionary(v => v, v => false);
                dict[1] = true;
                dict[data[i]] = true;
                answer++;
                flag = false;
            }
        }
        output.AddLine(answer);
    }
}