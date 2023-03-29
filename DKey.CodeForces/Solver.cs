using System.Text;
using DKey.Algorithms.IEnumerableExtensions;

namespace DKey.CodeForces;

public abstract class Solver
{
    public static Type[] ArgTypes;
    public static StringBuilder output = new StringBuilder();

    public static object[] Parse()
    {
        return ArgTypes.Select(x => IOHelper.ReadArg(x)).ToArray();
    }
    public static void Print()
    {
        output.Print();
    }
    public Solver(Type[] types)
    {
        ArgTypes = types;
    }

    public abstract void Solve(object[] objects);

    public virtual void Run()
    {
        var args = Parse();
        Solve(args);
        output.Print();
    }

}