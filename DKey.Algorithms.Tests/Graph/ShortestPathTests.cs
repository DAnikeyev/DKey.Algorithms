using DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;

namespace DKey.Algorithms.Tests.Graph;

[TestFixture]
public class ShortestPathTests
{
    
    [Test]
    public void ShortestPath_UnweightedGraph_ReturnsCorrectPath()
    {
        var graph = new[]
        {
            new List<int> { 1, 2 },
            new List<int> { 0, 3 },
            new List<int> { 0, 3 },
            new List<int> { 1, 2, 4 },
            new List<int> { 3 }
        };

        var startVertex = 0;
        var endVertex = 4;
        var expectedPath = new List<int> { 0, 1, 3, 4 };

        var path = ShortestPath.Get(graph, startVertex, endVertex);

        Assert.IsNotNull(path);
        Assert.AreEqual(expectedPath, path);
    }

    [Test]
    public void ShortestPath_UnconnectedVertices_ReturnsNull()
    {
        var graph = new[]
        {
            new List<int> { 1 },
            new List<int> { 0 },
            new List<int> { 3 },
            new List<int> { 2, 4 },
            new List<int> { 3 }
        };

        var startVertex = 0;
        var endVertex = 4;

        var path = ShortestPath.Get(graph, startVertex, endVertex);

        Assert.IsNull(path);
    }

    [Test]
    public void ShortestPath_SingleVertex_ReturnsPathWithOneVertex()
    {
        var graph = new[]
        {
            new List<int> { }
        };

        var startVertex = 0;
        var endVertex = 0;
        var expectedPath = new List<int> { 0 };

        var path = ShortestPath.Get(graph, startVertex, endVertex);

        Assert.IsNotNull(path);
        Assert.AreEqual(expectedPath, path);
    }
}