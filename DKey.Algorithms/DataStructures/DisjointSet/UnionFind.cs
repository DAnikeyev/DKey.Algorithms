namespace DKey.Algorithms.DataStructures.DisjointSet;

/// <summary>
/// Disjoint-set union (union-find) over the integers 0..size-1 with union by
/// rank and path compression: near-constant amortized cost per operation.
/// </summary>
public class UnionFind
{
    private readonly int[] _parent;
    private readonly int[] _rank;

    /// <summary>
    /// Creates <paramref name="size"/> singleton sets.
    /// </summary>
    /// <param name="size">Number of elements; elements are 0..size-1.</param>
    public UnionFind(int size)
    {
        _parent = new int[size];
        _rank = new int[size];
        for (var i = 0; i < size; i++)
        {
            _parent[i] = i;
        }

        Count = size;
    }

    /// <summary>The number of elements the structure was created with.</summary>
    public int Size => _parent.Length;

    /// <summary>The number of disjoint sets (components) right now.</summary>
    public int Count { get; private set; }

    /// <summary>
    /// Finds the representative of the set containing <paramref name="x"/>.
    /// Compresses the traversed path to point at the representative.
    /// Complexity: O(α(n)) amortized (inverse Ackermann), effectively constant.
    /// </summary>
    /// <param name="x">Element index, 0..Size-1.</param>
    public int Find(int x)
    {
        var root = x;
        while (_parent[root] != root)
        {
            root = _parent[root];
        }

        while (_parent[x] != root)
        {
            var next = _parent[x];
            _parent[x] = root;
            x = next;
        }

        return root;
    }

    /// <summary>
    /// Merges the sets containing <paramref name="x"/> and <paramref name="y"/>
    /// (union by rank: the shallower tree is attached under the deeper root).
    /// Complexity: O(α(n)) amortized.
    /// </summary>
    /// <returns>true when two different sets were merged; false when the
    /// elements were already in the same set (no change made).</returns>
    public bool Union(int x, int y)
    {
        var rootX = Find(x);
        var rootY = Find(y);
        if (rootX == rootY)
        {
            return false;
        }

        if (_rank[rootX] < _rank[rootY])
        {
            (rootX, rootY) = (rootY, rootX);
        }

        _parent[rootY] = rootX;
        if (_rank[rootX] == _rank[rootY])
        {
            _rank[rootX]++;
        }

        Count--;
        return true;
    }

    /// <summary>
    /// Whether <paramref name="x"/> and <paramref name="y"/> belong to the same
    /// set. Complexity: O(α(n)) amortized.
    /// </summary>
    public bool AreConnected(int x, int y)
    {
        return Find(x) == Find(y);
    }
}
