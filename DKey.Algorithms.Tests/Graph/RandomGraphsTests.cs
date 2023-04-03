using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.Graph;

public class RandomGraphsTests
{
    [Test]
    public void T01_RandomGraphsReliabilityTest()
    {
        var graphGenerator = GraphGenerator.Instance();
        var graphs = graphGenerator.GetVariousGraphes(10);
        foreach (var graph in graphs)
        {
            var containsCycle = GenericGraphAlgorithms.ContainsCycle(graph);
            var components = GenericGraphAlgorithms.ConnectedComponents(graph);
            var path = GenericGraphAlgorithms.ShortestPath(graph, 0, 1);
            var diameter = GenericGraphAlgorithms.TreeDiameter(graph);
        }
    }
    [Explicit]
    [Test]
    public void T02_RandomGraphs([Values(3, 5, 10, 25, 100, 1000, 5000)]int value)
    {
        var graphGenerator = GraphGenerator.Instance();
        var graphs = graphGenerator.GetVariousGraphes(value);
        foreach (var graph in graphs)
        {
            var containsCycle = GenericGraphAlgorithms.ContainsCycle(graph);
            var components = GenericGraphAlgorithms.ConnectedComponents(graph);
            var path = GenericGraphAlgorithms.ShortestPath(graph, 0, 1);
            var diameter = GenericGraphAlgorithms.TreeDiameter(graph);
        }
    }
}