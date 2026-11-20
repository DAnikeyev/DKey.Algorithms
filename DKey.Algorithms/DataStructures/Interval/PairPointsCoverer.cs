namespace DKey.Algorithms.DataStructures.Interval;

/// <summary>
/// Interval-pair coverer: scans the intervals in increasing right order and
/// reports the shortest segment built from the interval left endpoints. See
/// <see cref="GetSmallestCover"/> for the exact contract.
/// </summary>
public class PairPointsCoverer
{
    private readonly (long left, long right)[] _intervals;

    /// <summary>Sorts the intervals by their right end (the scan order).</summary>
    public PairPointsCoverer(List<(long left, long right)> intervals)
    {
        _intervals = intervals.OrderBy(x => x.right).ToArray();
    }

    /// <summary>
    /// Returns the shortest candidate window observed while consuming the
    /// intervals in scan order: after each interval its own left endpoint is
    /// removed from the pending set, and each candidate covers the remaining
    /// left endpoints plus the current interval's right end and the first
    /// interval's right end (the window edges are the min of those lefts and
    /// the max of those rights). Ties keep the first window found.
    /// </summary>
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
