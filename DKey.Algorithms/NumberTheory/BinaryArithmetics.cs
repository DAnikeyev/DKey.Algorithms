using System.Collections;

namespace DKey.Algorithms.NumberTheory;

/// <summary>
/// Helpers for binary representations of integers.
/// </summary>
public class BinaryArithmetics
{
    /// <summary>
    /// Returns all 32 bits of the value, least significant bit first: 11 -> {1, 1, 0, 1, 0, 0, 0, ...}.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    public static bool[] ConvertToBoolArray(int value)
    {
        var arr = new BitArray(BitConverter.GetBytes(value));
        bool[] bits = new bool[arr.Count];
        arr.CopyTo(bits, 0);
        return bits;
    }

    /// <summary>
    /// Returns the significant bits of the value, most significant bit first (leading zeros trimmed): 11 -> {1, 0, 1, 1}.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    public static List<int> ConvertToBinaryReversedTrimmedList(int value)
    {
        var f = false;
        var res = new List<int>();
        var arr = new BitArray(BitConverter.GetBytes(value));
        for (var i = arr.Count - 1; i >= 0; i--)
        {
            if (arr[i])
                f = true;
            if (f)
                res.Add(arr[i] ? 1 : 0);
        }

        return res;
    }

    /// <summary>
    /// Count non-zero bits in positive int.
    /// </summary>
    /// <param name="value">Value to count bits in.</param>
    public static int CountPositiveBits(int value)
    {
        return ConvertToBoolArray(value).Count(x => x);
    }

    /// <summary>
    /// Convert binary to int.
    /// </summary>
    /// <param name="data">Bits of the number.</param>
    /// <param name="reverse">When true, treats data as most-significant-bit first.</param>
    /// <returns>The reconstructed integer.</returns>
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
    /// <param name="data">Bits of the number.</param>
    /// <param name="reverse">When true, treats data as most-significant-bit first.</param>
    /// <returns>The reconstructed integer.</returns>
    public static int ConvertToInt(IList<bool> data, bool reverse = false)
    {
        if (data.Count > 32)
            throw new ArgumentException("data is too long");
        return data.Select((x, i) => x ? 1 << (reverse ? data.Count - 1 - i : i) : 0).Sum();
    }

    /// <summary>
    /// Returns ceil(log2(value)) for value > 1, and 0 otherwise.
    /// </summary>
    /// <param name="value">Value to compute the logarithm for.</param>
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

    /// <summary>
    /// Converts a value to its string representation in the custom base defined by baseChars
    /// (e.g. new[]{'0','1'} gives the binary representation).
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="baseChars">Digit alphabet; its length is the target base.</param>
    /// <returns>The value written in the target base.</returns>
    public static string IntToString(long value, char[] baseChars)
    {
        var result = string.Empty;
        var targetBase = baseChars.Length;

        do
        {
            result = baseChars[value % targetBase] + result;
            value = value / targetBase;
        }
        while (value > 0);

        return result;
    }
}
