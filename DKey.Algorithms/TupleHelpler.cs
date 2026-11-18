namespace DKey.Algorithms;

/// <summary>
/// Obsolete misspelled alias of <see cref="TupleHelper"/>: every member
/// forwards to the canonical implementation. Bridge removal is reserved for
/// a future major version — do not extend it.
/// </summary>
[Obsolete("Typo — use TupleHelper instead. The bridge is removed in a future major version.")]
public class TupleHelpler
{
    /// <summary>Swaps First and Second; forwards to <see cref="TupleHelper.Swap"/>.</summary>
    public static (T First, T Second) Swap<T>((T First, T Second) tuple)
        => TupleHelper.Swap(tuple);

    /// <summary>Picks the tuple with the larger First; forwards to
    /// <see cref="TupleHelper.SelectMax"/>.</summary>
    public static (T First, T Second) SelectMax<T>((T First, T Second) tuple)
        where T : IComparable<T>
        => TupleHelper.SelectMax(tuple);
}
