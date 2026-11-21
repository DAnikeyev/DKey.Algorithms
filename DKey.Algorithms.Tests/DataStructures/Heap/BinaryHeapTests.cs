using DKey.Algorithms.DataStructures.Heap;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.DataStructures.Heap;

[TestFixture]
public class BinaryHeapTests
{
    [Test]
    public void PushPop_ReturnsSortedOrder()
    {
        var heap = new BinaryHeap<int>();
        foreach (var value in new[] { 5, 1, 4, 1, 8, 2, 7 })
        {
            heap.Push(value);
        }

        var popped = new List<int>();
        while (heap.Count > 0)
        {
            popped.Add(heap.Pop());
        }

        ClassicAssert.IsTrue(new[] { 1, 1, 2, 4, 5, 7, 8 }.SequenceEqual(popped));
    }

    [Test]
    public void Duplicates_AreIndependentEntries()
    {
        var heap = new BinaryHeap<int>();
        var h1 = heap.Push(3);
        var h2 = heap.Push(3);

        ClassicAssert.AreEqual(2, heap.Count);
        ClassicAssert.IsTrue(heap.TryDecreaseKey(h1, 1));
        ClassicAssert.AreEqual(1, heap.Pop());
        ClassicAssert.AreEqual(3, heap.Pop());
        Assert.That(h1, Is.Not.EqualTo(h2));
    }

    [Test]
    public void PeekPop_Empty_Throws()
    {
        var heap = new BinaryHeap<int>();

        Assert.That((Action)(() => heap.Peek()), Throws.InvalidOperationException);
        Assert.That((Action)(() => heap.Pop()), Throws.InvalidOperationException);
    }

    [Test]
    public void MaxHeap_ViaReversedComparer()
    {
        var heap = new BinaryHeap<int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
        foreach (var value in new[] { 5, 1, 4, 8, 2 })
        {
            heap.Push(value);
        }

        ClassicAssert.AreEqual(8, heap.Pop());
        ClassicAssert.AreEqual(5, heap.Pop());
    }

    [Test]
    public void DecreaseKey_MakesNewMinimum_VisibleInPeek()
    {
        var heap = new BinaryHeap<int>();
        heap.Push(10);
        heap.Push(20);
        var handle = heap.Push(30);

        ClassicAssert.AreEqual(10, heap.Peek());
        ClassicAssert.IsTrue(heap.TryDecreaseKey(handle, 5));
        ClassicAssert.AreEqual(5, heap.Peek());
        ClassicAssert.AreEqual(5, heap.Pop());
        ClassicAssert.AreEqual(10, heap.Pop());
    }

    [Test]
    public void DecreaseKey_LargerOrEqualValue_RejectedWithoutChange()
    {
        var heap = new BinaryHeap<int>();
        var handle = heap.Push(7);

        ClassicAssert.IsFalse(heap.TryDecreaseKey(handle, 8));
        ClassicAssert.IsFalse(heap.TryDecreaseKey(handle, 7));
        ClassicAssert.AreEqual(7, heap.Peek());
    }

    [Test]
    public void DecreaseKey_DeadHandle_Rejected()
    {
        var heap = new BinaryHeap<int>();
        var handle = heap.Push(1);
        heap.Pop();

        ClassicAssert.IsFalse(heap.TryDecreaseKey(handle, 0));
        ClassicAssert.IsFalse(heap.TryDecreaseKey(-1, 0));
        ClassicAssert.IsFalse(heap.TryDecreaseKey(12345, 0));
    }

    [Test]
    public void HeapInvariant_HoldsAfterEveryOperation()
    {
        var heap = new BinaryHeap<int>();
        var mirror = new List<int>();
        var knownHandles = new List<int>();
        var generator = ListGenerator.Instance(seed: 20261121);

        int? TryGet(int handle)
        {
            try
            {
                return heap.GetValue(handle);
            }
            catch (InvalidOperationException)
            {
                return null; // handle already popped — Pop does not report handles
            }
        }

        for (var op = 0; op < 2_000; op++)
        {
            var action = generator.RandomList(1, 0, 2)[0];
            if (action == 0 || heap.Count == 0)
            {
                var value = generator.RandomList(1, 0, 100)[0];
                knownHandles.Add(heap.Push(value));
                mirror.Add(value);
            }
            else if (action == 1)
            {
                var popped = heap.Pop();
                mirror.Remove(popped);
            }
            else if (knownHandles.Count > 0)
            {
                var index = generator.RandomList(1, 0, knownHandles.Count - 1)[0];
                var handle = knownHandles[index];
                var current = TryGet(handle);
                if (current is null)
                {
                    knownHandles.RemoveAt(index);
                    continue;
                }

                var newValue = generator.RandomList(1, 0, 20)[0];
                if (heap.TryDecreaseKey(handle, newValue))
                {
                    mirror.Remove(current.Value);
                    mirror.Add(newValue);
                }
            }

            if (mirror.Count > 0)
            {
                ClassicAssert.AreEqual(mirror.Min(), heap.Peek(), $"op {op}: heap minimum diverged from mirror");
            }
        }
    }

    [Test]
    public void HundredThousandOps_MatchSortedListReference()
    {
        const int operations = 100_000;
        var heap = new BinaryHeap<int>();
        var reference = new SortedList<int, int>(); // value -> count (duplicates fold)
        var knownHandles = new List<int>();
        var generator = ListGenerator.Instance(seed: 20261122);
        var pushes = 0;
        var decreases = 0;

        int? TryGet(int handle)
        {
            try
            {
                return heap.GetValue(handle);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        void RemoveFromReference(int value)
        {
            if (reference[value] == 1)
            {
                reference.Remove(value);
            }
            else
            {
                reference[value]--;
            }
        }

        for (var op = 0; op < operations; op++)
        {
            var action = generator.RandomList(1, 0, 9)[0];
            if (action < 5 || reference.Count == 0)
            {
                var value = generator.RandomList(1, 0, 1_000)[0];
                knownHandles.Add(heap.Push(value));
                reference[value] = reference.GetValueOrDefault(value) + 1;
                pushes++;
            }
            else if (action < 9)
            {
                var expected = reference.Keys[0];
                ClassicAssert.AreEqual(expected, heap.Pop(), $"op {op}: pop mismatch");
                RemoveFromReference(expected);
            }
            else if (knownHandles.Count > 0)
            {
                var index = generator.RandomList(1, 0, knownHandles.Count - 1)[0];
                var handle = knownHandles[index];
                var current = TryGet(handle);
                if (current is null)
                {
                    knownHandles.RemoveAt(index);
                    continue;
                }

                var newValue = generator.RandomList(1, 0, 100)[0];
                if (heap.TryDecreaseKey(handle, newValue))
                {
                    RemoveFromReference(current.Value);
                    reference[newValue] = reference.GetValueOrDefault(newValue) + 1;
                    decreases++;
                }
            }
        }

        ClassicAssert.Greater(pushes, 0);
        ClassicAssert.Greater(decreases, 0);
        while (heap.Count > 0)
        {
            ClassicAssert.AreEqual(reference.Keys[0], heap.Pop());
            RemoveFromReference(reference.Keys[0]);
        }

        ClassicAssert.AreEqual(0, reference.Count);
    }
}
