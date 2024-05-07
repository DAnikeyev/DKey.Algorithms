using DKey.Algorithms;
using DKey.CodeForces.Contest1955;

namespace DKey.CodeForces;

public static class Program
{
    private static Solver _solver = new Solver1955H();
    public static void Main()
    {
        _solver.Run();
    }
}