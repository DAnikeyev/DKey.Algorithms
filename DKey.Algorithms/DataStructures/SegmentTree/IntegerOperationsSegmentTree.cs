namespace DKey.Algorithms.DataStructures.SegmentTree;

/// <summary>
/// Segment tree whose aggregation is a custom operation with access to a shared value dictionary,
/// e.g. counting distinct values in an interval.
/// </summary>
public class IntegerOperationsSegmentTree : IntegerSegmentTree
{
    private Dictionary<long, long> _dictionary;
    private Func<Dictionary<long, long>, long, long, long> _operation;
    private SegmentTreeNode _neutral;

    /// <summary>
    /// The custom associative tree operation over two node values.
    /// </summary>
    protected override long ItemOperation(long a, long b)
    {
        return _operation(_dictionary, a, b);
    }

    /// <summary>
    /// Creates a tree with a custom aggregation operation.
    /// </summary>
    /// <param name="operation">Associative operation (dictionary, a, b) => aggregated value.</param>
    /// <param name="map">Value mapping shared with the operation.</param>
    public IntegerOperationsSegmentTree(Func<Dictionary<long, long>, long, long, long> operation, Dictionary<long, long> map)
    {
        _dictionary = map;
        _operation = operation;
        _neutral = new SegmentTreeNode(map.Count - 1);
    }

    /// <summary>The neutral node, pre-built from the map's current size.</summary>
    protected override SegmentTreeNode Neutral()
    {
        return _neutral;
    }
}
