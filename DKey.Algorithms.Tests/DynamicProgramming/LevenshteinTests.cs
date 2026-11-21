using DKey.Algorithms.DynamicProgramming;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.DynamicProgramming;

[TestFixture]
public class LevenshteinTests
{
    [Test]
    public void EmptySequences()
    {
        ClassicAssert.AreEqual(0, Levenshtein.Distance(new List<char>(), new List<char>()));
        ClassicAssert.AreEqual(3, Levenshtein.Distance(new List<char>(), "abc".ToList()));
        ClassicAssert.AreEqual(3, Levenshtein.Distance("abc".ToList(), new List<char>()));
    }

    [Test]
    public void EqualSequences_AreZero()
    {
        ClassicAssert.AreEqual(0, Levenshtein.Distance("kitten".ToList(), "kitten".ToList()));
        ClassicAssert.AreEqual(0, Levenshtein.Distance(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }));
    }

    [Test]
    public void KnownDistances()
    {
        ClassicAssert.AreEqual(3, Levenshtein.Distance("kitten".ToList(), "sitting".ToList()));
        ClassicAssert.AreEqual(2, Levenshtein.Distance("flaw".ToList(), "lawn".ToList()));
        ClassicAssert.AreEqual(3, Levenshtein.Distance("abc".ToList(), "xyz".ToList()));
        ClassicAssert.AreEqual(1, Levenshtein.Distance("abc".ToList(), "abd".ToList()));
    }

    [Test]
    public void Symmetric()
    {
        var a = "intention".ToList();
        var b = "execution".ToList();

        ClassicAssert.AreEqual(
            Levenshtein.Distance(a, b),
            Levenshtein.Distance(b, a));
    }

    [Test]
    public void TriangleInequality_Holds()
    {
        // dist(a, c) <= dist(a, b) + dist(b, c) — a defining property of the metric.
        var a = "sunday".ToList();
        var b = "saturday".ToList();
        var c = "sundae".ToList();

        ClassicAssert.LessOrEqual(
            Levenshtein.Distance(a, c),
            Levenshtein.Distance(a, b) + Levenshtein.Distance(b, c));
    }

    [Test]
    public void GenericElements_IntSequences()
    {
        ClassicAssert.AreEqual(2, Levenshtein.Distance(new[] { 1, 2, 3, 4 }, new[] { 2, 3, 5 }));
    }

    [Test]
    public void RandomSequences_MatchFullMatrixReference()
    {
        var generator = ListGenerator.Instance(seed: 20261121);

        for (var attempt = 0; attempt < 50; attempt++)
        {
            var alphabet = generator.RandomList(1, 2, 8)[0];
            var a = generator.RandomString(generator.RandomList(1, 0, 60)[0], alphabet).ToList();
            var b = generator.RandomString(generator.RandomList(1, 0, 60)[0], alphabet).ToList();

            var expected = FullMatrixDistance(a, b);
            var actual = Levenshtein.Distance(a, b);

            ClassicAssert.AreEqual(expected, actual,
                $"attempt {attempt}: '{new string(a.ToArray())}' vs '{new string(b.ToArray())}'");
        }
    }

    private static int FullMatrixDistance<T>(IReadOnlyList<T> a, IReadOnlyList<T> b, IEqualityComparer<T>? comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        var dp = new int[a.Count + 1, b.Count + 1];
        for (var i = 0; i <= a.Count; i++)
        {
            dp[i, 0] = i;
        }

        for (var j = 0; j <= b.Count; j++)
        {
            dp[0, j] = j;
        }

        for (var i = 1; i <= a.Count; i++)
        {
            for (var j = 1; j <= b.Count; j++)
            {
                var cost = comparer.Equals(a[i - 1], b[j - 1]) ? 0 : 1;
                dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
            }
        }

        return dp[a.Count, b.Count];
    }
}
