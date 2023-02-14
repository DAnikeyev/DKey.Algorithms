namespace DKey.Algorithms.DataStructures.Graph.SuffixTree;

/// <summary>
/// Not Ready Yet
/// </summary>
/// <typeparam name="T"></typeparam>
public class SuffixTree<T> where T : IComparable
{
    public List<T> _data;

    public static SuffixTree<T> Build(List<T> data, T maxChar)
    {
        var tree = new SuffixTree<T>();
        tree._data = new List<T>(data);
        tree._data.Append(maxChar);
        return tree;
    }

    public void PointDown(T node)
    {
        
    }

    public bool Contains(List<T> data)
    {
        return true;
    }
}