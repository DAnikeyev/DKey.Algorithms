using DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

namespace DKey.Algorithms.Tests.Graph;

public class DFSTests
{
    
    [Test]
    public void Iterative_GivenGraph_ReturnsExpectedTraversal()
    {
        var graph = new List<int>[]
        {
            new List<int> { 1, 2 },
            new List<int> { 3, 4 },
            new List<int>(),
            new List<int>(),
            new List<int>(),
        };
        var context = new DFSContext(graph, new HashSet<int>(), 0);
        var traversal = new List<int>();

        DepthFirstSearch.Iterative(context, c => traversal.Add(c.CurrentVertex));

        Assert.That(traversal, Is.EqualTo(new[] { 0, 1, 3, 4, 2 }));
    }

    [Test]
    public void Recursive_GivenGraph_ReturnsExpectedTraversal()
    {
        var graph = new List<int>[]
        {
            new List<int> { 1, 2 },
            new List<int> { 3, 4 },
            new List<int>(),
            new List<int>(),
            new List<int>(),
        };
        var context = new DFSContext(graph, new HashSet<int>(), 0);
        var traversal = new List<int>();
        DepthFirstSearch.Recursive(context, c => traversal.Add(c.CurrentVertex));
        Assert.That(traversal, Is.EqualTo(new[] { 0, 1, 3, 4, 2 }));
    }
}