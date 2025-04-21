using DKey.Algorithms;

namespace DKey.CodeForces.Contest2095;


/// <summary>
/// https://codeforces.com/contest/2095/problem/B
/// </summary>
public class Solver2095B : Solver
{
    public Solver2095B() : base( new Type[]{ })
    {
    }

    private Dictionary<int, int> ans = new Dictionary<int, int>()
    {
        { 1, 3 },
        { 2, 3 },
        { 3, 3 },
        { 4, 3 },
        { 5, 4 },
        { 6, 4 },
        { 7, 4 },
        { 8, 4 },
        { 9, 4 },
        { 10, 4 },
    };

    public override void Solve(object[] objects)
    {
        var q = Console.ReadLine();
        var gameid = int.Parse(q[^1].ToString());
        Console.WriteLine(ans[gameid]);
    }
}