namespace DKey.Algorithms.DataStructures;

public class SortedArray<T> where T : IComparable<T>
{
    private T[] sortedArray;

    public SortedArray(T[] inputArray)
    {
        sortedArray = (T[])inputArray.Clone();
        Array.Sort(sortedArray);
    }

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