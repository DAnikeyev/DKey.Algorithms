using DKey.Algorithms.DataStructures.Graph;

namespace DKey.Algorithms.Tests.Graph;

public class TreeTests
{
    [Test]
    public void T01_ChildNumbersTest()
    {
        var V = 5;
        var edges = new List<(int, int)>()
        {
            (1,2),
            (2,3),
            (3,4),
            (4,1),
            (1,5),
        };
        var G = DataConverter.BuildNeighboursList(edges, V, false);
        var tree = TreeGraph.Build(G, 5, 1);
        CollectionAssert.AreEqual(new List<int>(){2,1,0,1,0}, tree.Vertices.Select(x => x.Children!.Count).ToList());
    }
}