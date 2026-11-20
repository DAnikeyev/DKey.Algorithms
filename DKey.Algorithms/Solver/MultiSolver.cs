namespace DKey.Algorithms;

/// <summary>
/// Base class for multi-test solvers: the input starts with the test count,
/// then each test is parsed and solved in sequence with one shared
/// <see cref="Solver.output"/> buffer.
/// </summary>
public abstract class MultiSolver : Solver
{
    /// <summary>Hook run once before the tests; override for global setup.</summary>
    public virtual void Init()
    {
    }

    /// <summary>Multi-test flow: init, read the test count, then parse + solve
    /// each test, and print the accumulated <see cref="Solver.output"/> once at
    /// the end.</summary>
    public override void Run()
    {
        Init();
        var iterations = IOHelper.ReadInt();
        for (var i = 0; i < iterations; i++)
        {
            var args = Parse();
            Solve(args);
        }
        output.Print();
    }

    /// <summary>Creates the solver for the argument types each test is parsed with.</summary>
    public MultiSolver(Type[] types) : base(types)
    {
    }
}
