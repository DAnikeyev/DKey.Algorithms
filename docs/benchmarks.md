# Benchmarks

Current results for the core structures, measured with
[BenchmarkDotNet v0.15.8](https://benchmarkdotnet.org) from
`DKey.Algorithms.Benchmark` (`dotnet run -c Release --project DKey.Algorithms.Benchmark`).

**Machine:** AMD Ryzen 9 5900HX (8 physical cores), Windows 11, .NET 8.0.34, X64 RyuJIT.
**Jobs:** `OrderedStructures` uses warmup 5 / iterations 15; the multi-millisecond suites use
`ShortRun` (warmup 3 / iterations 3) — treat those as indicative rather than lab-grade.
Fixed seeds (`seed: 20261121`) make the inputs reproducible.

## Ordered structures — `RBTInt` vs `SortedSet<int>`

10k inserts of a random permutation (per run); 1k lookups against a 10k structure.

| Method             | Mean        | Error     | StdDev    | Gen0     | Gen1    | Allocated |
|------------------- |------------:|----------:|----------:|---------:|--------:|----------:|
| SortedSet_Contains |    303.3 μs |  10.91 μs |   9.67 μs |        - |        - |         - |
| RBT_Contains       |    347.9 μs |  12.99 μs |  11.51 μs |        - |        - |         - |
| SortedSet_Add      | 10,833.0 μs | 109.53 μs |  91.47 μs | 46.8750  | 15.6250 |  400048 B |
| RBT_Add            | 24,117.6 μs | 445.21 μs | 394.67 μs | 250.0000 | 93.7500 | 2245704 B |

Lookups are on par with `SortedSet`; inserts are ~2.2× slower and allocate ~5.6× more
(per-node objects). The tree's value is its ordered-statistics API (`Search` returns the
rank), not raw insert speed.

## Segment tree — `IntegerSegmentTree` vs naive loops

n = 100 000, 100 random intervals per invocation. RangeSum runs on a prebuilt tree;
InitAndRangeAdd/CopyAndRangeAdd include the one-time build/copy plus the 100 updates.

| Method                      | Mean        | Error      | StdDev    | Allocated   |
|---------------------------- |------------:|-----------:|----------:|------------:|
| SegmentTree_RangeSum        |    254.5 μs |   128.6 μs |   7.05 μs |           - |
| Naive_CopyAndRangeAdd       | 17,557.0 μs | 3,103.5 μs | 170.11 μs |    400024 B |
| Naive_RangeSum              | 18,385.8 μs |   885.4 μs |  48.53 μs |           - |
| SegmentTree_InitAndRangeAdd | 35,370.9 μs | 6,932.6 μs | 380.00 μs | 10788910 B |

Interval queries are ~72× faster than naive loops; the tree pays for it with the one-time
O(n) build (~10.8 MB for 100k elements) — amortized across many queries/updates.

## Suffix tree — `SuffixTree<char>` build and search

Text: 20 000 random lowercase letters; 100 patterns of length 32–63 taken from the text.

| Method            | Mean        | Error       | StdDev      | Allocated |
|------------------ |------------:|------------:|------------:|----------:|
| String_Contains   |    651.0 μs |    759.8 μs |    41.65 μs |         - |
| SuffixTree_Search |    698.4 μs |    175.2 μs |     9.60 μs |  152888 B |
| SuffixTree_Build  | 80,999.6 μs | 94,670.8 μs | 5,189.23 μs | 5571817 B |

`Contains` matches `string.Contains` for repeated queries over the same text once the
~81 ms build is amortized — and generalizes to any `IComparable` alphabet
(`LongestCommonSubstring`, cross-document queries).

## Argument search — binary / ternary vs linear scan

Range [0, 1 000 000]; monotone predicate x² > 999 999 000; unimodal (x − 500 000)².

| Method                              | Mean           | Error           | StdDev       | Allocated |
|------------------------------------ |---------------:|----------------:|-------------:|----------:|
| BinarySearch_FirstXWithXSquareOverN |       251.6 ns |        23.29 ns |      1.28 ns |         - |
| TernarySearch_UnimodalMinimum       |       836.3 ns |        36.66 ns |      2.01 ns |         - |
| LinearScan_FirstXWithXSquareOverN   |    88,117.1 ns |    17,595.17 ns |    964.45 ns |         - |
| LinearScan_UnimodalMinimum          | 6,991,411.5 ns | 1,308,273.24 ns | 71,710.87 ns |         - |

~350× (binary) and ~8 400× (ternary) over the linear scans on this range size.
