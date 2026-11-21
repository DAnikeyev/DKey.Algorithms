using DKey.Algorithms.RandomData;
using DKey.Algorithms.Strings;

namespace DKey.Algorithms.Tests.Strings;

[TestFixture]
public class ZAlgorithmTests
{
    [Test]
    public void ZFunction_KnownValues()
    {
        // aaaaa -> 5 4 3 2 1
        ClassicAssert.IsTrue(new[] { 5, 4, 3, 2, 1 }.SequenceEqual(
            ZAlgorithm.ZFunction("aaaaa".ToList())));
        // abacaba -> 7 0 1 0 3 0 1
        ClassicAssert.IsTrue(new[] { 7, 0, 1, 0, 3, 0, 1 }.SequenceEqual(
            ZAlgorithm.ZFunction("abacaba".ToList())));
        // abcdef -> only z[0] is non-zero
        ClassicAssert.IsTrue(new[] { 6, 0, 0, 0, 0, 0 }.SequenceEqual(
            ZAlgorithm.ZFunction("abcdef".ToList())));
    }

    [Test]
    public void ZFunction_EmptySequence_ReturnsEmpty()
    {
        ClassicAssert.AreEqual(0, ZAlgorithm.ZFunction(new List<char>()).Length);
    }

    [Test]
    public void AllIndexes_IncludesOverlappingMatches()
    {
        ClassicAssert.IsTrue(new[] { 1, 3 }.SequenceEqual(
            ZAlgorithm.AllIndexes("banana".ToList(), "ana".ToList())));
        ClassicAssert.IsTrue(new[] { 0, 1, 2, 3 }.SequenceEqual(
            ZAlgorithm.AllIndexes("aaaaa".ToList(), "aa".ToList())));
    }

    [Test]
    public void AllIndexes_Absent_ReturnsEmpty()
    {
        ClassicAssert.AreEqual(0, ZAlgorithm.AllIndexes("abcdef".ToList(), "xyz".ToList()).Count);
    }

    [Test]
    public void AllIndexes_EmptyPattern_ReturnsEveryPosition()
    {
        ClassicAssert.IsTrue(new[] { 0, 1, 2, 3 }.SequenceEqual(
            ZAlgorithm.AllIndexes("abc".ToList(), new List<char>())));
    }

    [Test]
    public void GenericElements_IntSequences()
    {
        var text = new[] { 1, 2, 1, 2, 1, 2, 2 };
        var pattern = new[] { 2, 1, 2, 2 };

        ClassicAssert.IsTrue(new[] { 3 }.SequenceEqual(ZAlgorithm.AllIndexes(text, pattern)));
    }

    [Test]
    public void RandomSequences_ZArrayMatchesBruteForce()
    {
        var generator = ListGenerator.Instance(seed: 20261121);

        for (var attempt = 0; attempt < 50; attempt++)
        {
            var alphabet = generator.RandomList(1, 2, 4)[0];
            var data = generator.RandomString(generator.RandomList(1, 1, 150)[0], alphabet).ToList();
            var actual = ZAlgorithm.ZFunction(data);
            var expected = BruteForceZ(data);

            ClassicAssert.IsTrue(expected.SequenceEqual(actual),
                $"attempt {attempt}: brute [{string.Join(",", expected)}] vs z [{string.Join(",", actual)}]");
        }
    }

    [Test]
    public void RandomSequences_SearchMatchesNaiveScan()
    {
        var generator = ListGenerator.Instance(seed: 20261122);

        for (var attempt = 0; attempt < 50; attempt++)
        {
            var alphabet = generator.RandomList(1, 2, 4)[0];
            var text = generator.RandomString(200, alphabet).ToList();
            var patternLength = generator.RandomList(1, 1, 6)[0];
            var pattern = generator.RandomString(patternLength, alphabet).ToList();

            var expected = NaiveAll(text, pattern);
            var actual = ZAlgorithm.AllIndexes(text, pattern);

            ClassicAssert.IsTrue(expected.SequenceEqual(actual),
                $"attempt {attempt} (alphabet {alphabet}): naive [{string.Join(",", expected)}] vs z [{string.Join(",", actual)}]");
        }
    }

    private static int[] BruteForceZ<T>(IReadOnlyList<T> data, IEqualityComparer<T>? comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        var z = new int[data.Count];
        if (data.Count > 0)
        {
            z[0] = data.Count;
        }

        for (var i = 1; i < data.Count; i++)
        {
            while (i + z[i] < data.Count && comparer.Equals(data[z[i]], data[i + z[i]]))
            {
                z[i]++;
            }
        }

        return z;
    }

    private static List<int> NaiveAll<T>(IReadOnlyList<T> text, IReadOnlyList<T> pattern, IEqualityComparer<T>? comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        var matches = new List<int>();
        for (var start = 0; start + pattern.Count <= text.Count; start++)
        {
            var ok = true;
            for (var i = 0; i < pattern.Count; i++)
            {
                if (!comparer.Equals(text[start + i], pattern[i]))
                {
                    ok = false;
                    break;
                }
            }

            if (ok)
            {
                matches.Add(start);
            }
        }

        return matches;
    }
}
