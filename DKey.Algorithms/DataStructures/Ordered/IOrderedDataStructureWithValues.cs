namespace DKey.Algorithms.DataStructures.Ordered;

/// <summary>
/// An ordered key structure (e.g. <see cref="RedBlackTree{T}"/>): sorted
/// iteration, membership and rank queries over distinct keys.
/// </summary>
public interface IOrderedDataStructureWithValues<T>
{
    /// <summary>All keys in sorted order.</summary>
    public IEnumerable<T> Keys { get; }

    /// <summary>True when the key is present in the structure.</summary>
    public bool Contains(T item);

    /// <summary>Number of distinct keys stored.</summary>
    public int Count { get; }

    /// <summary>Adds the key; false when it was already present.</summary>
    public bool TryAdd(T item);

    /// <summary>Deletes the key; false when it was absent.</summary>
    public bool TryDelete(T item);

    /// <summary>The smallest key. Throws when the structure is empty.</summary>
    public T Min { get; }

    /// <summary>The largest key. Throws when the structure is empty.</summary>
    public T Max { get; }

    /// <summary>Locates <paramref name="item"/> and reports how many stored keys
    /// are strictly smaller (its rank). <paramref name="found"/> reports presence;
    /// the item is default(T) (null for reference key types) when absent.</summary>
    public (int index, T? item) Search(T item, out bool found);
}
