namespace DKey.Algorithms;

public abstract class MultiSolver : Solver
{
    public override void Run()
    {
        var iterations = IOHelper.ReadInt();
        for (var i = 0; i < iterations; i++)
        {
            var args = Parse();
            Solve(args);
        }
        output.Print();
    }

    public MultiSolver(Type[] types) : base(types)
    {
    }
}