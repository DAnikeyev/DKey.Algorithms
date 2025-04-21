using DKey.Algorithms.DataStructures.Graph;

namespace DKey.Algorithms.Tests.Graph;

[TestFixture]
public class GraphBuilderTests
{
    [Test]
    public void BuildGraph_ExpectedAdjacency()
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
        var G = GraphBuilder.Undirected(edges, V, false);
        Assert.IsTrue(G[0].Count == 3);
        Assert.IsTrue(G[0].Contains(4));;
        Assert.IsTrue(!G[0].Contains(2));
    }
}