using DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;

namespace DKey.Algorithms.Tests.Graph;

[TestFixture]
public class TreeDiameterTests
{
    [Test]
    public void TreeDiameter_LinearTree_ReturnsCorrectDiameter()
    {
        var graph = new[]
        {
            new List<int> { 1 },
            new List<int> { 0, 2 },
            new List<int> { 1, 3 },
            new List<int> { 2 }
        };

        var expectedDiameter = (3, 0, 3);

        var diameter = TreeDiameter.Get(graph);

        Assert.AreEqual(expectedDiameter, diameter);
    }

    [Test]
    public void TreeDiameter_StarShapeTree_ReturnsCorrectDiameter()
    {
        var graph = new[]
        {
            new List<int> { 1, 2, 3 },
            new List<int> { 0 },
            new List<int> { 0 },
            new List<int> { 0 }
        };

        var expectedDiameter = (3, 2, 2);

        var diameter = TreeDiameter.Get(graph);

        Assert.AreEqual(expectedDiameter, diameter);
    }

    [Test]
    public void TreeDiameter_ComplexTree_ReturnsCorrectDiameter()
    {
        var graph = new[]
        {
            new List<int> { 1, 2 },
            new List<int> { 0, 3, 4 },
            new List<int> { 0, 5, 6 },
            new List<int> { 1 },
            new List<int> { 1 },
            new List<int> { 2 },
            new List<int> { 2, 7 },
            new List<int> { 6 }
        };

        var expectedDiameter = (7, 4, 5);

        var diameter = TreeDiameter.Get(graph);

        Assert.AreEqual(expectedDiameter, diameter);
    }
}