namespace DKey.Algorithms.DataStructures.Interval;

public class PairPointsCoverer
{
    private readonly (long left, long right)[] _intervals;

    public PairPointsCoverer(List<(long left, long right)> intervals)
    {
        _intervals = intervals.OrderBy(x => x.right).ToArray();
    }

    public (long left, long right) GetSmallestCover()
    {
        var leftValues = new SortedSet<long>();
        var leftsQueue = new Queue<long>();

        foreach (var interval in _intervals)
        {
            leftValues.Add(interval.left);
            leftsQueue.Enqueue(interval.left);
        }

        // Take all lefts.
        var offset = leftValues.Min;
        var length = leftValues.Max - leftValues.Min;
        foreach (var interval in _intervals)
        {
            leftValues.Remove(leftsQueue.Dequeue());
            if (leftValues.Count > 0)
            {
                var r_cand = Math.Max(interval.right, leftValues.Max);
                var l_cand = Math.Min(_intervals[0].right, leftValues.Min);
                var length_cand = r_cand - l_cand;
                if (length_cand < length)
                {
                    length = length_cand;
                    offset = l_cand;
                }
            }
        }

        return (offset, offset + length);
    }
}