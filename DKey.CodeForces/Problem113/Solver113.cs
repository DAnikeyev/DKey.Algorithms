using DKey.Algorithms.NumberTheory;

namespace DKey.CodeForces.Problem113;

//This somehow doesn't work. Contact me if you now WHY.
public class Solver113 : Solver
{
    public override void Solve(object[] args)
    {
        var n = (int)args[0];
        var seq = (List<int>)args[1];
        foreach (var val in seq)
        {
            var factors = PrimeArithmetics.GetPrimeFactors(val);
            if(factors.Sum(x => x.factor) == 2)
                output.AddL("Yes");
            else
                output.AddL("No");
        }
    }

    public Solver113(Type[] types) : base(types)
    {
    }
}