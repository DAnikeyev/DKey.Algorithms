namespace DKey.Algorithms.DataStructures.SegmentTree;

public struct SegmentTreeNode
{
    public long Value;
    public long LazyValue;
    public bool Pending;

    public SegmentTreeNode(long value)
    {
        Value = value;
        LazyValue = 0;
        Pending = false;
    }
}