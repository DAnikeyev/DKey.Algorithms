namespace DKey.Algorithms.DataStructures.Ordered;

public class RedBlackTree<T> : IOrderedDataStructureWithValues<T>
{
    private RedBlackTreeNode<T>? _root;
    private int _count;
    private IComparer<T> _comparer;
    public IEnumerable<T> Keys => GetKeys();

    public RedBlackTree(IComparer<T> comparer)
    {
        _count = 0;
        _comparer = comparer;
    }

    private IEnumerable<T> GetKeys() => InOrderTraversal(_root);

    // As tree is well-balanced we won't get stack overflow exception.
    private IEnumerable<T> InOrderTraversal(RedBlackTreeNode<T>? node)
    {
        if (node != null)
        {
            foreach (var kv in InOrderTraversal(node.Left))
            {
                yield return kv;
            }

            yield return node.Key;
            foreach (var kv in InOrderTraversal(node.Right))
            {
                yield return kv;
            }
        }
    }

    public bool Contains(T item)
    {
        bool found;
        Search(item, out found);
        return found;
    }

    public int Count => _count;

    public bool TryAdd(T item)
    {
        var newNode = new RedBlackTreeNode<T>(item);
        if (_root == null)
        {
            _root = newNode;
            _root.IsRed = false;
            _count++;
            return true;
        }

        var current = _root;
        RedBlackTreeNode<T>? parent = null;

        var toAdd = new List<RedBlackTreeNode<T>>();
        while (current != null)
        {
            parent = current;
            if (_comparer.Compare(newNode.Key, current.Key) < 0)
            {
                toAdd.Add(current);
                current = current.Left;
            }
            else if (_comparer.Compare(newNode.Key, current.Key) > 0)
            {
                current = current.Right;
            }
            else
            {
                return false; // Duplicate values are not allowed
            }
        }

        foreach (var node in toAdd)
        {
            node.LeftSubTreeSize++;
        }

        newNode.Parent = parent;
        if (_comparer.Compare(newNode.Key, parent!.Key) < 0)
        {
            parent.Left = newNode;
        }
        else
        {
            parent.Right = newNode;
        }

        InsertFixup(newNode);
        _count++;
        return true;
    }

    public (int index, T? item) Search(T item, out bool found)
    {
        var current = _root;
        var index = 0;

        while (current != null)
        {
            var comparison = _comparer.Compare(item, current.Key);
            if (comparison == 0)
            {
                found = true;
                index += current.LeftSubTreeSize;
                return (index, current.Key);
            }
            if (comparison < 0)
            {
                current = current.Left;
            }
            else
            {
                index += current.LeftSubTreeSize + 1;
                current = current.Right;
            }
        }

        found = false;
        return (-1, default(T));
    }


    public bool TryDelete(T item)
    {
        
        var node = _root;

        var toRemove = new List<RedBlackTreeNode<T>>();
        while (node != null)
        {
            if (_comparer.Compare(item, node.Key) < 0)
            {
                toRemove.Add(node);
                node = node.Left;
            }
            else if (_comparer.Compare(item, node.Key) > 0)
            {
                node = node.Right;
            }
            else
            {
                break;
            }
        }
        
        if (node == null)
        {
            return false;
        }

        
        foreach (var n in toRemove)
        {
            n.LeftSubTreeSize--;
        }
        
        var v = node;
        var u = v.Left == null || v.Right == null ? v : GetSuccessor(v);
        var child = u.Left ?? u.Right;

        if (child != null)
        {
            child.Parent = u.Parent;
        }

        if (u.Parent == null)
        {
            _root = child;
        }
        else if (u == u.Parent.Left)
        {
            u.Parent.Left = child;
        }
        else
        {
            u.Parent.Right = child;
        }

        if (u != v)
        {
            v.Key = u.Key;
        }

        if (!u.IsRed)
        {
            FixDoubleBlack(child, u.Parent);
        }

        _count--;
        return true;
    }
    
    public T Min
    {
        get
        {
            var current = _root;
            while (current?.Left != null)
            {
                current = current.Left;
            }

            return current!.Key;
        }
    }
    
    public T Max
    {
        get
        {
            var current = _root;
            while (current?.Right != null)
            {
                current = current.Right;
            }

            return current!.Key;
        }
    }

    # region Core
    private RedBlackTreeNode<T> GetSuccessor(RedBlackTreeNode<T> node)
    {
        var current = node.Right;
        while (current?.Left != null)
        {
            current = current.Left;
        }

        return current!;
    }

