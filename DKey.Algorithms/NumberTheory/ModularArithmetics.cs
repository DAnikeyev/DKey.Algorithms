using System.Numerics;
namespace DKey.Algorithms.NumberTheory;

 
public class ModularArithmetics
{
    public readonly int Module;
    private List<int>? _factorialCache;
    private Dictionary<int, int>? _inverseCache;
    
 
    /// <summary>
    /// Create a modular arithmetic fot some PRIME module.
    /// </summary>
    public ModularArithmetics(int module = 1000000007, bool cacheFactorials = true, bool inverseCache = true)
    {
        if (cacheFactorials)
            _factorialCache = new() {1, 1, 2};
        if (inverseCache)
            _inverseCache = new();
        Module = module;
    }

    public int Power(long v, long k)
    {
        long ret = 1;
        k %= Module - 1;
        for (; k > 0; k >>= 1, v = v * v % Module)
            if ((k & 1) == 1) ret = ret * v % Module;
        return (int)ret;
    }

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

    public int Add(int a, int b) => ((a + b) % Module);
    
    public void FastAdd(ref int a, int b)
    {
        a += b;
        if (a >= Module)
            a -= Module;
    }
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