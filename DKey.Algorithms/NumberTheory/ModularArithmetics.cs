﻿namespace DKey.Algorithms.NumberTheory;

 
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
        if (power == 0)
            return 1;
        if (!bitsCache.TryGetValue(power, out var bits))
        {
            bits = BinaryArithmetics.ConvertToBoolArray(power);
            bitsCache.Add(power, bits);
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
 
    public int Inverse(int value) => Power(value, Module - 2);
 
    public int Add(int a, int b) => (int)((a + (long)b) % Module);
    public int Multiply(int a, int b) => (int)((a * (long)b) % Module);
    public int Divide(int a, int b) => (int)(((long)a * Inverse(b)) % Module);
    public int ToNonNegative(int a) => (a % Module + Module) % Module;
 
    public int Factorial(int n)
    {
        var ans = 1;
        for (var i = 1; i <= n; i++)
        {
            ans = Multiply(ans, i);
        }
 
        return ans;
    }
 
    public int Choose(int n, int k)
    {
        return Divide(Divide(Factorial(n), Factorial(k)), Factorial(n - k));
    }
}