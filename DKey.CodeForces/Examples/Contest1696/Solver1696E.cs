using DKey.Algorithms;
using DKey.Algorithms.NumberTheory;

namespace DKey.CodeForces.Contest1696;

/// <summary>
/// https://codeforces.com/contest/1696/problem/E
/// </summary>
public class Solver1696E : Solver
{
    public Solver1696E() : base( new []{typeof(int), typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var data = (List<int>) (objects[1]);
        var mod = new ModularArithmetics(1_000_000_007);
        var res = 0;
        
        //Convolution of required binoms has O(n) terms, instead of O(n^2).
        for (var i = 0; i < data.Count; i++)
            res = mod.Add(res, mod.Choose(i + data[i], i + 1));
        output.AddLine(res);
    }
}