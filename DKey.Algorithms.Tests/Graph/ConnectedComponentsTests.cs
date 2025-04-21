using DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;

namespace DKey.Algorithms.Tests.Graph;

[TestFixture]
public class ConnectedComponentsTests
{
    [Test]
    public void ConnectedComponents_ReturnsExpectedComponent()
    {
        var graph = new List<int>[]
        {
            new List<int> {1},
            new List<int> {0, 2},
            new List<int> {1},
            new List<int> {4},
            new List<int> {3}
        };

        var expectedComponents = new List<List<int>>
        {
            new List<int> {0, 1, 2},
            new List<int> {3, 4}
        };

        var result = ConnectedComponents.Get(graph);
        Assert.AreEqual(expectedComponents.Count, result.Count);
        
        for (var i = 0; i < expectedComponents.Count; i++)
        {
            CollectionAssert.AreEquivalent(expectedComponents[i], result[i]);
        }
    }
}