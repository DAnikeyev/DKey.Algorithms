using DKey.Algorithms.DataStructures.Graph;

namespace DKey.Algorithms.Tests.Graph;

public class ShortestPathTests
{
    
    [Test]
    public void T01_ShortestPath_UnweightedGraph_ReturnsCorrectPath()
    {
        var graph = new List<int>[]
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

        var path = GenericGraphAlgorithms.ShortestPath(graph, startVertex, endVertex);

        Assert.IsNotNull(path);
        Assert.AreEqual(expectedPath, path);
    }

    [Test]
    public void T02_ShortestPath_UnconnectedVertices_ReturnsNull()
    {
        var graph = new List<int>[]
        {
            new List<int> { 1 },
            new List<int> { 0 },
            new List<int> { 3 },
            new List<int> { 2, 4 },
            new List<int> { 3 }
        };

        var startVertex = 0;
        var endVertex = 4;

        var path = GenericGraphAlgorithms.ShortestPath(graph, startVertex, endVertex);

        Assert.IsNull(path);
    }

    [Test]
    public void T03_ShortestPath_SingleVertex_ReturnsPathWithOneVertex()
    {
        var graph = new List<int>[]
        {
            new List<int> { }
        };

        var startVertex = 0;
        var endVertex = 0;
        var expectedPath = new List<int> { 0 };

        var path = GenericGraphAlgorithms.ShortestPath(graph, startVertex, endVertex);

        Assert.IsNotNull(path);
        Assert.AreEqual(expectedPath, path);
    }
}