using DKey.Algorithms.DataStructures.SegmentTree;

namespace DKey.Algorithms.Tests.Graph_likeStructures;

[TestFixture]
public class IntegerIntervalTreeTests
{
    [Test]
    public void BasicIntervalAdditionAndQuery_ReturnsExpectedSum()
    {
        IList<int> data = new List<int> { 1, 2, 3, 4, 5 };
        IntegerSegmentTree tree = new IntegerSegmentTree();
        tree.InitFromIntList(data);

        tree.AddToInterval(1, 3, 2);
        SegmentTreeNode sum = tree.GetCumulativeOperation(1, 4);
        Assert.AreEqual(20, sum.Value);
    }
}