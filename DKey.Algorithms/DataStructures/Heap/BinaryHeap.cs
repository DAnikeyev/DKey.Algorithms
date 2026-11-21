namespace DKey.Algorithms.DataStructures.Heap;

/// <summary>
/// Min-binary-heap with handle-tracked positions: besides Push/Pop/Peek it
/// supports <see cref="TryDecreaseKey"/> in O(log n) — the operation the BCL
/// <c>PriorityQueue&lt;T,E&gt;</c> lacks. Elements are identified by integer
/// handles returned from <see cref="Push"/>, which keeps duplicate values
/// independent (each handle is its own heap entry).
/// </summary>
public class BinaryHeap<T>
{
    private readonly IComparer<T> _comparer;
    private readonly List<int> _slots = new();        // heap slot -> handle
    private readonly List<T> _values = new();         // handle -> value
    private readonly List<int> _position = new();     // handle -> heap slot, -1 when popped

    /// <summary>
    /// Creates an empty min-heap under <paramref name="comparer"/> (defaults
    /// to <see cref="Comparer{T}.Default"/>; pass a reversed comparer for a
    /// max-heap).
    /// </summary>
    public BinaryHeap(IComparer<T>? comparer = null)
    {
        _comparer = comparer ?? Comparer<T>.Default;
    }

    /// <summary>Number of live elements. Duplicates count separately.</summary>
    public int Count => _slots.Count;

    /// <summary>
    /// Adds <paramref name="item"/> and returns its handle for later
    /// <see cref="TryDecreaseKey"/> calls. Handles are unique and never
    /// reused; internal per-handle bookkeeping grows with the total number of
    /// pushes. Complexity: O(log n) amortized.
    /// </summary>
    public int Push(T item)
    {
        var handle = _values.Count;
        _values.Add(item);
        _position.Add(_slots.Count);
        _slots.Add(handle);
        SiftUp(_slots.Count - 1);
        return handle;
    }

    /// <summary>
    /// The current minimum without removing it. Throws
    /// <see cref="InvalidOperationException"/> when empty.
    /// Complexity: O(1).
    /// </summary>
    public T Peek()
    {
        if (_slots.Count == 0)
        {
            throw new InvalidOperationException("The heap is empty.");
        }

        return _values[_slots[0]];
    }

    /// <summary>
    /// Removes and returns the minimum (ties broken by heap order, not
    /// deterministically). Throws <see cref="InvalidOperationException"/> when
    /// empty. Complexity: O(log n).
    /// </summary>
    public T Pop()
    {
        if (_slots.Count == 0)
        {
            throw new InvalidOperationException("The heap is empty.");
        }

        var handle = _slots[0];
        var result = _values[handle];
        SwapSlots(0, _slots.Count - 1);
        _slots.RemoveAt(_slots.Count - 1);
        _position[handle] = -1;
        SiftDown(0);
        return result;
    }

    /// <summary>
    /// The current value stored under <paramref name="handle"/>. Throws
    /// <see cref="InvalidOperationException"/> for a dead handle (popped or
    /// never existed). Complexity: O(1).
    /// </summary>
    public T GetValue(int handle)
    {
        if (handle < 0 || handle >= _position.Count || _position[handle] < 0)
        {
            throw new InvalidOperationException($"Handle {handle} is not live.");
        }

        return _values[handle];
    }

    /// <summary>
    /// Decreases the value stored under <paramref name="handle"/> to
    /// <paramref name="newValue"/> and restores the heap order.
    /// Complexity: O(log n).
    /// </summary>
    /// <returns>true when the value was decreased; false when the handle is
    /// dead (already popped / never existed) or <paramref name="newValue"/>
    /// does not compare smaller than the stored value — in both cases the
    /// heap is unchanged.</returns>
    public bool TryDecreaseKey(int handle, T newValue)
    {
        if (handle < 0 || handle >= _position.Count || _position[handle] < 0)
        {
            return false;
        }

        var slot = _position[handle];
        if (_comparer.Compare(newValue, _values[handle]) >= 0)
        {
            return false;
        }

        _values[handle] = newValue;
        SiftUp(slot);
        return true;
    }

    private void SiftUp(int slot)
    {
        while (slot > 0)
        {
            var parent = (slot - 1) / 2;
            if (_comparer.Compare(_values[_slots[slot]], _values[_slots[parent]]) >= 0)
            {
                break;
            }

            SwapSlots(slot, parent);
            slot = parent;
        }
    }

    private void SiftDown(int slot)
    {
        while (true)
        {
            var left = slot * 2 + 1;
            var right = slot * 2 + 2;
            var smallest = slot;
            if (left < _slots.Count && _comparer.Compare(_values[_slots[left]], _values[_slots[smallest]]) < 0)
            {
                smallest = left;
            }

            if (right < _slots.Count && _comparer.Compare(_values[_slots[right]], _values[_slots[smallest]]) < 0)
            {
                smallest = right;
            }

            if (smallest == slot)
            {
                return;
            }

            SwapSlots(slot, smallest);
            slot = smallest;
        }
    }

    private void SwapSlots(int a, int b)
    {
        (_slots[a], _slots[b]) = (_slots[b], _slots[a]);
        _position[_slots[a]] = a;
        _position[_slots[b]] = b;
    }
}
