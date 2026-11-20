namespace DKey.Algorithms.LinqExtension;

/// <summary>
/// Contest-oriented LINQ helpers: pre-sized projections, counting
/// dictionaries and other small utilities the BCL lacks.
/// </summary>
public static class LinqExtension
{

    /// <summary>Sums the projected long values. Complexity: O(n).</summary>
    public static long LongSum<T1, T2>(this IEnumerable<T1> flow, Func<T1, long> func)
    {
        var ans = 0L;
        foreach (var item in flow)
        {
            ans += func(item);
        }

        return ans;
    }

    /// <summary>Sums the int values without overflow (accumulated as long).
    /// Complexity: O(n).</summary>
    public static long LongSum(this IEnumerable<int> flow)
    {
        var ans = 0L;
        foreach (var item in flow)
        {
            ans += item;
        }

        return ans;
    }

    /// <summary>Projects the flow into a pre-sized array (no dynamic resizing).
    /// Complexity: O(n).</summary>
    public static T2[] SelectToArray<T1, T2>(this IEnumerable<T1> flow, int count, Func<T1, T2> func)
    {
        var ret = new T2[count];
        int i = 0;
        foreach (var item in flow)
        {
            ret[i++] = func(item);
        }

        return ret;
    }

    /// <summary>Returns the elements with distinct keys, keeping the first
    /// element of each key. Complexity: O(n).</summary>
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>
        (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        HashSet<TKey> seenKeys = new HashSet<TKey>();
        foreach (TSource element in source)
        {
            if (seenKeys.Add(keySelector(element)))
            {
                yield return element;
            }
        }
    }

    /// <summary>Counts the occurrences of each key. Complexity: O(n).</summary>
    public static Dictionary<TKey, int> ToCountDictionary<TSource, TKey>
        (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull
    {
        var dictionary = new Dictionary<TKey, int>();
        foreach (TSource element in source)
        {
            var key = keySelector(element);
            dictionary.TryGetValue(key, out var val);
            dictionary[key] = val + 1;
        }

        return dictionary;
    }

    /// <summary>Adds the key counts of the flow into an existing counting
    /// dictionary. Complexity: O(n).</summary>
    public static void AddToCountDictionary<TSource, TKey>
        (this Dictionary<TKey, int> dictionary, IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull
    {
        foreach (TSource element in source)
        {
            var key = keySelector(element);
            dictionary.TryGetValue(key, out var val);
            dictionary[key] = val + 1;
        }
    }

    /// <summary>Increments the count of a single element in an existing
    /// counting dictionary.</summary>
    public static void AddToCountDictionary<TSource, TKey>
        (this Dictionary<TKey, int> dictionary, TSource element, Func<TSource, TKey> keySelector) where TKey : notnull
    {
        var key = keySelector(element);
        dictionary.TryGetValue(key, out var val);
        dictionary[key] = val + 1;
    }


    /// <summary>Inserts an item into a sorted list, keeping it sorted.
    /// Appends in O(1) when the item is ≥ the last; otherwise the position is
    /// found by binary search and the shift costs O(n).</summary>
    public static void AddSorted<T>(this List<T> list, T item) where T : IComparable<T>
    {
        if (list.Count == 0)
        {
            list.Add(item);
            return;
        }
        if (list[^1].CompareTo(item) <= 0)
        {
            list.Add(item);
            return;
        }
        if (list[0].CompareTo(item) >= 0)
        {
            list.Insert(0, item);
            return;
        }
        int index = list.BinarySearch(item);
        if (index < 0)
            index = ~index;
        list.Insert(index, item);
    }
}
