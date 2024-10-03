using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Graph.Misc;

namespace DKey.Algorithms.Tests.Graph;

[TestFixture]
internal class DijkstraTests
{
    [Test]
    public void FindShortestPath_GivenGraphWithOneEdgeAndStartingVertex_ReturnsCorrectPath()
    {
        var graph = new[] { new List<int> { 1 }, new List<int>() };
        var weights = new Dictionary<(int, int), int> { { (0, 1), 3 }, { (1, 0), 3 } };
        var context = new WeightedGraphContext(graph, 0, weights);

        var path = Dijkstra.ToAll(context);

        Assert.That(path, Is.EqualTo(new[] { 0, 3 }));
    }


    [Test]
    [TestCaseSource(nameof(GraphDateCases))]
    public void FindShortestPath_SmallGraph_ReturnsCorrectDistance(
        int vertexCount,
        List<(int u, int v, int weight)> weights,
        List<int> expectedAnswers)
    {
        var graph = GraphBuilder.Undirected(weights.Select(x => (x.u, x.v)).ToList(), vertexCount);
        var weightDict = new Dictionary<(int, int), int>();
        foreach (var (u, v, weight) in weights)
        {
            weightDict[(u, v)] = weight;
            weightDict[(v, u)] = weight;
        }

        var context = new WeightedGraphContext(graph, 0, weightDict);

        var distances = Dijkstra.ToAll(context);

        CollectionAssert.AreEqual(expectedAnswers, distances);

    }

    private static IEnumerable<TestCaseData> GraphDateCases
    {
        get
        {
            yield return new TestCaseData(
                2,
                new List<(int u, int v, int weight)> { (0, 1, 3) },
                new List<int> { 0, 3 }
            );
            yield return new TestCaseData(
                3,
                new List<(int u, int v, int weight)> { (0, 1, 2), (1, 2, 4), (0, 2, 10) },
                new List<int> { 0, 2, 6 }
            );

            yield return new TestCaseData(
                6,
                new List<(int u, int v, int weight)>
                {
                    (0, 1, 1),
                    (1, 4, 1),
                    (4, 3, 1),
                    (3, 2, 1),
                    (2, 5, 1),
                    (1, 2, 5),
                    (0, 2, 8),
                    (3, 5, 2),
                },
                new List<int> { 0, 1, 4, 3, 2, 5 }
            );
        }
    }
}