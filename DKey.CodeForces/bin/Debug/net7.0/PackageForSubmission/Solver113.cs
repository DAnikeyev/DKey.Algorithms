using DKey.Algorithms.NumberTheory;

namespace DKey.CodeForces._113;

public class Solver113 : Solver
{
    public static void Solve(int n)
    {
        var factors = PrimeArithmetics.GetPrimeFactors(n);
        if(factors.Count == 2 && factors.All(x => x.factor == 1))
            output.AddL("Yes");
        else
            output.AddL("No");
    }
}