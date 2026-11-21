using DKey.Algorithms.DataStructures.SegmentTree;

namespace DKey.Algorithms.Tests.Graph_likeStructures;

[TestFixture]
public class SegmentTreeTests
{

    [Test]
    public void MultipleOperationsWithLargerList_ReturnsExpectedSum()
    {
        IList<int> data = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        IntegerSegmentTree tree = new IntegerSegmentTree();
        tree.InitFromIntList(data);

        // Add 5 to the interval [2, 5]
        tree.AddToInterval(2, 5, 5);
        // Updated data: [1, 2, 8, 9, 10, 11, 7, 8, 9, 10]

        ClassicAssert.AreEqual(38, tree.GetCumulativeOperation(2, 5).Value); // Sum of 8 + 9 + 10 + 11
        ClassicAssert.AreEqual(11, tree.GetCumulativeOperation(0, 2).Value); // Sum of 1 + 2 + 8
        ClassicAssert.AreEqual(45, tree.GetCumulativeOperation(5, 9).Value); // Sum of 11 + 7 + 8 + 9 + 10

        // Add -3 to the interval [0, 3]
        tree.AddToInterval(0, 3, -3);
        // Updated data: [-2, -1, 5, 6, 10, 11, 7, 8, 9, 10]

        ClassicAssert.AreEqual(2, tree.GetCumulativeOperation(0, 2).Value); // Sum of -2 + (-1) + 5
        ClassicAssert.AreEqual(27, tree.GetCumulativeOperation(3, 5).Value); // Sum of 6 + 10 + 11
        ClassicAssert.AreEqual(45, tree.GetCumulativeOperation(5, 9).Value); // Sum of 11 + 7 + 8 + 9 + 10

        // Add 2 to the interval [6, 9]
        tree.AddToInterval(6, 9, 2);
        // Updated data: [-2, -1, 5, 6, 10, 11, 9, 10, 11, 12]

        ClassicAssert.AreEqual(42, tree.GetCumulativeOperation(6, 9).Value); // Sum of 9 + 10 + 11 + 12
        ClassicAssert.AreEqual(18, tree.GetCumulativeOperation(0, 4).Value); // Sum of -2 + (-1) + 5 + 6 + 10
    }
}
