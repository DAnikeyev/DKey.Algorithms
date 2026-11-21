using DKey.Algorithms.RandomData;
using DKey.Algorithms.Strings;

namespace DKey.Algorithms.Tests.Strings;

[TestFixture]
public class KmpSearchTests
{
    [Test]
    public void PrefixFunction_KnownValues()
    {
        // abacaba -> 0 0 1 0 1 2 3
        ClassicAssert.IsTrue(new[] { 0, 0, 1, 0, 1, 2, 3 }.SequenceEqual(
            KmpSearch.PrefixFunction("abacaba".ToList())));
        // aaaaa -> 0 1 2 3 4
        ClassicAssert.IsTrue(new[] { 0, 1, 2, 3, 4 }.SequenceEqual(
            KmpSearch.PrefixFunction("aaaaa".ToList())));
        // abcdef (no border) -> all zeros
        ClassicAssert.IsTrue(new[] { 0, 0, 0, 0, 0, 0 }.SequenceEqual(
            KmpSearch.PrefixFunction("abcdef".ToList())));
    }

    [Test]
    public void IndexOf_FindsFirstOccurrence()
    {
        ClassicAssert.AreEqual(1, KmpSearch.IndexOf("banana".ToList(), "ana".ToList()));
        ClassicAssert.AreEqual(0, KmpSearch.IndexOf("abc".ToList(), "abc".ToList()));
        ClassicAssert.AreEqual(4, KmpSearch.IndexOf("mississippi".ToList(), "issip".ToList()));
    }

    [Test]
    public void IndexOf_Absent_ReturnsMinusOne()
    {
        ClassicAssert.AreEqual(-1, KmpSearch.IndexOf("banana".ToList(), "nab".ToList()));
        ClassicAssert.AreEqual(-1, KmpSearch.IndexOf("abc".ToList(), "abcd".ToList()));
    }

    [Test]
    public void IndexOf_EmptyPattern_ReturnsZero()
    {
        ClassicAssert.AreEqual(0, KmpSearch.IndexOf("abc".ToList(), new List<char>()));
        ClassicAssert.AreEqual(0, KmpSearch.IndexOf(new List<char>(), new List<char>()));
    }

    [Test]
    public void IndexOf_EmptyText_NonEmptyPattern_ReturnsMinusOne()
    {
        ClassicAssert.AreEqual(-1, KmpSearch.IndexOf(new List<char>(), "a".ToList()));
    }

    [Test]
    public void AllIndexes_IncludesOverlappingMatches()
    {
        // banana / ana -> positions 1 and 3 (overlap at index 3)
        ClassicAssert.IsTrue(new[] { 1, 3 }.SequenceEqual(
            KmpSearch.AllIndexes("banana".ToList(), "ana".ToList())));
        // aaaaa / aa -> 0 1 2 3 (fully overlapping)
        ClassicAssert.IsTrue(new[] { 0, 1, 2, 3 }.SequenceEqual(
            KmpSearch.AllIndexes("aaaaa".ToList(), "aa".ToList())));
    }

    [Test]
    public void AllIndexes_None_ReturnsEmpty()
    {
        ClassicAssert.AreEqual(0, KmpSearch.AllIndexes("abcdef".ToList(), "xyz".ToList()).Count);
    }

    [Test]
    public void GenericElements_IntSequences()
    {
        var text = new[] { 1, 2, 1, 2, 1, 2, 2 };
        var pattern = new[] { 2, 1, 2, 2 };

        ClassicAssert.AreEqual(3, KmpSearch.IndexOf(text, pattern));
        ClassicAssert.IsTrue(new[] { 3 }.SequenceEqual(KmpSearch.AllIndexes(text, pattern)));
    }

    [Test]
    public void RandomStrings_MatchNaiveScan()
    {
        var generator = ListGenerator.Instance(seed: 20261121);

        for (var attempt = 0; attempt < 50; attempt++)
        {
            var alphabet = generator.RandomList(1, 2, 4)[0];
            var text = generator.RandomString(200, alphabet).ToList();
            var patternLength = generator.RandomList(1, 1, 6)[0];
            var pattern = generator.RandomString(patternLength, alphabet).ToList();

            var expected = NaiveAll(text, pattern);
            var actual = KmpSearch.AllIndexes(text, pattern);

            ClassicAssert.IsTrue(expected.SequenceEqual(actual),
                $"attempt {attempt} (alphabet {alphabet}): naive [{string.Join(",", expected)}] vs kmp [{string.Join(",", actual)}]");
            ClassicAssert.AreEqual(NaiveFirst(text, pattern), KmpSearch.IndexOf(text, pattern));
        }
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

    private static int NaiveFirst<T>(IReadOnlyList<T> text, IReadOnlyList<T> pattern, IEqualityComparer<T>? comparer = null)
    {
        var all = NaiveAll(text, pattern, comparer);
        return all.Count > 0 ? all[0] : -1;
    }
}
