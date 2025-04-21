using DKey.Algorithms;

namespace DKey.CodeForces.Contest1112;


/// <summary>
/// https://codeforces.com/contest/1112/problem/A
/// </summary>
public class Solver1112A : MultiSolver
{
    public Solver1112A() : base( new []{typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var n = seq[0];
        var m = seq[1];
        var total = 0;
        var words = 0;
        for (var i = 0; i < n; i++)
        {
            var  st = Console.ReadLine();
            total += st.Length;
            if (total <= m)
                words = i+1;
        }

        output.AddLine(words);
    }
}