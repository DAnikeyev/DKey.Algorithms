using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using DKey.Algorithms.Benchmark;

var config = ManualConfig.Create(DefaultConfig.Instance)
    .WithBuildTimeout(TimeSpan.FromMinutes(8));

BenchmarkRunner.Run(new[]
{
    typeof(OrderedStructuresBenchmark),
    typeof(SegmentTreeBenchmark),
    typeof(SuffixTreeBenchmark),
    typeof(SearchBenchmark),
    typeof(SandboxBenchmark),
}, config);
