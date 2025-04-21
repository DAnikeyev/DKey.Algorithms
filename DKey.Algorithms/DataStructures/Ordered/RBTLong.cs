namespace DKey.Algorithms.DataStructures.Ordered;

public class RBTLong : RedBlackTree<long>
{
    public RBTLong() : base(Comparer<long>.Default)
    {
    }
}