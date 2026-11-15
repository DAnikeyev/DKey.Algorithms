namespace DKey.Algorithms.Search;

/// <summary>
/// Search helpers over sorted and unsorted data.
/// </summary>
public class SortedDataSearch
{
    /// <summary>
    /// Returns the index of the element with the maximal selected value; the first occurrence wins on ties.
    /// </summary>
    /// <param name="source">Input sequence.</param>
    /// <param name="valueSelector">Selects the comparable value to maximize.</param>
    public static int GetIndexOfMaxBy<TSource, TKey>
        (IEnumerable<TSource> source, Func<TSource, TKey> valueSelector) where TKey : IComparable<TKey>
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
    /// valueSelector must be monotonic increasing in source.
    /// </summary>
    /// <param name="source">Sorted input list.</param>
    /// <param name="min">Exclusive lower bound of the interval.</param>
    /// <param name="max">Exclusive upper bound of the interval.</param>
    /// <param name="valueSelector">Monotonically increasing value selector.</param>
    /// <returns>Index of the first element with value in (min, max), or -1 when there is none.</returns>
    public static int GetFirstIndexInSortedListInInterval<TSource>
        (IList<TSource> source, int min, int max, Func<TSource, long> valueSelector)
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
