using DKey.Algorithms.DataStructures.Graph;

namespace DKey.Algorithms.DataStructures.SegmentTree;

/// <summary>
///Can be modified to perform any associative operation like max or bit operations.
///Supports lazy propagation, so adding value to interval is O(log n) instead of O(n).
/// </summary>
public class IntegerIntervalTree : BinaryTree<SegmentTreeNode>
{
    public IntegerIntervalTree(IList<int> data)
        : base(data.Select(x => new SegmentTreeNode(x)).ToArray())
    {
    }

    protected override SegmentTreeNode Add(SegmentTreeNode a, SegmentTreeNode b)
    {
        return new SegmentTreeNode(a.Value + b.Value);
    }

    protected override SegmentTreeNode Neutral()
    {
        return new SegmentTreeNode(0);
    }

    public void AddToInterval(int left, int right, int value)
    {
        AddToIntervalHelper(left, right, value, 0);
    }

    private void ApplyPending(int current)
    {
        var (rangeLeft, rangeRight) = LeafRanges[current];

        TreeModel[current].Value += TreeModel[current].LazyValue * (rangeRight - rangeLeft + 1);

        // If the current node is not a leaf, propagate the lazy value to its children
        if (rangeLeft != rangeRight)
        {
            int leftChild = LeftChild(current);
            int rightChild = RightChild(current);

            TreeModel[leftChild].LazyValue += TreeModel[current].LazyValue;
            TreeModel[leftChild].Pending = true;

            TreeModel[rightChild].LazyValue += TreeModel[current].LazyValue;
            TreeModel[rightChild].Pending = true;
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
            TreeModel[current].Value += value * (rangeRight - rangeLeft + 1);

            if (rangeLeft != rangeRight)
            {
                int leftChild = LeftChild(current);
                int rightChild = RightChild(current);

                TreeModel[leftChild].LazyValue += value;
                TreeModel[leftChild].Pending = true;

                TreeModel[rightChild].LazyValue += value;
                TreeModel[rightChild].Pending = true;
            }

            return;
        }

        // Recursively update the left and right children
        AddToIntervalHelper(left, right, value, LeftChild(current));
        AddToIntervalHelper(left, right, value, RightChild(current));

        // Update the current node value based on its children's values
        TreeModel[current].Value = TreeModel[LeftChild(current)].Value + TreeModel[RightChild(current)].Value;
    }

    public override SegmentTreeNode GetSum(int left, int right)
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
