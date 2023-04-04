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
        var G = GraphBuilder.Unordered(edges, V, false);
        var tree = TreeGraph.Build(G, 5, 1);
        CollectionAssert.AreEqual(new List<int>(){2,1,0,1,0}, tree.Vertices.Select(x => x.Children!.Count).ToList());
    }
    
    [Test]
    public void T02_Build_GivenTreeGraph_ReturnsTreeGraph()
    {
        var graph = new List<int>[]
        {
            new List<int> { 1, 2 },
            new List<int> { 3, 4 },
            new List<int>(),
            new List<int>(),
            new List<int>(),
        };
        var root = 0;

        var tree = TreeGraph.Build(graph, 5, root);

        Assert.That(tree.Root, Is.EqualTo(root));
        Assert.That(tree.VerticesCount, Is.EqualTo(5));
        Assert.That(tree.Vertices, Has.Length.EqualTo(5));
        Assert.That(tree.Vertices[root].ParentIndex, Is.EqualTo(-1));
        Assert.That(tree.Vertices[1].ParentIndex, Is.EqualTo(0));
        Assert.That(tree.Vertices[2].ParentIndex, Is.EqualTo(0));
        Assert.That(tree.Vertices[3].ParentIndex, Is.EqualTo(1));
        Assert.That(tree.Vertices[4].ParentIndex, Is.EqualTo(1));
        Assert.That(tree.Vertices[root].Children, Has.Count.EqualTo(2));
        Assert.That(tree.Vertices[root].Children, Has.Member(1));
        Assert.That(tree.Vertices[root].Children, Has.Member(2));
        Assert.That(tree.Vertices[1].Children, Has.Count.EqualTo(2));
        Assert.That(tree.Vertices[1].Children, Has.Member(3));
        Assert.That(tree.Vertices[1].Children, Has.Member(4));
        Assert.That(tree.Vertices[2].Children, Has.Count.EqualTo(0));
        Assert.That(tree.Vertices[3].Children, Has.Count.EqualTo(0));
        Assert.That(tree.Vertices[4].Children, Has.Count.EqualTo(0));
    }
}