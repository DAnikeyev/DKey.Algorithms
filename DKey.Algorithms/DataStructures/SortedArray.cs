namespace DKey.Algorithms.DataStructures;

/// <summary>
/// Immutable sorted array with strict "count of elements strictly greater
/// than x" queries via binary search.
/// </summary>
public class SortedArray<T> where T : IComparable<T>
{
    private T[] sortedArray;

    /// <summary>Copies and sorts the input. Complexity: O(n log n).</summary>
    public SortedArray(T[] inputArray)
    {
        sortedArray = (T[])inputArray.Clone();
        Array.Sort(sortedArray);
    }

    /// <summary>Number of elements strictly greater than <paramref name="x"/>.
    /// Complexity: O(log n) + O(k) scan over equal elements.</summary>
    public int CountGreaterThan(T x)
    {
        int index = Array.BinarySearch(sortedArray, x);

        if (index < 0)
        {
            index = ~index;
        }
        else
        {
            while (index < sortedArray.Length && x.CompareTo(sortedArray[index]) >= 0)
            {
                index++;
            }
        }

        return sortedArray.Length - index;
    }
}
