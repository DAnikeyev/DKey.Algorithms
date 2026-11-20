namespace DKey.Algorithms.DataStructures.Ordered;

/// <summary>A <see cref="RedBlackTree{T}"/> of strings with the default comparer.</summary>
public class RBTString : RedBlackTree<string>
{
    /// <summary>Creates an empty tree.</summary>
    public RBTString() : base(Comparer<string>.Default)
    {
    }
}
