using DKey.Algorithms.NumberTheory;

namespace DKey.Algorithms.DataStructures.Graph;

/// <summary>
/// Use this for Immutable trees, like SegmentTree.
/// Fast access to the tree elements, but rotations/insertion/deletion can't be implemented fast,
/// as it requires to store tree of nodes, instead of array.
/// </summary>
public abstract class ImmutableBinaryTree<T>
{
    public int Size;
    public int Start;
    public T[] TreeModel;
    public (int left, int right)[] LeafRanges;
    
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
            TreeModel[Start +i] = Neutral();
            LeafRanges[Start +i] = (i, i);
        }

        for (var i = Start - 1; i >= 0; i--)
        {
            TreeModel[i] = Add(TreeModel[LeftChild(i)], TreeModel[RightChild(i)]);
            LeafRanges[i] = (LeafRanges[LeftChild(i)].left, LeafRanges[RightChild(i)].right);
        }
    }
    
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
    
    public int LeftChild(int i) => i * 2 + 1;
    public int RightChild(int i) => i * 2 + 2;
    public int Parent(int i) => (i - 1) / 2;
    protected abstract T Add(T a, T b);
    protected abstract T Neutral();
}