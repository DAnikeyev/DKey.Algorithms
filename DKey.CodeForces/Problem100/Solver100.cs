using DKey.CodeForces;

namespace DKey.CodeForces.Problem100;

public class Solver100 : Solver
{
    public Solver100(Type[] types) : base(types)
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var a = seq[0];
        var b = seq[1];
        output.AddL(a+b);
    }
}