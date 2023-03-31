namespace DKey.Algorithms.DataStructures.IntervalTree;

public class DataSum
{
    public static long[] PrefixSum(IList<int> data)
    {
        var res = new long[data.Count+1];
        res[0] = 0;
        for (var i = 0; i < data.Count; i++)
            res[i+1] = res[i] + data[i];
        return res;
    }

    public static long[] SuffixSum(IList<int> data)
    {
        var res = new long[data.Count+1];
        res[data.Count] = 0;
        for (var i = data.Count - 1; i >= 0; i--)
        {
            res[i] = res[i + 1] + data[i];
        }
        return res;
    }
}