using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace DKey.Algorithms.Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
[MaxIterationCount(20)]
[MaxWarmupCount(20)]
public class SandboxBenchmark
{
    private const int N = 193876;

    [Benchmark]
    public void ToBinary()
    {
        Algorithms.NumberTheory.BinaryArithmetics.Convert(N);
    }
}