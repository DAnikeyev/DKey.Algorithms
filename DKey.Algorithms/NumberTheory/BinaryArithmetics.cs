using System.Collections;

namespace DKey.Algorithms.NumberTheory;

public class BinaryArithmetics
{
    //11 -> {1, 1, 0, 1, 0, 0, 0,...}
    public static bool[] ConvertToBoolArray(int value)
    {
        var arr = new BitArray(BitConverter.GetBytes(value));
        bool[] bits = new bool[arr.Count];
        arr.CopyTo(bits, 0);
        return bits;
    }

    //11 -> {1, 0, 1, 1}
    public static List<int> ConvertToBinaryReversedTrimmedList(int value)
    {
        var f = false;
        var res = new List<int>();
        var arr = new BitArray(BitConverter.GetBytes(value));
        for (var i = arr.Count - 1; i >= 0; i--)
        {
            if(arr[i])
                f = true;
            if(f)
                res.Add(arr[i] ? 1 : 0);
        }

        return res;
    }
    
    /// <summary>
    /// Count non-zero bits in positive int.
    /// </summary>
    public static int CountPositiveBits(int value)
    {
        return ConvertToBoolArray(value).Count(x => x);
    }
    
    /// <summary>
    /// Convert binary to int.
    /// </summary>
    public static int ConvertToInt(IList<int> data, bool reverse = false)
    {
        var size = data.Count - 1;
        if (data.Count > 32)
            throw new ArgumentException("{data is too long");
        return data.Select((x, i) => (x == 1) ? 1 << (reverse ? data.Count - 1 - i : i) : 0).Sum();
    }
    
    /// <summary>
    /// Convert binary to int.
    /// </summary>
    public static int ConvertToInt(IList<bool> data, bool reverse = false)
    {
        if (data.Count > 32)
            throw new ArgumentException("data is too long");
        return data.Select((x, i) => x ? 1 << (reverse ? data.Count - 1 -i : i) : 0).Sum();
    }

    public static int GetCeilingLog(int value)
    {
        if (value <= 1)
            return 0;
        var i = 0;
        while (value > 1)
        {
            value = (value + 1) >> 1;
            i++;
        }

        return i;
    }
}