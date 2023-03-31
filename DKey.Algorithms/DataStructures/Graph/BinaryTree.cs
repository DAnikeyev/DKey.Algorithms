using DKey.Algorithms.NumberTheory;

namespace DKey.Algorithms.DataStructures.Graph;

public abstract class BinaryTree<T>
{
    public int size;
    public int start;

    public T[] TreeModel;
    public BinaryTree(IList<T> data)
    {
        size = data.Count;
        var boxSize = 1 << BinaryArithmetics.GetCeilingLog(data.Count);
        TreeModel = new T[boxSize * 2 - 1];
        start = boxSize - 1;
        for(var i = 0; i < data.Count; i++)
            TreeModel[start + i] = data[i];
        
        for(var i = start - 1; i >=0; i++)
            TreeModel[i] = Add(TreeModel[LeftChild(i)], TreeModel[RightChild(i)]);
    }
    
    public void Set(int index, T value)
    {
        index += start;
        TreeModel[index] = value;

        while (index > 0)
        {
            index = Parent(index);
            TreeModel[index] = Add(TreeModel[LeftChild(index)], TreeModel[RightChild(index)]);
        }
    }

    public T GetSum(int left, int right)
    {
        return GetSumHelper(left, right, 0, start, start + 1);
    }

    private T GetSumHelper(int left, int right, int current, int rangeLeft, int rangeRight)
    {
        if (left >= rangeRight || right <= rangeLeft)
        {
            return Neutral();
        }

        if (left <= rangeLeft && right >= rangeRight)
        {
            return TreeModel[current];
        }

        int mid = (rangeLeft + rangeRight) / 2;
        T leftSum = GetSumHelper(left, right, LeftChild(current), rangeLeft, mid);
        T rightSum = GetSumHelper(left, right, RightChild(current), mid, rangeRight);

        return Add(leftSum, rightSum);
    }
    
    public int LeftChild(int i) => i * 2 + 1;
    public int RightChild(int i) => i * 2 + 2;
    public int Parent(int i) => (i - 1) / 2;
    protected abstract T Add(T a, T b);
    protected abstract T Neutral();
}