using System.Text;

namespace DKey.CodeForces;

public static class IOHelper
{
    internal static List<T> ReadLine<T>(Func<string, T> f) =>
        Console.ReadLine().Split(' ').Select(x => f(x)).ToList();

    internal static List<int> ReadIntLine() => ReadLine(x => int.Parse(x));
    internal static int ReadInt() => ReadIntLine()[0];
    internal static List<long> ReadLongLine() => ReadLine(x => long.Parse(x));
    internal static long ReadLong() => ReadLongLine()[0];
    internal static List<ulong> ReadUlongLine() => ReadLine(x => ulong.Parse(x));
    internal static List<int> ReadintLine() => ReadLine(x => int.Parse(x));
    
    internal static void AddL(this StringBuilder sb, object obj)
    {
        sb.Append(obj.ToString());
        sb.Append("\n");
    }
    
    internal static void AddL(this StringBuilder sb, IEnumerable<object> data)
    {
        sb.Append(string.Join(" ", data.ToString()));
        sb.Append("\n");
    }

    internal static void Print(this StringBuilder sb) => Console.Write(sb.ToString());
}