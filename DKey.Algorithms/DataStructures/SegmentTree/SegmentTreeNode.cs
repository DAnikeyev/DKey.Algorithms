namespace DKey.Algorithms.DataStructures.SegmentTree;

/// <summary>
/// Node of an integer segment tree: the aggregate value plus a pending lazy update.
/// </summary>
public struct SegmentTreeNode
{
    /// <summary>
    /// Aggregate value of the node's range.
    /// </summary>
    public long Value;

    /// <summary>
    /// Pending lazy addition for the node's subtree.
    /// </summary>
    public long LazyValue;

    /// <summary>
    /// Whether a lazy update is pending for the node.
    /// </summary>
    public bool Pending;

    /// <summary>
    /// Creates a node with the given aggregate value and no pending update.
    /// </summary>
    /// <param name="value">Initial aggregate value.</param>
    public SegmentTreeNode(long value)
    {
        Value = value;
        LazyValue = 0;
        Pending = false;
    }
}
