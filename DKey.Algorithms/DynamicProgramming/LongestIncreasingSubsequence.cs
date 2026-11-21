namespace DKey.Algorithms.DynamicProgramming;

/// <summary>
/// Longest increasing subsequence in O(n log n): the classic tail-array +
/// binary-search algorithm, with optional reconstruction of one subsequence.
/// </summary>
public static class LongestIncreasingSubsequence
{
    /// <summary>
    /// Length of the longest increasing subsequence of
    /// <paramref name="data"/> under <paramref name="comparer"/> — strictly
    /// increasing by default (equal neighbors do not chain); pass
    /// <paramref name="allowEqual"/> = true for non-strict (each next element
    /// &gt;= the previous one). Complexity: O(n log n) time, O(n) memory.
    /// </summary>
    /// <param name="data">Input sequence.</param>
    /// <param name="comparer">Element ordering; defaults to
    /// <see cref="Comparer{T}.Default"/>.</param>
    /// <param name="allowEqual">false (default) = strict increase, true =
    /// non-strict.</param>
    public static int Length<T>(IReadOnlyList<T> data, IComparer<T>? comparer = null, bool allowEqual = false)
    {
        comparer ??= Comparer<T>.Default;
        var tails = new T[data.Count];
        var length = 0;
        foreach (var item in data)
        {
            var position = FirstNonExtensible(tails, length, item, comparer, allowEqual);
            tails[position] = item;
            if (position == length)
            {
                length++;
            }
        }

        return length;
    }

    /// <summary>
    /// One longest increasing subsequence of <paramref name="data"/> (the
    /// earliest-ending variant), under the same strict/non-strict semantics as
    /// <see cref="Length"/>. Empty input yields an empty list.
    /// Complexity: O(n log n) time, O(n) memory.
    /// </summary>
    public static List<T> Sequence<T>(IReadOnlyList<T> data, IComparer<T>? comparer = null, bool allowEqual = false)
    {
        comparer ??= Comparer<T>.Default;
        var tails = new T[data.Count];
        var tailIndexes = new int[data.Count];
        var previous = new int[data.Count];
        var length = 0;
        for (var i = 0; i < data.Count; i++)
        {
            var item = data[i];
            var position = FirstNonExtensible(tails, length, item, comparer, allowEqual);
            tails[position] = item;
            tailIndexes[position] = i;
            previous[i] = position > 0 ? tailIndexes[position - 1] : -1;
            if (position == length)
            {
                length++;
            }
        }

        var result = new List<T>(length);
        for (var index = length > 0 ? tailIndexes[length - 1] : -1; index >= 0; index = previous[index])
        {
            result.Add(data[index]);
        }

        result.Reverse();
        return result;
    }

    /// <summary>
    /// First tail position that <paramref name="item"/> cannot extend:
    /// binary search over the non-decreasing tail array. Strict mode requires
    /// item &gt; tail to extend (equal stops); non-strict accepts item &gt;= tail.
    /// </summary>
    private static int FirstNonExtensible<T>(T[] tails, int length, T item, IComparer<T> comparer, bool allowEqual)
    {
        int low = 0, high = length;
        while (low < high)
        {
            var middle = (low + high) / 2;
            var comparison = comparer.Compare(item, tails[middle]);
            var extends = allowEqual ? comparison >= 0 : comparison > 0;
            if (extends)
            {
                low = middle + 1;
            }
            else
            {
                high = middle;
            }
        }

        return low;
    }
}
