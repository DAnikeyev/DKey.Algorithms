using DKey.Algorithms;
using DKey.Algorithms.LinqExtension;

namespace DKey.CodeForces.Contest2103;


/// <summary>
/// https://codeforces.com/contest/2103/problem/A
/// </summary>
public class Solver2103AA : MultiSolver
{
    public Solver2103AA() : base( new Type[]
    {
        typeof(List<int>),
        typeof(List<int>),
        typeof(List<int>),
    })
    {
    }

    public override void Solve(object[] objects)
    {
        var predata = (List<int>) objects[0];
        var n = predata[0];
        var k = predata[1];
        var l = (List<int>) objects[1];
        var r = (List<int>) objects[2];
        var gloves = l.Zip(r).Select(x => TupleHelpler.SelectMax(x)).OrderByDescending(x => x.Second).ToList();
        var add = gloves.Take(k - 1).Select(x =>  x.Second).LongSum();
        var init = gloves.Select(x =>x.First).LongSum();
        output.AddLine(init + add + 1);
    }
}