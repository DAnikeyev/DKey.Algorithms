using DKey.CodeForces._113;

namespace DKey.CodeForces;

public static class Program
{
    public static void Main()
    {
        var iter = IOHelper.ReadInt();
        for (var i = 0; i < iter; i++)
        {
            var n = IOHelper.ReadInt();
            Solver113.Solve(n);
        }
        Solver113.Print();
    }
}
