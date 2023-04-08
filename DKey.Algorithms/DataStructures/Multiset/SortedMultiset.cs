namespace DKey.Algorithms.DataStructures.Multiset;

/// <typeparam name="T"></typeparam>
public class SortedMultiset<T> where T : IComparable<T>
{
    private T _min;
    private T _max;
    public long Count { get; private set; }
    internal readonly SortedDictionary<T, long> Multiset;

    public SortedMultiset()
    {
        Multiset = new();
    }
    
    public SortedMultiset(Dictionary<T, long> countDictionary)
    {
        Multiset = new SortedDictionary<T, long>(countDictionary);
        _min = _max = countDictionary.FirstOrDefault().Key;
        foreach (var item in countDictionary)
        {
            if(_min.CompareTo(item.Key) > 0)
                _min = item.Key;
            if(_max.CompareTo(item.Key) < 0)
                _max = item.Key;
            Count += item.Value;
        }
    }

    /// <summary>
    /// Add item to multiset.
    /// </summary>
    /// <param name="item">Item.</param>
    /// <param name="value">How many times to add item.</param>
    public void Add(T item, long value = 1)
    {
        if (Count == 0)
            _min = _max = item;
        Multiset.TryGetValue(item, out var count);
        Multiset[item] = count + value;
        Count += value;
        if (count == 0)
        {
            if(_min.CompareTo(item) > 0)
                _min = item;
            if(_max.CompareTo(item) < 0)
                _max = item;
        }
    }

    /// <summary>
    /// Returns false if amount of items was less then value.
    /// </summary>
    public bool Remove(T item, long value = 1)
    {
        if(!Multiset.TryGetValue(item, out var count))
            return false;
        var removeAmount = Math.Min(count, value);
        Multiset[item]-= removeAmount;
        Count-= removeAmount;
        if (count <= value)
        {
            Multiset.Remove(item);
            if(_max.CompareTo(item)<=0)
                _max = (Multiset.Keys.Count > 0 ? Multiset.Keys.Max() : default(T))!;
            if(_min.CompareTo(item)>=0)
                _min = (Multiset.Keys.Count > 0 ? Multiset.Keys.Min() : default(T))!;
        }

        return count >= value;
    }
    
    /// <summary>
    /// Get distinct items of multiset.
    /// </summary>
    public SortedDictionary<T,long>.KeyCollection GetDistinctItems()
    {
        return Multiset.Keys;
    }

    /// <summary>
    /// Returns false if no such key existed. 
    /// </summary>
    public bool TryGetValue(T key, out long count)
    {
        var hasKey = Multiset.TryGetValue(key, out var setCount);
        count = setCount;
        return hasKey;
    }

    public T Min => _min;
    public T Max => _max;
    public int CountDistinct => Multiset.Count();
}