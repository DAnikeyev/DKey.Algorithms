using DKey.Algorithms.DataStructures.SegmentTree;

namespace DKey.Algorithms.Tests.DataStructures;

[TestFixture]
public class SegmentTreeTests
{
    
    [Test]
    public void MinOperation_ReturnsCorrectIntervalMin()
    {
        var sequence = new List<int> { 3, 1, 2, 5, 4, 2, 1, 6 };
        var dictionary = new Dictionary<long, long>();
        for(var i = 0; i < sequence.Count; i++)
            dictionary[i] = sequence[i];
        // Need neutral element for the operation.
        var dictionaryMinNeutral = new Dictionary<long, long>(dictionary);
        dictionaryMinNeutral.Add(dictionaryMinNeutral.Count, long.MaxValue);
        
        // Operation is calculated for the indexes of collection. To make it work, we need to provide dictionary with values and comparison function.
        var minIndexOperation = new Func<Dictionary<long, long>, long, long, long>((dic, a, b) => dic[a] < dic[b] ? a : b);
        var minTree = new IntegerOperationsSegmentTree( minIndexOperation, dictionaryMinNeutral);
        minTree.InitFromIntList(Enumerable.Range(0, sequence.Count).ToList());
        var questions = new (int l, int r)[]{(1,4), (2,5), (3,4), (0,7)};
        
        var processed = questions.Select(q => minTree.GetCumulativeOperation(q.l, q.r).Value).ToArray();
        Assert.AreEqual(new long[]{1, 2, 4, 1}, processed.Select(x => dictionary[x]).ToArray());
    }
}