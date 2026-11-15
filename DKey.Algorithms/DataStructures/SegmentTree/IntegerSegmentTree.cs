using System.Data;
using DKey.Algorithms.DataStructures.Graph;

namespace DKey.Algorithms.DataStructures.SegmentTree;

/// <summary>
/// Segment tree for addition with lazy propagation: adding a value to an interval and querying the
/// interval sum both take O(log n). Can be adapted to any associative operation by overriding ItemOperation.
/// </summary>
public class IntegerSegmentTree : ImmutableBinaryTree<SegmentTreeNode>
{
    /// <summary>
    /// Creates an empty tree; initialize it with InitFromIntList or InitFromLongList before use.
    /// </summary>
    public IntegerSegmentTree()
    {
    }

    /// <summary>
    /// Builds the tree from a list of 64-bit leaves.
    /// </summary>
    /// <param name="data">Initial leaf values.</param>
    public void InitFromLongList(IList<long> data)
    {
        this.Init(data.Select(x => new SegmentTreeNode(x)).ToList());
    }

    /// <summary>
    /// Builds the tree from a list of 32-bit leaves.
    /// </summary>
    /// <param name="data">Initial leaf values.</param>
    public void InitFromIntList(IList<int> data)
    {
        this.Init(data.Select(x => new SegmentTreeNode(x)).ToList());
    }

    /// <summary>
    /// Combines two node aggregates with the tree operation.
    /// </summary>
    protected override SegmentTreeNode Add(SegmentTreeNode a, SegmentTreeNode b)
    {
        return new SegmentTreeNode(ItemOperation(a.Value, b.Value));
    }

    /// <summary>
    /// The associative operation of the tree (addition by default); override to change the semantics.
    /// </summary>
    protected virtual long ItemOperation(long a, long b)
    {
        return a + b;
    }

    /// <summary>
    /// Neutral element of the tree operation (0 for addition).
    /// </summary>
    protected override SegmentTreeNode Neutral()
    {
        return new SegmentTreeNode(0);
    }

    /// <summary>
    /// Adds value to every leaf in [left, right] in O(log n).
    /// </summary>
    /// <param name="left">Left bound of the interval (inclusive, 0-based leaf index).</param>
    /// <param name="right">Right bound of the interval (inclusive, 0-based leaf index).</param>
    /// <param name="value">Value to add to every element of the interval.</param>
    public void AddToInterval(int left, int right, int value)
    {
        AddToIntervalHelper(left, right, value, 0);
    }

    private void ApplyPending(int current)
    {
        var (rangeLeft, rangeRight) = LeafRanges[current];

        TreeModel[current].Value = UpdateValue(TreeModel[current].Value, 0, TreeModel[current].LazyValue, rangeLeft, rangeRight);

        // If the current node is not a leaf, propagate the lazy value to its children
        if (rangeLeft != rangeRight)
        {
            int leftChild = LeftChild(current);
            int rightChild = RightChild(current);
            LazyUpdate(leftChild, TreeModel[current].LazyValue);
            LazyUpdate(rightChild, TreeModel[current].LazyValue);
        }

        // Reset the lazy value and pending flag for the current node
        TreeModel[current].LazyValue = 0;
        TreeModel[current].Pending = false;
    }

    private void AddToIntervalHelper(int left, int right, int value, int current)
    {
        var (rangeLeft, rangeRight) = LeafRanges[current];

        // If there is a pending operation, apply it to the current node
        if (TreeModel[current].Pending)
            ApplyPending(current);

        // If the current node is outside the query range, return
        if (left > rangeRight || right < rangeLeft)
        {
            return;
        }

        // If the current node is within the query range, update its value and propagate the lazy value to its children
        if (left <= rangeLeft && right >= rangeRight)
        {
            TreeModel[current].Value = UpdateValue(TreeModel[current].Value, value, 0, rangeLeft, rangeRight);

            if (rangeLeft != rangeRight)
            {
                int leftChild = LeftChild(current);
                int rightChild = RightChild(current);

                LazyUpdate(leftChild, value);
                LazyUpdate(rightChild, value);
            }
            return;
        }

        // Recursively update the left and right children
        AddToIntervalHelper(left, right, value, LeftChild(current));
        AddToIntervalHelper(left, right, value, RightChild(current));

        // Update the current node value based on its children's values
        TreeModel[current].Value = ItemOperation(TreeModel[LeftChild(current)].Value, TreeModel[RightChild(current)].Value);
    }

    private void LazyUpdate(int index, long value)
    {
        TreeModel[index].LazyValue = UpdateLazyValue(TreeModel[index].LazyValue, value);
        TreeModel[index].Pending = true;
    }

    private long UpdateValue(long oldValue, long update, long LazyValue, int left, int right)
    {
        return oldValue + (update + LazyValue) * (right - left + 1);
    }

    private long UpdateLazyValue(long oldValue, long addedValue)
    {
        return ItemOperation(oldValue, addedValue);
    }

    /// <summary>
    /// Returns the aggregate of all leaves in [left, right]: the interval sum by default.
    /// </summary>
    /// <param name="left">Left bound of the interval (inclusive, 0-based leaf index).</param>
    /// <param name="right">Right bound of the interval (inclusive, 0-based leaf index).</param>
    public override SegmentTreeNode GetCumulativeOperation(int left, int right)
    {
        return GetSumHelper(left, right, 0);
    }

    private SegmentTreeNode GetSumHelper(int left, int right, int current)
    {
        var (rangeLeft, rangeRight) = LeafRanges[current];

        if (TreeModel[current].Pending)
            ApplyPending(current);

        if (left > rangeRight || right < rangeLeft)
        {
            return Neutral();
        }

        if (left <= rangeLeft && right >= rangeRight)
        {
            return TreeModel[current];
        }

        SegmentTreeNode leftSum = GetSumHelper(left, right, LeftChild(current));
        SegmentTreeNode rightSum = GetSumHelper(left, right, RightChild(current));

        return Add(leftSum, rightSum);
    }
}
