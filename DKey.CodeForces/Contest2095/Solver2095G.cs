using System.Text;
using DKey.Algorithms;

namespace DKey.CodeForces.Contest2095;


/// <summary>
/// https://codeforces.com/contest/2095/problem/G
/// </summary>
public class Solver2095G : Solver
{
    private Dictionary<string, int> lang = new Dictionary<string, int>
    {
        { "la", 0 },
        { "le", 1 },
        { "lon", 2 },
        { "sha", 3 },
        { "she", 4 },
        { "shon", 5 },
        { "ta", 6 },
        { "te", 7 },
        { "ton", 8 },
    };

    private Dictionary<int, string> lang2 = new Dictionary<int, string>
    {
        { 0, "la" },
        { 1, "le" },
        { 2, "lon" },
        { 3, "sha" },
        { 4, "she" },
        { 5, "shon" },
        { 6, "ta" },
        { 7, "te" },
        { 8, "ton" },
    };

    public Solver2095G() : base( new Type[]{ })
    {
    }

    public override void Solve(object[] objects)
    {
        var data = Console.ReadLine().Split(" ");
        var a = Translate(data[0]);
        var b = Translate(data[1]);
        var res = a + b;
        output.AddLine(TranslateBack(res));
    }

    private int Translate(string data)
    {
        int result = 0;
        StringBuilder sb = new StringBuilder();
        foreach (char c in data.SkipLast(1))
        {
            sb.Append(c);
            if (lang.ContainsKey(sb.ToString()))
            {
                result = result * 9 + lang[sb.ToString()];
                sb.Clear();
            }
        }
        return result;
    }

    private string TranslateBack(int data)
    {
        string result = "";
        while (data > 0)
        {
            int digit = data % 9;
            result = lang2[digit] + result;
            data /= 9;
        }
        return result + "s";
    }
}