namespace DKey.Algorithms.NumberTheory;

/// <summary>
/// Integer arithmetics helpers.
/// </summary>
public class IntArithmetics
{
    /// <summary>
    /// Greatest common divisor (Euclid's algorithm).
    /// </summary>
    /// <param name="a">First integer.</param>
    /// <param name="b">Second integer.</param>
    /// <returns>The greatest common divisor of a and b.</returns>
    public static int GCD(int a, int b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }

    /// <summary>
    /// Greatest common divisor (Euclid's algorithm).
    /// </summary>
    /// <param name="a">First integer.</param>
    /// <param name="b">Second integer.</param>
    /// <returns>The greatest common divisor of a and b.</returns>
    public static long GCD(long a, long b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }
}
