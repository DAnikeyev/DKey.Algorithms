using System.Collections;

namespace DKey.Algorithms.NumberTheory;

public class BinaryArithmetics
{
    public static bool[] Convert(int value)
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
        return Convert(value).Count(x => x);
    }
}