using System.Collections;

namespace DKey.Algorithms.NumberTheory;

public class BinaryArithmetics
{
    internal static bool[] Convert(int value)
    {
        var arr = new BitArray(BitConverter.GetBytes(value));
        bool[] bits = new bool[arr.Count];
        arr.CopyTo(bits, 0);
        return bits;
    }
}