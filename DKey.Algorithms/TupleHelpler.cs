namespace DKey.Algorithms;

public class TupleHelpler
{
    public static (T First, T Second) Swap<T>((T First, T Second) tuple)
    {
        return (tuple.Second, tuple.First);
    }

    public static (T First, T Second) SelectMax<T>((T First, T Second) tuple)
        where T : IComparable<T>
    {
        return tuple.First.CompareTo(tuple.Second) > 0 ? tuple : Swap(tuple);
    }
}