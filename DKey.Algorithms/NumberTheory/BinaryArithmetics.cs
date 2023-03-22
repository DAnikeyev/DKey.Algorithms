using System.Collections;

namespace DKey.Algorithms.NumberTheory;

public class BinaryArithmetics
{
    public static bool[] ConvertToBoolArray(int value)
    {
        var arr = new BitArray(BitConverter.GetBytes(value));
        bool[] bits = new bool[arr.Count];
        arr.CopyTo(bits, 0);
        return bits;
    }
    
    /// <summary>
    /// Count non-zero bits in positive int.
    /// </summary>
    public static int CountPositiveBits(int value)
    {
        return ConvertToBoolArray(value).Count(x => x);
    }
    
    /// <summary>
    /// Convert sequence of zeros and ones to int.
    /// </summary>
    public static int ConvertToInt(IList<int> data)
    {
        if (data.Count > 32)
            throw new ArgumentException("{data is too long");
        return data.Select((x, i) => (x == 1) ? 1 << i : 0).Sum();
    }
    
    /// <summary>
    /// Convert sequence of zeros and ones to int.
    /// </summary>
    public static int ConvertToInt(IList<bool> data)
    {
        if (data.Count > 32)
            throw new ArgumentException("data is too long");
        return data.Select((x, i) => x ? 1 << i : 0).Sum();
    }
}