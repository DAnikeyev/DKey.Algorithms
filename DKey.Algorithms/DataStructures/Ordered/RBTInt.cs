namespace DKey.Algorithms.DataStructures.Ordered;

/// <summary>A <see cref="RedBlackTree{T}"/> of ints with the default comparer.</summary>
public class RBTInt : RedBlackTree<int>
{
    /// <summary>Creates an empty tree.</summary>
    public RBTInt() : base(Comparer<int>.Default)
    {
    }
}
