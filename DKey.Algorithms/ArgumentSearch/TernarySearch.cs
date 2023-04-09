namespace DKey.Algorithms.ArgumentSearch;

public static class TernarySearch
{
    //Argmin f(x), for unimodal function (function that has minimum and non-increasing before it and non-decrising after it)
    public static int GetIndex(int left, int right, Func<int, int> func)
    {
        while (true)
        {
            if (right - left <= 1) return func(left) <= func(right) ? left : right;

            var mid1 = left + Math.Max((right - left) / 3, 1);
            var mid2 = right - Math.Max((right - left) / 3, 1);

            if (func(mid1) > func(mid2))
            {
                left = mid1;
                continue;
            }

            right = mid2;
        }
    }
    
    //Argmin f(x), for unimodal function (function that has minimum and non-increasing before it and non-decrising after it)
    public static int GetIndexLong(int left, int right, Func<int, long> func)
    {
        while (true)
        {
            if (right - left <= 1) return func(left) <= func(right) ? left : right;

            var mid1 = left + Math.Max((right - left) / 3, 1);
            var mid2 = right - (right - left) / 3;

            if (func(mid1) > func(mid2))
            {
                left = mid1;
                continue;
            }

            right = mid2;
        }
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