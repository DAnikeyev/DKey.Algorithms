namespace DKey.Algorithms.IEnumerableExtensions;

public static class LinqExtension
{
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

    public static void AddToCountDictionary<TSource, TKey>
        (this Dictionary<TKey, int> dictionary, TSource element, Func<TSource, TKey> keySelector) where TKey : notnull
    {
        var key = keySelector(element);
        dictionary.TryGetValue(key, out var val);
        dictionary[key] = val + 1;
    }
    
    
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