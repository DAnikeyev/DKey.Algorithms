namespace DKey.Algorithms.Search;

public class SortedDataSearch
{
    public static int GetIndexOfMaxBy<TSource, TKey>
        ( IEnumerable<TSource> source, Func<TSource, TKey> valueSelector) where TKey : IComparable<TKey>
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
    
    
    /// <summary>
    /// Returns the index of the first element in the sorted list that is in the interval (min, max).
    /// </summary>
    /// <param name="valueSelector">must be monotonic increasing in source.</param>
    public static int GetFirstIndexInSortedListInInterval<TSource>
        ( IList<TSource> source, int min, int max, Func<TSource, long> valueSelector)
    {
        var n = source.Count();
        var maxindex = BinarySearch.GetIndexLong(0, n - 1, x => valueSelector(source[x]) - max > 0 ? 1 : -1);
        var minindex = BinarySearch.GetIndexLong(0, n - 1, x => valueSelector(source[x]) - min > 0 ? 1 : -1) - 1;
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
}