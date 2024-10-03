using DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.Graph;

[TestFixture]
public class CycleTests
{
    
    [Test]
    public void FindACycle_NoCycle()
    {
        var graph = new[]
        {
            new List<int> { 1, 2 },
            new List<int> { 0, 3 },
            new List<int> { 0, 4 },
            new List<int> { 1 },
            new List<int> { 2, 5 },
            new List<int> { 4 }
        };

        var result = Cycle.Find(graph);

        Assert.IsNull(result);
    }
    
    [Test]
    public void FindACycle_CycleExists()
    {
        var graph = new[]
        {
            new List<int> { 1 },
            new List<int> { 2 },
            new List<int> { 3 },
            new List<int> { 4 },
            new List<int> { 1 },
        };

        var result = Cycle.Find(graph);

        Assert.IsNotNull(result);
        CollectionAssert.AreEqual(new List<int> { 4, 3, 2, 1}, result);
    }
    
    [Test]
    public void FindARandomCycle_CycleExists()
    {
        var graph = GraphGenerator.Instance(42).RandomUnicyclic(10);
        var result = Cycle.Find(graph);
        Assert.IsNotNull(result);
    }
}