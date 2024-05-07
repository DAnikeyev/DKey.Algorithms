using DKey.Algorithms.DataStructures.Graph;

namespace DKey.Algorithms.Tests.Graph_likeStructures;

public class IntegerSumBinaryTree : ImmutableBinaryTree<int>
{
    protected override int Add(int a, int b)
    {
        return a + b;
    }

    protected override int Neutral()
    {
        return 0;
    }
}

[TestFixture]
public class BinaryTreeTests
{
    [Test]
    public void GetSumTest()
    {
        var data = new List<int> { 1, 2, 3, 4, 5 };
        var tree = new IntegerSumBinaryTree();
        tree.Init(data);

        Assert.AreEqual(15, tree.GetCumulativeOperation(0, 4));
        Assert.AreEqual(6, tree.GetCumulativeOperation(0, 2));
        Assert.AreEqual(7, tree.GetCumulativeOperation(2, 3));
        Assert.AreEqual(9, tree.GetCumulativeOperation(3, 4));
    }
}