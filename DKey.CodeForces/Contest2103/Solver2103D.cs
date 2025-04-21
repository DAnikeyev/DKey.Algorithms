using DKey.Algorithms;

namespace DKey.CodeForces.Contest2103;


/// <summary>
/// https://codeforces.com/contest/2103/problem/D
/// </summary>
public class Solver2103D : MultiSolver
{
    public Solver2103D() : base( new Type[]
    {
        typeof(List<int>),
        typeof(List<int>),
    })
    {
    }

    public override void Solve(object[] objects)
    {
        var predata = (List<int>) objects[0];
        var n = predata[0];
        var data = (List<int>) objects[1];
        var dict = new Dictionary<int, List<int>>();
        var max = 0;
        for (var i = 0; i < data.Count; i++)
        {
            if(dict.TryGetValue(data[i], out var list))
            {
                list.Add(i);
            }
            else
            {
                dict[data[i]] = new List<int> {i};
            }
            if (data[i] > max)
            {
                max = data[i];
            }
        }
        var answer = new int[data.Count];
        var curmin = 0;
        var curmax = 0;
        var startpos = dict[-1].First();
        for (var i = max; i > 0; i--)
        {
            var stageIsNotMin = i % 2 == 1;
            var elements = dict[i].OrderBy(x => Math.Abs(x - startpos)).ToList();
            foreach (var element in elements)
            {
                if (stageIsNotMin)
                {
                    answer[element] = curmax+1;
                    curmax++;
                }
                else
                {
                    answer[element] = curmin-1;
                    curmin--;
                }
            }
        }

        var delta = answer.Min();
        output.AddListLine(answer.Select(x =>x - delta + 1));
    }
}