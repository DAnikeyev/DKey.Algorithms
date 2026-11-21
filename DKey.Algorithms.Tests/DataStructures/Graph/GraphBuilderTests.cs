using DKey.Algorithms.DataStructures.Graph;

namespace DKey.Algorithms.Tests.DataStructures.Graph;

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
        ClassicAssert.IsTrue(G[0].Count == 3);
        ClassicAssert.IsTrue(G[0].Contains(4)); ;
        ClassicAssert.IsTrue(!G[0].Contains(2));
    }
}
