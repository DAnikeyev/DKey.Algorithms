namespace DKey.Algorithms;

public class BinarySearch
{
    //Min x: f(x)>0 for mono increasing func;
    public static int GetIndex(int left, int right, Func<int, int> func)
    {
        if (right - left == 0)
            return right;
        var mid = (right + left) / 2;
        return (func(mid) <= 0) ? GetIndex(mid + 1, right, func) : GetIndex(left, mid, func);
    }
    
    //Min x: f(x)>0 for mono increasing func;
    public static long GetIndexLong(int left, int right, Func<int, long> func)
    {
        if (right - left == 0)
            return right;
        var mid = (right + left) / 2;
        return (func(mid) <= 0) ? GetIndexLong(mid + 1, right, func) : GetIndexLong(left, mid, func);
    }
}