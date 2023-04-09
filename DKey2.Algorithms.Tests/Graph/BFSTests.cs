using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Graph.BreadthFirstSearch;

namespace DKey.Algorithms.Tests.Graph;

public class BFSTests
{

    [TestFixture]
    public class BreadthFirstSearchTests
    {
        [Test]
        public void T01_Traverse_GivenGraphWithOneEdgeAndStartingVertex_VisitsBothVertices()
        {
            var graph = new List<int>[] {new List<int> {1}, new List<int>()};
            var used = new HashSet<int>();
            var context = new TraverseContext(graph, 0);

            BFS.Traverse(context);

            Assert.That(context.Used, Has.Member(0));
            Assert.That(context.Used, Has.Member(1));
        }

        [Test]
        public void T02_Traverse_GivenGraphWithCycleAndStartingVertex_VisitsAllVertices()
        {
            var graph = new List<int>[] {new List<int> {1, 2}, new List<int> {0, 2}, new List<int> {0, 1}};
            var used = new HashSet<int>();
            var context = new TraverseContext(graph, 0);

            BFS.Traverse(context);

            Assert.That(context.Used, Has.Member(0));
            Assert.That(context.Used, Has.Member(1));
            Assert.That(context.Used, Has.Member(2));
        }

        [Test]
        public void T03_Traverse_GivenGraphWithBranchAndStartingVertex_VisitsAllVertices()
        {
            
            var graph = new List<int>[]
                {new List<int> {1, 2}, new List<int> {3, 4}, new List<int>(), new List<int>(), new List<int>()};
            var used = new HashSet<int>();
            var context = new TraverseContext(graph, 0);

            BFS.Traverse(context);
            
            Assert.That(context.Used, Has.Member(0));
            Assert.That(context.Used, Has.Member(1));
            Assert.That(context.Used, Has.Member(2));
            Assert.That(context.Used, Has.Member(3));
            Assert.That(context.Used, Has.Member(4));
        }

        [Test]
        public void T04_Traverse_GivenGraphAndStartingVertex_CalculatesVertexInfo()
        {
            var graph = new List<int>[]
                {new List<int> {1, 2}, new List<int> {3, 4}, new List<int>(), new List<int>(), new List<int>()};
            var used = new HashSet<int>();
            var context = new TraverseContext(graph, 0);

            BFS.Traverse(context);

            Assert.That(context.VertexInfo[0].parent, Is.EqualTo(-1));
            Assert.That(context.VertexInfo[0].depth, Is.EqualTo(0));
            Assert.That(context.VertexInfo[1].parent, Is.EqualTo(0));
            Assert.That(context.VertexInfo[1].depth, Is.EqualTo(1));
        }
    }
}