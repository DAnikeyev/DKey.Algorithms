namespace DKey.Algorithms.DataStructures.SegmentTree;

public class IntegerOperationsSegmentTree : IntegerSegmentTree
{
    private Dictionary<long, long> _dictionary;
    private Func<Dictionary<long, long>, long, long, long> _operation;
    private SegmentTreeNode _neutral;
    
    protected override long ItemOperation(long a, long b)
    {
        return _operation(_dictionary, a, b);
    }

    public IntegerOperationsSegmentTree(Func<Dictionary<long, long>, long, long, long> operation, Dictionary<long, long> map)
    {
        _dictionary = map;
        _operation = operation;
        _neutral = new SegmentTreeNode(map.Count - 1);
    }
    
    protected override SegmentTreeNode Neutral()
    {
        return _neutral;
    }
}