using System.Numerics;
namespace DKey.Algorithms.NumberTheory;

 
public class ModularArithmetics
{
    public readonly int Module;
    private Dictionary<int, bool[]> _bitsCache = new ();
    private List<int>? _factorialCache;
    
 
    /// <summary>
    /// Create a modular arithmetic fot some PRIME module.
    /// </summary>
    public ModularArithmetics(int module = 1000000007, bool cacheFactorials = true)
    {
        if (cacheFactorials)
            _factorialCache = new() {1, 1, 2};
        Module = module;
    }
 
    /// <summary>
    /// Fast exponentiation.
    /// </summary>
    public int Power(int value, int power)
    {
        if (power == 0)
            return 1;
        if (!_bitsCache.TryGetValue(power, out var bits))
        {
            bits = BinaryArithmetics.ConvertToBoolArray(power);
            _bitsCache.Add(power, bits);
        }
 
 
        long remainder = value % Module;
        foreach (var bit in bits.Reverse().SkipWhile(x => !x).Skip(1))
        {
            remainder = (remainder * remainder) % Module;
            if (bit)
                remainder = remainder * value % Module;
        }
 
        return (int)remainder;
    }
 
    /// <summary>
    /// GPT Sugessted, TODO: check perfomance and values.
    /// </summary>
    public uint Power2(uint value, uint power)
    {
        if (power == 0)
            return 1;

        var remainder = value % Module;
        var mask = 1u << (int)(BitOperations.Log2(power) - 1);

        while (mask > 0)
        {
            remainder = (remainder * remainder) % Module;
            if ((power & mask) != 0)
                remainder = remainder * value % Module;

            mask >>= 1;
        }

        return (uint)remainder;
    }
    
    public int Inverse(int value) => Power(value, Module - 2);
 
    public int Add(int a, int b) => (int)((a + (long)b) % Module);
    public int Multiply(int a, int b) => (int)((a * (long)b) % Module);
    public int Divide(int a, int b) => (int)(((long)a * Inverse(b)) % Module);
    public int ToNonNegative(int a) => (a % Module + Module) % Module;
 
    public int Factorial(int n)
    {
        if (_factorialCache != null)
        {
            for(var i = _factorialCache.Count; i <= n; i++)
                _factorialCache.Add(Multiply(_factorialCache[i - 1], i));
            return _factorialCache[n];
        }
        var ans = 1;
        for (var i = 1; i <= n; i++)
        {
            ans = Multiply(ans, i);
        }
 
        return ans;
    }
 
    /// <summary>
    /// n choose k.
    /// </summary>
    public int Choose(int n, int k)
    {
        return n < k ? 0 : Divide(Divide(Factorial(n), Factorial(k)), Factorial(n - k));
    }
}