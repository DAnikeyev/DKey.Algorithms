namespace DKey.Algorithms.NumberTheory;


public class ModularArithmetics
{
    public readonly int Module;

    /// <param name="module">Should be prime.</param>
    public ModularArithmetics(int module = 1000000007)
    {
        Module = module;
    }

    public int Power(int value, int power)
    {
        var bits = BinaryArithmetics.Convert(power);

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
}