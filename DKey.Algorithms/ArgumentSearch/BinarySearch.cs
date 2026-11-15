namespace DKey.Algorithms.Search;

/// <summary>
/// Binary search over a monotone predicate on integer arguments.
/// </summary>
public class BinarySearch
{
    /// <summary>
    /// Returns the smallest x in [left, right] for which the monotonically increasing func(x) is positive;
    /// right + 1 when func(x) is non-positive on the whole range.
    /// </summary>
    /// <param name="left">Left bound of the search range (inclusive).</param>
    /// <param name="right">Right bound of the search range (inclusive).</param>
    /// <param name="func">Monotonically increasing function.</param>
    public static int GetIndex(int left, int right, Func<int, int> func)
    {
        while (true)
        {
            if (right - left == 0) return func(right) > 0 ? right : right + 1;
            var mid = (right + left) / 2;
            if ((func(mid) <= 0))
            {
                left = mid + 1;
                continue;
            }

            right = mid;
        }
    }

    /// <summary>
    /// Returns the smallest x in [left, right] for which the monotonically increasing func(x) is positive;
    /// right + 1 when func(x) is non-positive on the whole range.
    /// </summary>
    /// <param name="left">Left bound of the search range (inclusive).</param>
    /// <param name="right">Right bound of the search range (inclusive).</param>
    /// <param name="func">Monotonically increasing function.</param>
    public static int GetIndexLong(int left, int right, Func<int, long> func)
    {
        while (true)
        {
            if (right - left == 0) return func(right) > 0 ? right : right + 1;
            var mid = (right + left) / 2;
            if ((func(mid) <= 0))
            {
                left = mid + 1;
                continue;
            }

            right = mid;
        }
    }

    /// <summary>
    /// Returns the smallest x in [left, right] for which the monotonically increasing func(x) is positive;
    /// right + 1 when func(x) is non-positive on the whole range.
    /// </summary>
    /// <param name="left">Left bound of the search range (inclusive).</param>
    /// <param name="right">Right bound of the search range (inclusive).</param>
    /// <param name="func">Monotonically increasing function.</param>
    public static long GetLongIndexLong(long left, long right, Func<long, long> func)
    {
        while (true)
        {
            if (right - left == 0) return func(right) > 0 ? right : right + 1;
            var mid = (right + left) / 2;
            if ((func(mid) <= 0))
            {
                left = mid + 1;
                continue;
            }

            right = mid;
        }
    }
}
