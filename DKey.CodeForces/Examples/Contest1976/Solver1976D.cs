using DKey.Algorithms;
using DKey.Algorithms.DataStructures.SegmentTree;

namespace DKey.CodeForces.Examples.Contest1976;

/// <summary>
/// https://codeforces.com/contest/1976/problem/D
/// </summary>
public class Solver1976D : MultiSolver
{
    public Solver1976D() : base( new Type[]{typeof(string)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (string) objects[0];
        var values = seq.Select(x => x == '(' ? 1 : -1).ToArray();
        List<List<int>> hdata = new List<List<int>>();
        for (var i = 0; i < values.Length; i++)
        {
            hdata.Add(new List<int>());
        }

        var sum = 0;
        var dictionary = new Dictionary<long, long>();
        for (var i = 0; i < values.Length; i++)
        {
            sum += values[i];
            hdata[sum].Add(i);
            dictionary[i] = sum;
        }

        // Need neutral element for the operation.
        var dictionaryMaxNeutral = new Dictionary<long, long>(dictionary);
        dictionaryMaxNeutral.Add(dictionaryMaxNeutral.Count, long.MinValue);
        
        // Operation is calculated for the indexes of collection. To make it work, we need to provide dictionary with values and comparison function.
        var maxIndexOperation = new Func<Dictionary<long, long>, long, long, long>((dic, a, b) => dic[a] > dic[b] ? a : b);
        var maxTree = new IntegerOperationsSegmentTree( maxIndexOperation, dictionaryMaxNeutral);
        maxTree.InitFromIntList(Enumerable.Range(0, values.Length).ToList());

        long answer = 0;
        for (var i = 1; i <= values.Length / 2; i++)
        {
            var candidates = hdata[i];
            if (candidates.Count < 2)
            {
                continue;
            }

            var prevl = candidates[0];
            var combo = 1;
            for(var j = 1; j < candidates.Count; j++)
            {
                var l = prevl;
                var r = candidates[j];
                var max = dictionary[maxTree.GetCumulativeOperation(l, r).Value];
                if (max <= 2*i)
                {
                    answer+=combo;
                    combo++;
                }
                else
                {
                    combo = 1;
                }
                prevl = r;
            }
        }
        output.AddLine(answer);
    }
}