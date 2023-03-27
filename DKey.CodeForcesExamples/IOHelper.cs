using System.Text;

namespace DKey.CodeForcesExamples;

public static class IOHelper
{
    private static List<T> ReadLine<T>(Func<string, T> f) =>
        Console.ReadLine().Split(' ').Select(x => f(x)).ToList();

    private static List<int> ReadIntLine() => ReadLine(x => int.Parse(x));
    private static List<long> ReadLongLine() => ReadLine(x => long.Parse(x));
    private static List<ulong> ReadUlongLine() => ReadLine(x => ulong.Parse(x));
    private static List<int> ReadintLine() => ReadLine(x => int.Parse(x));
    
    private static void AddL(this StringBuilder sb, object s)
    {
        sb.Append(s.ToString());
        sb.Append("\n");
    }

    private static void Print(this StringBuilder sb) => Console.Write(sb.ToString());

}