namespace DKey.Algorithms.NumberTheory;

public class PrimeArithmetics
{
    public static bool IsPrime(int n)
    {
        if (n < 2)
            return false;
        if (n == 2)
            return true;
        if (n % 2 == 0)
            return false;
        for (int i = 3; i * i <= n; i += 2)
            if (n % i == 0)
                return false;
        return true;
    }


    public static List<(int prime, int factor)> GetPrimeFactors(int n)
    {
        var result = new List<(int prime, int factor)>();
        for (var i = 2; i * i <= n; i++)
        {
            var factor = 0;
            while (n % i == 0)
            {
                factor++;
                n /= i;
            }
            if (factor > 0)
                result.Add((i, factor));
        }
        if (n > 1)
            result.Add((n, 1));

        return result;
    }
    
    public static List<(int prime, int factor)>[] GetPrimeFactorsForInterval(int n)
    {
        var smallestPrimeFactor = new int[n + 1];
        for (var i = 2; i <= n; i += 2)
            smallestPrimeFactor[i] = 2;

        for (var i = 3; i * i <= n; i += 2)
        {
            if (smallestPrimeFactor[i] == 0)
            {
                smallestPrimeFactor[i] = i;
                for (var j = i * i; j <= n; j += i * 2)
                {
                    if (smallestPrimeFactor[j] == 0)
                        smallestPrimeFactor[j] = i;
                }
            }
        }

        for (var i = 2; i <= n; i++)
        {
            if (smallestPrimeFactor[i] == 0)
                smallestPrimeFactor[i] = i;
        }

        var result = new List<(int prime, int factor)>[n + 1];
        result[0] = new List<(int prime, int factor)>();
        result[1] = new List<(int prime, int factor)>();
        for (int i = 2; i <= n; i++)
        {
            result[i] = new List<(int prime, int factor)>();
            var current = i;
            while (current != 1)
            {
                var prime = smallestPrimeFactor[current];
                var factor = 0;
                while (current % prime == 0)
                {
                    factor++;
                    current /= prime;
                }
                result[i].Add((prime, factor));
            }
        }

        return result;
    }
    
    
    public static List<int> GetAllDividers(List<(int prime, int factors)> primeFactors)
    {
        var dividers = new List<int> { 1 };

        foreach (var (prime, factor) in primeFactors)
        {
            var size = dividers.Count;
            var currentMultiplier = 1;

            for (var i = 0; i < factor; i++)
            {
                currentMultiplier *= prime;
                for (var j = 0; j < size; j++)
                {
                    dividers.Add(dividers[j] * currentMultiplier);
                }
            }
        }

        dividers.Sort();
        return dividers;
    }
    
    public static List<int> GetAllDividers(int n)
    {
        var primeFactors = GetPrimeFactors(n);
        var dividers = new List<int> { 1 };

        foreach (var (prime, factor) in primeFactors)
        {
            var size = dividers.Count;
            var currentMultiplier = 1;

            for (var i = 0; i < factor; i++)
            {
                currentMultiplier *= prime;
                for (var j = 0; j < size; j++)
                {
                    dividers.Add(dividers[j] * currentMultiplier);
                }
            }
        }

        dividers.Sort();
        return dividers;
    }

}