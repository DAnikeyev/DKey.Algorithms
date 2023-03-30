using System.Text;

namespace DKey.Algorithms;

internal static class IOHelper
{
    public static Dictionary<Type, Func<object>> typeReaderMap = new()
    {
        { typeof(int), () => ReadInt() },
        { typeof(long), () => ReadLong() },
        { typeof(string), () => Console.ReadLine() },
        { typeof(List<int>), () => ReadIntLine() },
        { typeof(List<long>), () => ReadLongLine() },
        { typeof(List<string>), () => Console.ReadLine().Split(' ').ToList() }
    };
    
    public static List<T> ReadLine<T>(Func<string, T> f) =>
        Console.ReadLine().Split(' ').Select(x => f(x)).ToList();

    public static List<int> ReadIntLine() => ReadLine(x => int.Parse(x));
    public static int ReadInt() => ReadIntLine()[0];
    public static List<long> ReadLongLine() => ReadLine(x => long.Parse(x));
    public static long ReadLong() => ReadLongLine()[0];
    public static List<ulong> ReadUlongLine() => ReadLine(x => ulong.Parse(x));
    public static List<int> ReadintLine() => ReadLine(x => int.Parse(x));
    
    public static void AddL(this StringBuilder sb, object obj)
    {
        sb.Append(obj.ToString());
        sb.Append("\n");
    }
    
    public static void AddL(this StringBuilder sb, IEnumerable<object> data)
    {
        sb.Append(string.Join(" ", data.ToString()));
        sb.Append("\n");
    }

    public static List<List<int>> Read2dList(int n)
    {
        return Enumerable.Repeat(1, n).Select(x => ReadIntLine()).ToList();
    }

    public static void Print(this StringBuilder sb) => Console.Write(sb.ToString());
    public static object ReadArg(Type type, int dim1 = 0)
    {
        if (type == typeof(List<List<int>>))
            return Read2dList(dim1);
        
        if (typeReaderMap.TryGetValue(type, out var reader))
        {
            return reader();
        }

        throw new Exception("Unknown type");
    }
}