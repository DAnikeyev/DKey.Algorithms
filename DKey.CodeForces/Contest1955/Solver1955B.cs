using DKey.Algorithms;

namespace DKey.CodeForces.Contest1955;

public class Solver1955B : MultiSolver
{
    public Solver1955B() : base( new Type[]{typeof(List<int>), typeof(List<long>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var preseq = (List<int>) objects[0];
        var n = preseq[0];
        var c = preseq[1];
        var d = preseq[2];
        var seq = (List<long>) objects[1];
        seq.Sort();
        var newList = new List<long> ();
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
                newList.Add( c * i + d * j);
        }
        newList.Sort();
        var data = newList.Select((x, i) => x - seq[i]).ToHashSet();
        output.AddAnswer(data.Count == 1, false);
    }
}