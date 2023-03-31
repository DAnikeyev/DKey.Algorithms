using DKey.Algorithms.DataStructures.Graph;

namespace DKey.Algorithms.Tests;

public class IntegerSumBinaryTree : BinaryTree<int>
{
    public IntegerSumBinaryTree(IList<int> data) : base(data) { }

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
        var tree = new IntegerSumBinaryTree(data);

        Assert.AreEqual(15, tree.GetSum(0, 4));
        Assert.AreEqual(6, tree.GetSum(0, 2));
        Assert.AreEqual(7, tree.GetSum(2, 3));
        Assert.AreEqual(9, tree.GetSum(3, 4));
    }
}