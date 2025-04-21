namespace DKey.Algorithms.DataStructures.Ordered;

public interface IOrderedDataStructureWithValues<T>
{
    public IEnumerable<T> Keys { get; }
    
    public bool Contains(T item);
    
    public int Count { get; }
    
    public bool TryAdd(T item);

    public bool TryDelete (T item);
    
    public T Min { get; }
    
    public T Max { get; }

    public (int index, T? item) Search(T item, out bool found);
}