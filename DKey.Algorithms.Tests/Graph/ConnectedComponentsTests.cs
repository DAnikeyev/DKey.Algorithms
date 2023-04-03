using DKey.Algorithms.DataStructures.Graph;

namespace DKey.Algorithms.Tests.Graph;

public class ConnectedComponentsTests
{
    [Test]
    public void T01_ConnectedComponents()
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

        var result = GenericGraphAlgorithms.ConnectedComponents(graph);
        Assert.AreEqual(expectedComponents.Count, result.Count);
        
        for (var i = 0; i < expectedComponents.Count; i++)
        {
            CollectionAssert.AreEquivalent(expectedComponents[i], result[i]);
        }
    }
}