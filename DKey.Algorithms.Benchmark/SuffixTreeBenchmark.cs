using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using DKey.Algorithms.DataStructures.Graph.SuffixTree;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Benchmark;

/// <summary>
/// Suffix tree build cost and Contains search vs string.Contains.
/// </summary>
[ShortRunJob]
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class SuffixTreeBenchmark
{
    private const int TextLength = 20_000;
    private const int SearchCount = 100;

    private string _text = null!;
    private string[] _patterns = null!;
    private SuffixTree<char> _tree = null!;

    [GlobalSetup]
    public void Setup()
    {
        var gen = RandomData.ListGenerator.Instance(seed: 20261121);
        _text = new string(gen.RandomString(TextLength, 26).ToCharArray());
        _tree = SuffixTree<char>.Build(_text.ToList(), char.MinValue);
        _patterns = new string[SearchCount];
        for (var i = 0; i < SearchCount; i++)
        {
            var start = gen.RandomList(1, 0, TextLength - 64)[0];
            _patterns[i] = _text.Substring(start, 32 + i % 32);
        }
    }

    [Benchmark]
    public SuffixTree<char> SuffixTree_Build()
    {
        return SuffixTree<char>.Build(_text.ToList(), char.MinValue);
    }

    [Benchmark]
    public int SuffixTree_Search()
    {
        var found = 0;
        foreach (var pattern in _patterns)
            if (_tree.Contains(pattern.ToList())) found++;
        return found;
    }

    [Benchmark]
    public int String_Contains()
    {
        var found = 0;
        foreach (var pattern in _patterns)
            if (_text.Contains(pattern)) found++;
        return found;
    }
}
