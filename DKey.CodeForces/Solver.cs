using System.Text;
using DKey.Algorithms;
using DKey.Algorithms.IEnumerableExtensions;

namespace DKey.CodeForces;

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

}