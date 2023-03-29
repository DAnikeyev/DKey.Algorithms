using DKey.CodeForces.Problem113;

namespace DKey.CodeForces;

public static class Program
{
    public static Solver solver = new Solver113(new [] {typeof(int), typeof(List<int>)});
    public static void Main()
    {
        solver.Run();
    }
}
