namespace DKey.Algorithms.DataStructures.SegmentTree;

public struct SegmentTreeNode
{
    public int Value;
    public int LazyValue;
    public bool Pending;

    public SegmentTreeNode(int value)
    {
        Value = value;
        LazyValue = 0;
        Pending = false;
    }
}