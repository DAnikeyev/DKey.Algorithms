using System.Text;

namespace DKey.Algorithms;

public abstract class Solver
{
    public Type[] ArgTypes;
    public readonly StringBuilder output = new StringBuilder();

    public Solver(Type[] types)
    {
        ArgTypes = types;
    }
    
    public object[] Parse()
    {
        return ArgTypes.Select(x => IOHelper.ReadArg(x)).ToArray();
    }
    public void Print()
    {
        output.Print();
    }

    public abstract void Solve(object[] objects);

    public virtual void Run()
    {
        var args = Parse();
        Solve(args);
        output.Print();
    }

    public virtual void RunWithArgs(object[] args, bool print = false)
    {
        Solve(args);
        if (print)
            output.Print();
    }
}