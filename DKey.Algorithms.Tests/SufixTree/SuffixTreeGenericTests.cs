using DKey.Algorithms.DataStructures.Graph.SuffixTree;
using DKey.Algorithms.Tests.Graph;

namespace DKey.Algorithms.Tests.SufixTree;

[TestFixture]
public class SuffixTreeGenericTests
{
    [Test]
    public void T01_TestMyValueSuffixTree_Positive()
    {
        var data = new List<MyValue> { new MyValue(1), new MyValue(2), new MyValue(3), new MyValue(6), new MyValue(7), new MyValue(3) };
        var tree = SuffixTree<MyValue>.Build(data, new MyValue(int.MinValue));

        Assert.IsTrue(tree.Contains(new List<MyValue> { new MyValue(1), new MyValue(2), new MyValue(3) }));
        Assert.IsTrue(tree.Contains(new List<MyValue> { new MyValue(2), new MyValue(3), new MyValue(1) }));
        Assert.IsTrue(tree.Contains(new List<MyValue> { new MyValue(3), new MyValue(1), new MyValue(2) }));
        Assert.IsTrue(tree.Contains(new List<MyValue> { new MyValue(1), new MyValue(2) }));
    }

    [Test]
    public void T02_TestMyValueSuffixTree_Negative()
    {
        var data = new List<MyValue> { new MyValue(1), new MyValue(2), new MyValue(3), new MyValue(6), new MyValue(7), new MyValue(3) };
        var tree = SuffixTree<MyValue>.Build(data, new MyValue(0));

        Assert.IsFalse(tree.Contains(new List<MyValue> { new MyValue(1), new MyValue(2), new MyValue(4) }));
        Assert.IsFalse(tree.Contains(new List<MyValue> { new MyValue(2), new MyValue(3), new MyValue(3) }));
        Assert.IsFalse(tree.Contains(new List<MyValue> { new MyValue(3), new MyValue(1), new MyValue(1) }));
        Assert.IsFalse(tree.Contains(new List<MyValue> { new MyValue(4), new MyValue(2) }));
    }
}