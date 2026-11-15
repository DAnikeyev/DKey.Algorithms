namespace DKey.Algorithms.DataStructures.IntervalTree;

/// <summary>
/// Prefix and suffix sums of integer lists.
/// </summary>
public class DataSum
{
    /// <summary>
    /// Prefix sums of the data: result has data.Count + 1 entries,
    /// where result[i] = data[0] + ... + data[i - 1] and result[0] = 0.
    /// </summary>
    /// <param name="data">Input list.</param>
    /// <returns>Prefix sums, result[data.Count] being the total sum.</returns>
    public static long[] PrefixSum(IList<int> data)
    {
        var res = new long[data.Count + 1];
        res[0] = 0;
        for (var i = 0; i < data.Count; i++)
            res[i + 1] = res[i] + data[i];
        return res;
    }

    /// <summary>
    /// Suffix sums of the data: result has data.Count + 1 entries,
    /// where result[i] = data[i] + ... + data[data.Count - 1] and result[data.Count] = 0.
    /// </summary>
    /// <param name="data">Input list.</param>
    /// <returns>Suffix sums, result[0] being the total sum.</returns>
    public static long[] SuffixSum(IList<int> data)
    {
        var res = new long[data.Count + 1];
        res[data.Count] = 0;
        for (var i = data.Count - 1; i >= 0; i--)
        {
            res[i] = res[i + 1] + data[i];
        }
        return res;
    }
}
