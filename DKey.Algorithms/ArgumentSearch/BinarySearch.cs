namespace DKey.Algorithms.Search;

public class BinarySearch
{
    //Min x: f(x)>0 for mono increasing function.
    public static int GetIndex(int left, int right, Func<int, int> func)
    {
        while (true)
        {
            if (right - left == 0) return func(right) > 0 ? right : right + 1;
            var mid = (right + left) / 2;
            if ((func(mid) <= 0))
            {
                left = mid + 1;
                continue;
            }

            right = mid;
        }
    }

    //Min x: f(x)>0 for mono increasing function.
    public static int GetIndexLong(int left, int right, Func<int, long> func)
    {
        while (true)
        {
            if (right - left == 0) return func(right) > 0 ? right : right + 1;
            var mid = (right + left) / 2;
            if ((func(mid) <= 0))
            {
                left = mid + 1;
                continue;
            }

            right = mid;
        }
    }
}