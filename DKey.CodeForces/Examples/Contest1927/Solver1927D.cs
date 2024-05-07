using DKey.Algorithms;
using DKey.Algorithms.DataStructures.SegmentTree;

public class Solver1927D : MultiSolver
{
    public Solver1927D() : base( new Type[]{typeof(int), typeof(List<long>), typeof(int)})
    {
    }
 
    public override void Solve(object[] objects)
    {
        var seq = (List<long>) objects[1];
        var q = (int) objects[2];
        var dictionary = new Dictionary<long, long>();
        for(var i = 0; i < seq.Count; i++)
            dictionary[i] = seq[i];
        var dictionaryMinNeutral = new Dictionary<long, long>(dictionary);
        dictionaryMinNeutral.Add(dictionaryMinNeutral.Count, long.MaxValue);
        var dictionaryMaxNeutral = new Dictionary<long, long>(dictionary);
        dictionaryMaxNeutral.Add(dictionaryMaxNeutral.Count, long.MinValue);
        var minIndexOpp = new Func<Dictionary<long, long>, long, long, long>((dic, a, b) => dic[a] < dic[b] ? a : b);
        var maxIndexOpp = new Func<Dictionary<long, long>, long, long, long>((dic, a, b) => dic[a] > dic[b] ? a : b);
        var minTree = new IntegerOperationsSegmentTree( minIndexOpp, dictionaryMinNeutral);
        minTree.InitFromIntList(Enumerable.Range(0, seq.Count).ToList());
        var maxTree = new IntegerOperationsSegmentTree( maxIndexOpp, dictionaryMaxNeutral);
        maxTree.InitFromIntList(Enumerable.Range(0, seq.Count).ToList());
        var questions = IOHelper.Read2dList(q);
        foreach (var question in questions)
        {
            var l = question[0] - 1;
            var r = question[1] - 1;
            var minIndex = minTree.GetCumulativeOperation(l, r).Value;
            var maxIndex = maxTree.GetCumulativeOperation(l, r).Value;
            if (dictionary[minIndex] == dictionary[maxIndex])
                output.AddLine($"-1 -1");
            else
                output.AddLine($"{minIndex + 1} {maxIndex + 1}");
        }
    }
}