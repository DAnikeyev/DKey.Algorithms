using System.Text;

namespace DKey.Algorithms;

/// <summary>
/// Base class for single-test solvers: reads the input arguments of the
/// configured types, hands them to <see cref="Solve"/>, and accumulates the
/// answer in <see cref="output"/> for printing. Contest entry points inherit
/// this and implement <see cref="Solve"/>.
/// </summary>
public abstract class Solver
{
    /// <summary>The argument types parsed from the input, in order.</summary>
    public Type[] ArgTypes;
    /// <summary>The accumulating answer buffer, printed by <see cref="Print"/>.</summary>
    public readonly StringBuilder output = new StringBuilder();

    /// <summary>Creates the solver for the given argument types.</summary>
    public Solver(Type[] types)
    {
        ArgTypes = types;
    }

    /// <summary>Reads one value per configured argument type from the input.</summary>
    public object[] Parse()
    {
        return ArgTypes.Select(x => IOHelper.ReadArg(x)).ToArray();
    }
    /// <summary>Flushes <see cref="output"/> to the real output.</summary>
    public void Print()
    {
        output.Print();
    }

    /// <summary>The actual task solution over the parsed arguments; write results
    /// into <see cref="output"/>.</summary>
    public abstract void Solve(object[] objects);

    /// <summary>The standard single-test flow: parse, solve, print.</summary>
    public virtual void Run()
    {
        var args = Parse();
        Solve(args);
        output.Print();
    }

    /// <summary>Runs <see cref="Solve"/> on pre-built arguments; prints when
    /// <paramref name="print"/> is set.</summary>
    public virtual void RunWithArgs(object[] args, bool print = false)
    {
        Solve(args);
        if (print)
            output.Print();
    }
}
