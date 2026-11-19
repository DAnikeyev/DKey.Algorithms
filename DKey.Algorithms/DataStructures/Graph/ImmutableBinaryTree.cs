using DKey.Algorithms.NumberTheory;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Initialized in Init().

namespace DKey.Algorithms.DataStructures.Graph;

/// <summary>
/// Use this for Immutable trees, like SegmentTree.
/// Fast access to the tree elements, but rotations/insertion/deletion can't be implemented fast,
/// as it requires to store tree of nodes, instead of array.
/// </summary>
public abstract class ImmutableBinaryTree<T>
{
    /// <summary>Number of leaves the tree was built from.</summary>
    public int Size;
    /// <summary>Index in <see cref="TreeModel"/> of the first leaf.</summary>
    public int Start;
    /// <summary>The array-backed tree: internal nodes at [0..Start-1], leaves at [Start..].</summary>
    public T[] TreeModel;
    /// <summary>Per-node inclusive range of leaf indices it covers.</summary>
    public (int left, int right)[] LeafRanges;

    /// <summary>Builds the tree over <paramref name="data"/>: leaves first, then every
    /// internal node as <see cref="Add"/> of its children. Complexity: O(n).</summary>
    public void Init(IList<T> data)
    {
        Size = data.Count;
        var boxSize = 1 << BinaryArithmetics.GetCeilingLog(data.Count);
        TreeModel = new T[boxSize * 2 - 1];
        LeafRanges = new (int left, int right)[boxSize * 2 - 1];
        Start = boxSize - 1;
        for (var i = 0; i < data.Count; i++)
        {
            TreeModel[Start + i] = data[i];
            LeafRanges[Start + i] = (i, i);
        }

        for (var i = data.Count; i < boxSize; i++)
        {
            TreeModel[Start + i] = Neutral();
            LeafRanges[Start + i] = (i, i);
        }

        for (var i = Start - 1; i >= 0; i--)
        {
            TreeModel[i] = Add(TreeModel[LeftChild(i)], TreeModel[RightChild(i)]);
            LeafRanges[i] = (LeafRanges[LeftChild(i)].left, LeafRanges[RightChild(i)].right);
        }
    }

    /// <summary>Replaces the leaf at <paramref name="index"/> and recomputes its ancestors.
    /// Complexity: O(log n).</summary>
    public void Set(int index, T value)
    {
        index += Start;
        TreeModel[index] = value;

        while (index > 0)
        {
            index = Parent(index);
            TreeModel[index] = Add(TreeModel[LeftChild(index)], TreeModel[RightChild(index)]);
        }
    }

    /// <summary>Cumulative <see cref="Add"/> over the leaf range
    /// [<paramref name="left"/>, <paramref name="right"/>]. Complexity: O(log n).</summary>
    public virtual T GetCumulativeOperation(int left, int right)
    {
        return GetSumHelper(left, right, 0);
    }

    private T GetSumHelper(int left, int right, int current)
    {
        var (rangeLeft, rangeRight) = LeafRanges[current];
        if (left > rangeRight || right < rangeLeft)
        {
            return Neutral();
        }

        if (left <= rangeLeft && right >= rangeRight)
        {
            return TreeModel[current];
        }

        T leftSum = GetSumHelper(left, right, LeftChild(current));
        T rightSum = GetSumHelper(left, right, RightChild(current));

        return Add(leftSum, rightSum);
    }

    /// <summary>Index of the left child of the node at <paramref name="i"/>.</summary>
    public int LeftChild(int i) => i * 2 + 1;
    /// <summary>Index of the right child of the node at <paramref name="i"/>.</summary>
    public int RightChild(int i) => i * 2 + 2;
    /// <summary>Index of the parent of the node at <paramref name="i"/>.</summary>
    public int Parent(int i) => (i - 1) / 2;
    /// <summary>The combining operation the tree is built for (sum, min, max, …).</summary>
    protected abstract T Add(T a, T b);
    /// <summary>The operation's neutral element, used to pad the leaf array to a power of two.</summary>
    protected abstract T Neutral();
}
