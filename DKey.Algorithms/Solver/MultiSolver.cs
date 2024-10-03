namespace DKey.Algorithms;

public abstract class MultiSolver : Solver
{
    public virtual void Init()
    {
    }

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

    public MultiSolver(Type[] types) : base(types)
    {
    }
}