    private void FixDoubleBlack(RedBlackTreeNode<T>? node, RedBlackTreeNode<T>? parent)
    {
        while (node != _root && (node == null || !node.IsRed))
        {
            if (node == parent?.Left)
            {
                var sibling = parent?.Right;
                if (sibling?.IsRed ?? false)
                {
                    sibling.IsRed = false;
                    parent!.IsRed = true;
                    RotateLeft(parent);
                    sibling = parent.Right;
                }

                if ((sibling?.Left == null || !sibling.Left.IsRed) && (sibling?.Right == null || !sibling.Right.IsRed))
                {
                    sibling!.IsRed = true;
                    node = parent;
                    parent = node!.Parent;
                }
                else
                {
                    if (sibling.Right == null || !sibling.Right.IsRed)
                    {
                        if (sibling.Left != null)
                        {
                            sibling.Left.IsRed = false;
                        }

                        sibling.IsRed = true;
                        RotateRight(sibling);
                        sibling = parent?.Right;
                    }

                    sibling!.IsRed = parent!.IsRed;
                    parent.IsRed = false;
                    if (sibling.Right != null)
                    {
                        sibling.Right.IsRed = false;
                    }

                    RotateLeft(parent);
                    node = _root;
                }
            }
            else
            {
                var sibling = parent?.Left;
                if (sibling?.IsRed ?? false)
                {
                    sibling.IsRed = false;
                    parent!.IsRed = true;
                    RotateRight(parent);
                    sibling = parent.Left;
                }

                if ((sibling?.Right == null || !sibling.Right.IsRed) && (sibling?.Left == null || !sibling.Left.IsRed))
                {
                    sibling!.IsRed = true;
                    node = parent;
                    parent = node?.Parent;
                }
                else
                {
                    if (sibling.Left == null || !sibling.Left.IsRed)
                    {
                        if (sibling.Right != null)
                        {
                            sibling.Right.IsRed = false;
                        }

                        sibling.IsRed = true;
                        RotateLeft(sibling);
                        sibling = parent?.Left;
                    }

                    sibling!.IsRed = parent!.IsRed;
                    parent.IsRed = false;
                    if (sibling.Left != null)
                    {
                        sibling.Left.IsRed = false;
                    }

                    RotateRight(parent);
                    node = _root;
                }
            }
        }

        if (node != null)
        {
            node.IsRed = false;
        }
    }

    private void RotateLeft(RedBlackTreeNode<T> node)
    {
        var right = node.Right!;
        node.Right = right.Left;
        if (right.Left != null)
        {
            right.Left.Parent = node;
        }

        right.Parent = node.Parent;
        if (node.Parent == null)
        {
            _root = right;
        }
        else if (node == node.Parent.Left)
        {
            node.Parent.Left = right;
        }
        else
        {
            node.Parent.Right = right;
        }

        right.Left = node;
        node.Parent = right;
        
        right.LeftSubTreeSize = right.LeftSubTreeSize + node.LeftSubTreeSize + 1;
    }

    private void RotateRight(RedBlackTreeNode<T> node)
    {
        var left = node.Left!;
        node.Left = left.Right;
        if (left.Right != null)
        {
            left.Right.Parent = node;
        }

        left.Parent = node.Parent;
        if (node.Parent == null)
        {
            _root = left;
        }
        else if (node == node.Parent.Right)
        {
            node.Parent.Right = left;
        }
        else
        {
            node.Parent.Left = left;
        }

        left.Right = node;
        node.Parent = left;
        
        node.LeftSubTreeSize = node.LeftSubTreeSize - left.LeftSubTreeSize - 1;
    }

    private void InsertFixup(RedBlackTreeNode<T> node)
    {
        while (node != _root && node.Parent!.IsRed)
        {
            if (node.Parent.Parent != null && node.Parent == node.Parent.Parent.Left)
            {
                var uncle = node.Parent.Parent.Right;
                if (uncle != null && uncle.IsRed)
                {
                    node.Parent.IsRed = false;
                    uncle.IsRed = false;
                    node.Parent.Parent.IsRed = true;
                    node = node.Parent.Parent;
                }
                else
                {
                    if (node == node.Parent.Right)
                    {
                        node = node.Parent;
                        RotateLeft(node);
                    }

                    node.Parent!.IsRed = false;
                    if (node.Parent.Parent != null)
                    {
                        node.Parent.Parent.IsRed = true;
                        RotateRight(node.Parent.Parent);
                    }
                }
            }
            else if (node.Parent.Parent != null)
            {
                var uncle = node.Parent.Parent.Left;
                if (uncle != null && uncle.IsRed)
                {
                    node.Parent.IsRed = false;
                    uncle.IsRed = false;
                    node.Parent.Parent.IsRed = true;
                    node = node.Parent.Parent;
                }
                else
                {
                    if (node == node.Parent.Left)
                    {
                        node = node.Parent;
                        RotateRight(node);
                    }

                    node.Parent!.IsRed = false;
                    if (node.Parent.Parent != null)
                    {
                        node.Parent.Parent.IsRed = true;
                        RotateLeft(node.Parent.Parent);
                    }
                }
            }
        }

        _root!.IsRed = false;
    }
    #endregion
}