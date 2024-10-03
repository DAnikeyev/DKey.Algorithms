namespace DKey.Algorithms.DataStructures.Ordered;

public class RBTString : RedBlackTree<string>
{
    public RBTString() : base(Comparer<string>.Default)
    {
    }
}