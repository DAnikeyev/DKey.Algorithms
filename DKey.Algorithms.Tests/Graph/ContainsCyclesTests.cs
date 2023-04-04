using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;

namespace DKey.Algorithms.Tests.Graph;

public class ContainsCyclesTests
{
    [Test]
    public void T01_ContainsCycle_True()
    {
        var graph = new List<int>[]
        {
            new List<int> {1, 2},
            new List<int> {0, 2},
            new List<int> {0, 1, 3},
            new List<int> {2}
        };

        Assert.IsTrue(Cycle.Exists(graph));
    }

    [Test]
    public void T02_ContainsCycle_False()
    {
        var graph = new List<int>[]
        {
            new List<int> {1},
            new List<int> {0, 2},
            new List<int> {1},
            new List<int> {4},
            new List<int> {3}
        };

        Assert.IsFalse(Cycle.Exists(graph));
    }
}