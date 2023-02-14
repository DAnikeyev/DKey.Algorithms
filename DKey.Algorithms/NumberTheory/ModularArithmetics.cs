namespace DKey.Algorithms.NumberTheory;


public class ModularArithmetics
{
    public readonly int Module;
    public Dictionary<int, bool[]> bitsCache = new Dictionary<int, bool[]>();

    /// <param name="module">Should be prime.</param>
    public ModularArithmetics(int module = 1000000007)
    {
        Module = module;
    }

    /// <summary>
    /// Fast exponentiation.
    /// </summary>
    public int Power(int value, int power, bool useCache = true)
    {
        if (!bitsCache.TryGetValue(power, out var bits))
        {
            bits = BinaryArithmetics.Convert(power);
            bitsCache.Add(power, bits);
        }


        var remainder = value % Module;
        foreach (var bit in bits.Reverse().SkipWhile(x => !x).Skip(1))
        {
            remainder = (remainder * remainder) % Module;
            if (bit)
                remainder = remainder * value % Module;
        }

        return remainder;
    }

    public int Inverse(int value) => Power(value, Module - 2);

    public int Add(int a, int b) => (int)((a + (long) b) % Module);
    public int Multiply(int a, int b) => (int)((a * (long) b) % Module);
    public int Divide(int a, int b) => (int)((a * Inverse(b)) % Module);
    public int ToNonNegative(int a) => (a % Module + Module) % Module;
}