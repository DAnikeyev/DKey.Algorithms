using DKey.Algorithms.Graph;

namespace Dkey.Algorithms.Tests.Graph;

public class TreeTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
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
        var G = Helper.BuildNeighboursList(edges, V, false);
        var tree = Tree.Build(G, 5, 1);
        CollectionAssert.AreEqual(new List<int>(){1,2,1,0,0}, tree.Vertices.Select(x => x.children.Count()).ToList());
    }
}