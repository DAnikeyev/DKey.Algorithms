namespace DKey.Algorithms.RandomData;

public sealed class ListGenerator
{
    private static readonly object Padlock = new object();
    private static ListGenerator? _instance;
    private readonly Random _random;

    private ListGenerator(int? seed)
    {
        _random = (seed is null) ?  new Random() : new Random(seed.Value);
    }

    public static ListGenerator Instance(int? seed = null)
    {
        lock (Padlock)
        {
            if (_instance is null || seed is not null)
            {
                _instance = new ListGenerator(seed);
            }

            return _instance;
        }
    }

    /// <summary>
    /// Random list of length n with elements from minValue to maxValue.
    /// </summary>
    public List<int> RandomList(int length, int minValue, int maxValue)
    {
        var list = new List<int>();
        for (var i = 0; i < length; i++)
        {
            list.Add(_random.Next(minValue, maxValue + 1));
        }
        return list;
    }
    
    /// <summary>
    /// Random list of length n with elements from minValue to maxValue.
    /// </summary>
    public List<long> RandomList(int length, long minValue, long maxValue)
    {
        var list = new List<long>();
        for (var i = 0; i < length; i++)
        {
            list.Add(_random.NextInt64(minValue, maxValue + 1));
        }
        return list;
    }

    
    /// <summary>
    /// Random permutation of {0,...,n-1};
    /// </summary>
    public List<int> RandomPermutation(int n)
    {
        var permutation = new List<int>();
        for (var i = 0; i < n; i++)
        {
            permutation.Add(i);
        }
        for (var i = n - 1; i >= 1; i--)
        {
            var j = _random.Next(i + 1);
            (permutation[i], permutation[j]) = (permutation[j], permutation[i]);
        }
        return permutation;
    }

    public void Shuffle(List<int> data)
    {
        for (var i = data.Count - 1; i >= 1; i--)
        {
            var j = _random.Next(i + 1);
            (data[i], data[j]) = (data[j], data[i]);
        }
    }

    /// <summary>
    /// Random string of length n with letters from 'a' to 'a' + letters - 1.
    /// </summary>
    public string RandomString(int n, int letters)
    {
        return new string(RandomList(n, 0, letters - 1).Select(x => (char)(x + 'a')).ToArray());
    }
    /// <summary>
    /// Returns random sequence of k ones and n-k zeros.
    /// </summary>
    public List<int> RandomKElements(int n, int k)
    {
       var result = new int[n];
        for (int i = 0; i < k; i++)
        {
            int index = _random.Next(n - i);
            (result[i], result[i + index]) = (result[i + index], result[i]);
        }
        return result.ToList();
    }
}