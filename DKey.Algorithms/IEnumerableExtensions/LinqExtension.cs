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
    
    public static int GetIndexOfMaxBy<TSource, TKey>
        (this IEnumerable<TSource> source, Func<TSource, TKey> valueSelector) where TKey : IComparable<TKey>
    {
        if (source.FirstOrDefault() == null)
            return -1;
        var index = 0;
        var maxindex = 0;
        var max = valueSelector(source.First());
        foreach (TSource element in source)
        {
            var currentValue = valueSelector(element);
            if (currentValue.CompareTo(max) > 0)
            {
                max = currentValue;
                maxindex = index;
            }

            index++;
        }
        return maxindex;
    }
    
    //ValueSelector(item) must be monotonic increasing in source
    public static int GetFirstIndexInSortedListInInterval<TSource>
        (this IList<TSource> source, int min, int max, Func<TSource, long> valueSelector)
    {
        var n = source.Count();
        var maxindex = (int)BinarySearch.GetIndexLong(0, n - 1, x => valueSelector(source[x]) - max > 0 ? 1 : -1);
        var minindex = (int)BinarySearch.GetIndexLong(0, n - 1, x => valueSelector(source[x]) - min > 0 ? 1 : -1) - 1;
        minindex = Math.Max(minindex, 0);
        for (var i = minindex; i <= maxindex; i++)
        {
            if (valueSelector(source[i]) > min && valueSelector(source[i]) < max)
            {
                return i;
            }
        }
        return -1;
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