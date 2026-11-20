namespace DKey.Algorithms.DataStructures.Ordered;

/// <summary>A <see cref="RedBlackTree{T}"/> of longs with the default comparer.</summary>
public class RBTLong : RedBlackTree<long>
{
    /// <summary>Creates an empty tree.</summary>
    public RBTLong() : base(Comparer<long>.Default)
    {
    }
}
