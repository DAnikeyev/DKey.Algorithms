using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using DKey.Algorithms.DataStructures.SegmentTree;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Benchmark;

/// <summary>
/// IntegerSegmentTree (sum with lazy range add) vs naive array loops:
/// pure interval-sum queries on a prebuilt tree, and an end-to-end
/// build + batch range-add comparison.
/// </summary>
[ShortRunJob]
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class SegmentTreeBenchmark
{
    private const int Size = 100_000;
    private const int Queries = 100;

    private List<int> _data = null!;
    private (int l, int r)[] _ranges = null!;
    private IntegerSegmentTree _tree = null!;

    [GlobalSetup]
    public void Setup()
    {
        var gen = ListGenerator.Instance(seed: 20261121);
        _data = gen.RandomList(Size, 1, 1_000);
        _ranges = gen.RandomList(Queries, 0, Size - 1)
            .Zip(gen.RandomList(Queries, 0, Size - 1))
            .Select(p => (Math.Min(p.First, p.Second), Math.Max(p.First, p.Second)))
            .ToArray();
        _tree = new IntegerSegmentTree();
        _tree.InitFromIntList(_data);
    }

    [Benchmark]
    public long SegmentTree_RangeSum()
    {
        long total = 0;
        foreach (var (l, r) in _ranges)
            total += _tree.GetCumulativeOperation(l, r).Value;
        return total;
    }

    [Benchmark]
    public long Naive_RangeSum()
    {
        long total = 0;
        foreach (var (l, r) in _ranges)
        {
            var sum = 0;
            for (var i = l; i <= r; i++) sum += _data[i];
            total += sum;
        }
        return total;
    }

    [Benchmark]
    public long SegmentTree_InitAndRangeAdd()
    {
        var tree = new IntegerSegmentTree();
        tree.InitFromIntList(_data);
        foreach (var (l, r) in _ranges)
            tree.AddToInterval(l, r, 7);
        return tree.GetCumulativeOperation(0, Size - 1).Value;
    }

    [Benchmark]
    public long Naive_CopyAndRangeAdd()
    {
        var data = _data.ToArray();
        foreach (var (l, r) in _ranges)
            for (var i = l; i <= r; i++) data[i] += 7;
        return data[Size - 1];
    }
}
