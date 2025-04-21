namespace DKey.Algorithms.DataStructures.Ordered;

public class RBTInt : RedBlackTree<int>
{
    public RBTInt() : base(Comparer<int>.Default)
    {
    }
}