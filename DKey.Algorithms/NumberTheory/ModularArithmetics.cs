using System.Numerics;
namespace DKey.Algorithms.NumberTheory;


/// <summary>
/// Arithmetic operations modulo a prime number.
/// </summary>
public class ModularArithmetics
{
    /// <summary>
    /// The (prime) modulus.
    /// </summary>
    public readonly int Module;
    private List<int>? _factorialCache;
    private Dictionary<int, int>? _inverseCache;


    /// <summary>
    /// Create a modular arithmetic fot some PRIME module.
    /// </summary>
    public ModularArithmetics(int module = 1000000007, bool cacheFactorials = true, bool inverseCache = true)
    {
        if (cacheFactorials)
            _factorialCache = new() { 1, 1, 2 };
        if (inverseCache)
            _inverseCache = new();
        Module = module;
    }

    /// <summary>
    /// Computes v in the power of k modulo Module (fast exponentiation).
    /// </summary>
    /// <param name="v">Base.</param>
    /// <param name="k">Exponent.</param>
    /// <returns>v^k mod Module.</returns>
    public int Power(long v, long k)
    {
        long ret = 1;
        k %= Module - 1;
        for (; k > 0; k >>= 1, v = v * v % Module)
            if ((k & 1) == 1) ret = ret * v % Module;
        return (int)ret;
    }

    /// <summary>
    /// Multiplicative inverse of value modulo the prime Module (Fermat's little theorem); cached across calls
    /// when inverse caching is enabled.
    /// </summary>
    /// <param name="value">Value to invert.</param>
    /// <returns>value^(-1) mod Module.</returns>
    public int Inverse(int value)
    {
        if (_inverseCache == null)
            return Power(value, Module - 2);
        if (_inverseCache.TryGetValue(value, out var inverse))
            return inverse;
        var ret = Power(value, Module - 2);
        _inverseCache[value] = ret;
        return ret;
    }

    /// <summary>
    /// (a + b) mod Module.
    /// </summary>
    public int Add(int a, int b) => ((a + b) % Module);

    /// <summary>
    /// Adds b to a in place, assuming both are already in [0, Module).
    /// </summary>
    /// <param name="a">Value to update in place.</param>
    /// <param name="b">Addend in [0, Module).</param>
    public void FastAdd(ref int a, int b)
    {
        a += b;
        if (a >= Module)
            a -= Module;
    }
    /// <summary>
    /// (a * b) mod Module, computed in 64-bit to avoid overflow.
    /// </summary>
    public int Multiply(int a, int b) => (int)((a * (long)b) % Module);

    /// <summary>
    /// a / b mod Module, via the multiplicative inverse of b.
    /// </summary>
    public int Divide(int a, int b) => (int)(((long)a * Inverse(b)) % Module);

    /// <summary>
    /// Maps any residue a into [0, Module).
    /// </summary>
    public int ToNonNegative(int a) => (a % Module + Module) % Module;

    /// <summary>
    /// n! mod Module, computed iteratively and cached across calls when factorial caching is enabled.
    /// </summary>
    /// <param name="n">Argument of the factorial.</param>
    /// <returns>n! mod Module.</returns>
    public int Factorial(int n)
    {
        if (_factorialCache != null)
        {
            for (var i = _factorialCache.Count; i <= n; i++)
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
    /// <param name="n">Total number of items.</param>
    /// <param name="k">Number of items to choose.</param>
    /// <returns>n! / (k! * (n - k)!) mod Module; 0 when n is less than k.</returns>
    public int Choose(int n, int k)
    {
        return n < k ? 0 : Divide(Divide(Factorial(n), Factorial(k)), Factorial(n - k));
    }

    /// <summary>
    /// Builds a Pascal triangle of layers + 1 rows modulo the given module; each row has layers + 1
    /// entries, with the second half mirroring the first.
    /// </summary>
    /// <param name="layers">Number of triangle layers (rows 0..layers are produced).</param>
    /// <param name="module">Modulus applied to every entry.</param>
    public static List<int[]> MakePascalTriangle(int layers, int module)
    {
        var arithmetics = new ModularArithmetics(module, false, false);
        var factorials = new int[layers + 1];
        var invFactorials = new int[layers + 1];
        factorials[0] = 1;
        invFactorials[0] = 1;
        for (var i = 1; i <= layers; i++)
        {
            factorials[i] = arithmetics.Multiply(factorials[i - 1], i);
            invFactorials[i] = arithmetics.Inverse(factorials[i]);
        }
        var pascal = new List<int[]>();
        for (var i = 0; i <= layers; i++)
        {
            var row = new int[layers + 1];
            for (var j = 0; j <= i / 2; j++)
            {
                row[j] = arithmetics.Multiply(arithmetics.Multiply(factorials[i], invFactorials[j]), invFactorials[i - j]);
                row[i - j] = row[j];
            }
            pascal.Add(row);
        }

        return pascal;
    }
}
