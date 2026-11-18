namespace DKey.Algorithms;

/// <summary>
/// Tuple helpers for (First, Second) pairs: swap the members and pick the
/// maximum by comparison. Both operations are pure and generic.
/// </summary>
public class TupleHelper
{
    /// <summary>Returns the tuple with First and Second exchanged.</summary>
    public static (T First, T Second) Swap<T>((T First, T Second) tuple)
    {
        return (tuple.Second, tuple.First);
    }

    /// <summary>Returns the tuple whose First is not smaller than its Second —
    /// the members end up ordered descending.</summary>
    public static (T First, T Second) SelectMax<T>((T First, T Second) tuple)
        where T : IComparable<T>
    {
        return tuple.First.CompareTo(tuple.Second) > 0 ? tuple : Swap(tuple);
    }
}
