using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using DKey.Algorithms.ArgumentSearch;
using DKey.Algorithms.Search;

namespace DKey.Algorithms.Benchmark;

/// <summary>
/// BinarySearch/TernarySearch over integer predicates vs linear scans.
/// </summary>
[ShortRunJob]
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class SearchBenchmark
{
    private const int Range = 1_000_000;

    [Benchmark]
    public int BinarySearch_FirstXWithXSquareOverN()
    {
        return BinarySearch.GetIndexLong(0, Range, x => (long)x * x > 999_999_000 ? 1 : -1);
    }

    [Benchmark]
    public int LinearScan_FirstXWithXSquareOverN()
    {
        for (var x = 0; x <= Range; x++)
            if ((long)x * x > 999_999_000)
                return x;
        return Range + 1;
    }

    [Benchmark]
    public int TernarySearch_UnimodalMinimum()
    {
        return TernarySearch.GetIndexLong(0, Range, x => (long)(x - 500_000) * (x - 500_000));
    }

    [Benchmark]
    public int LinearScan_UnimodalMinimum()
    {
        var best = long.MaxValue;
        var bestX = 0;
        for (var x = 0; x <= Range; x++)
        {
            var value = (long)(x - 500_000) * (x - 500_000);
            if (value < best)
            {
                best = value;
                bestX = x;
            }
        }
        return bestX;
    }
}
