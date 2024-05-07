using DKey.Algorithms;
using DKey.Algorithms.NumberTheory;

namespace DKey.CodeForces.Contest1955;

public class Solver1955G : MultiSolver
{
    public Solver1955G() : base( new Type[]{typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var n = seq[0];
        var m = seq[1];
        var data = IOHelper.Read2dList(n);
        var res = new HashSet<int>[n, m];

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < m; j++)
            {
                res[i, j] = new HashSet<int>();
            }
        }

        res[0, 0].Add(data[0][0]);
        for(var i = 0; i < n; i++)
        {
            for(var j = 0; j < m; j++)
            {
                var o1 = i-1>=0 ? res[i-1, j] : new HashSet<int>();
                var o2 = j-1>=0 ? res[i, j-1] : new HashSet<int>();
                foreach (var val in o1)
                {
                    res[i,j].Add(IntArithmetics.GCD(val, data[i][j]));
                }
                foreach (var val in o2)
                {
                    res[i,j].Add(IntArithmetics.GCD(val, data[i][j]));
                }
            }
        }
        output.AddLine(res[n-1,m-1].Max());
        
    }
}