namespace DKey.Algorithms.DataStructures.Ordered;

internal class RedBlackTreeNode<T>
{
    public T Key { get; set; }
    public RedBlackTreeNode<T>? Left { get; set; }
    public RedBlackTreeNode<T>? Right { get; set; }
    public RedBlackTreeNode<T>? Parent { get; set; }
    public bool IsRed { get; set; }
    
    public int LeftSubTreeSize { get; set; }

    public RedBlackTreeNode(T key)
    {
        Key = key;
        IsRed = true;
    }
}