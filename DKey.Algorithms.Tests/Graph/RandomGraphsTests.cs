using DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.Graph;

[TestFixture]
public class RandomGraphsTests
{
    [Test]
    public void RandomGraphs_GraphMethods_ThrowNoExceptions()
    {
        var graphGenerator = GraphGenerator.Instance(42);
        var graphs = graphGenerator.GetVariousGraphes(10);
        foreach (var graph in graphs)
        {
            var containsCycle = Cycle.Exists(graph);
            var getCycle = Cycle.Find(graph);
            var components = ConnectedComponents.Get(graph);
            var path = ShortestPath.Get(graph, 0, 1);
            var diameter = TreeDiameter.Get(graph);
        }
    }

    [Explicit("Too long to run on CI")]
    [Test]
    public void RandomBigGraphs_GraphMethods_ThrowNoExceptions([Values(3, 5, 10, 25, 100, 1000, 5000)]int value)
    {
        var graphGenerator = GraphGenerator.Instance(42);
        var graphs = graphGenerator.GetVariousGraphes(value);
        foreach (var graph in graphs)
        {
            var containsCycle = Cycle.Exists(graph);
            var getCycle = Cycle.Find(graph);
            var components = ConnectedComponents.Get(graph);
            var path = ShortestPath.Get(graph, 0, 1);
            var diameter = TreeDiameter.Get(graph);
        }
    }
}