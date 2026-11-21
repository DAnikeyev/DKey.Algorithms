using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using DKey.Algorithms.DataStructures.Ordered;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Benchmark;

/// <summary>
/// RedBlackTree (via the RBTInt preset) vs the BCL SortedSet&lt;int&gt;.
/// </summary>
[SimpleJob(warmupCount: 5, iterationCount: 15)]
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class OrderedStructuresBenchmark
{
    private const int BuildSize = 10_000;
    private const int LookupCount = 1_000;

    private int[] _values = null!;
    private int[] _lookupKeys = null!;
    private RBTInt _tree = null!;
    private SortedSet<int> _sortedSet = null!;

    [GlobalSetup]
    public void Setup()
    {
        var gen = RandomData.ListGenerator.Instance(seed: 20261121);
        _values = gen.RandomPermutation(200_000).Take(BuildSize).ToArray();
        _lookupKeys = gen.RandomList(LookupCount, 0, 200_000).ToArray();
        _tree = new RBTInt();
        foreach (var value in _values) _tree.TryAdd(value);
        _sortedSet = new SortedSet<int>(_values);
    }

    [Benchmark]
    public int RBT_Add()
    {
        var tree = new RBTInt();
        foreach (var value in _values) tree.TryAdd(value);
        return tree.Count;
    }

    [Benchmark]
    public int SortedSet_Add()
    {
        var set = new SortedSet<int>();
        foreach (var value in _values) set.Add(value);
        return set.Count;
    }

    [Benchmark]
    public int RBT_Contains()
    {
        var found = 0;
        foreach (var key in _lookupKeys)
            if (_tree.Contains(key)) found++;
        return found;
    }

    [Benchmark]
    public int SortedSet_Contains()
    {
        var found = 0;
        foreach (var key in _lookupKeys)
            if (_sortedSet.Contains(key)) found++;
        return found;
    }
}
