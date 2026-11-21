using DKey.Algorithms.DataStructures.Graph.SuffixTree;

namespace DKey.Algorithms.Tests.SuffixTree;

[TestFixture]
public class SuffixTreeGenericTests
{
    [Test]
    public void TestMyValueSuffixTree_PositiveContainsCheck()
    {
        var data = new List<MyValue> { new MyValue(1), new MyValue(2), new MyValue(3), new MyValue(6), new MyValue(7), new MyValue(3) };
        var tree = SuffixTree<MyValue>.Build(data, new MyValue(int.MinValue));

        ClassicAssert.IsTrue(tree.Contains(new List<MyValue> { new MyValue(1), new MyValue(2), new MyValue(3) }));
        ClassicAssert.IsTrue(tree.Contains(new List<MyValue> { new MyValue(2), new MyValue(3), new MyValue(1) }));
        ClassicAssert.IsTrue(tree.Contains(new List<MyValue> { new MyValue(3), new MyValue(1), new MyValue(2) }));
        ClassicAssert.IsTrue(tree.Contains(new List<MyValue> { new MyValue(1), new MyValue(2) }));
    }

    [Test]
    public void TestMyValueSuffixTree_NegativeContainsCheck()
    {
        var data = new List<MyValue> { new MyValue(1), new MyValue(2), new MyValue(3), new MyValue(6), new MyValue(7), new MyValue(3) };
        var tree = SuffixTree<MyValue>.Build(data, new MyValue(0));

        ClassicAssert.IsFalse(tree.Contains(new List<MyValue> { new MyValue(1), new MyValue(2), new MyValue(4) }));
        ClassicAssert.IsFalse(tree.Contains(new List<MyValue> { new MyValue(2), new MyValue(3), new MyValue(3) }));
        ClassicAssert.IsFalse(tree.Contains(new List<MyValue> { new MyValue(3), new MyValue(1), new MyValue(1) }));
        ClassicAssert.IsFalse(tree.Contains(new List<MyValue> { new MyValue(4), new MyValue(2) }));
    }
}
