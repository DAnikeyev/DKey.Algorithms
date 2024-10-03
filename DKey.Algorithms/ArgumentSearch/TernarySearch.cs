namespace DKey.Algorithms.ArgumentSearch;

public static class TernarySearch
{
 
    // Argmin f(x), for unimodal function (function that has minimum and non-increasing before it and non-decreasing after it)
    public static int GetIndex(int left, int right, Func<int, int> func)
    {
        while (right - left > 2)
        {
            var mid1 = left + (right - left) / 3;
            var mid2 = right - (right - left) / 3;
    
            if (func(mid1) > func(mid2))
            {
                left = mid1;
            }
            else
            {
                right = mid2;
            }
        }
    
        var minIndex = left;
        var minValue = func(left);
        for (var i = left + 1; i <= right; i++)
        {
            var value = func(i);
            if (value < minValue)
            {
                minValue = value;
                minIndex = i;
            }
        }
        return minIndex;
    }
    
    // Argmin f(x), for unimodal function (function that has minimum and non-increasing before it and non-decreasing after it)
    public static int GetIndexLong(int left, int right, Func<int, long> func)
    {
        while (right - left > 2)
        {
            var mid1 = left + (right - left) / 3;
            var mid2 = right - (right - left) / 3;
    
            if (func(mid1) > func(mid2))
            {
                left = mid1;
            }
            else
            {
                right = mid2;
            }
        }
    
        var minIndex = left;
        var minValue = func(left);
        for (var i = left + 1; i <= right; i++)
        {
            var value = func(i);
            if (value < minValue)
            {
                minValue = value;
                minIndex = i;
            }
        }
        return minIndex;
    }
    
    //Argmin f(x), for unimodal function (function that has minimum and non-increasing before it and non-decrising after it)
    public static long GetLongIndexLong(long left, long right, Func<long, long> func)
    {
        while (right - left > 2)
        {
            var mid1 = left + (right - left) / 3;
            var mid2 = right - (right - left) / 3;

            if (func(mid1) > func(mid2))
            {
                left = mid1;
            }
            else
            {
                right = mid2;
            }
        }

        var minIndex = left;
        var minValue = func(left);
        for (var i = left + 1; i <= right; i++)
        {
            var value = func(i);
            if (value < minValue)
            {
                minValue = value;
                minIndex = i;
            }
        }
        return minIndex;
    }


    public static double GoldenSection(double left, double right, Func<double, double> func, double epsilon = 1e-8)
    {
        double phi = (1.0 + Math.Sqrt(5.0)) / 2.0; // Golden ratio


        double x1 = right - (right - left) / phi;
        double x2 = left + (right - left) / phi;

        double f1 = func(x1);
        double f2 = func(x2);

        while (Math.Abs(right - left) > epsilon)
        {
            if (f1 < f2)
            {
                right = x2;
                x2 = x1;
                f2 = f1;

                x1 = right - (right - left) / phi;
                f1 = func(x1);
            }
            else
            {
                left = x1;
                x1 = x2;
                f1 = f2;

                x2 = left + (right - left) / phi;
                f2 = func(x2);
            }
        }

        return (left + right) / 2.0;
    }
}