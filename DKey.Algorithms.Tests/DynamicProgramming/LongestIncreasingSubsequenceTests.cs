using DKey.Algorithms.DynamicProgramming;
using DKey.Algorithms.RandomData;

namespace DKey.Algorithms.Tests.DynamicProgramming;

[TestFixture]
public class LongestIncreasingSubsequenceTests
{
    [Test]
    public void Strict_Default_ExcludesEqualNeighbors()
    {
        var data = new[] { 1, 3, 3, 5 };

        ClassicAssert.AreEqual(3, LongestIncreasingSubsequence.Length(data));
    }

    [Test]
    public void NonStrict_AllowEqual_IncludesDuplicates()
    {
        var data = new[] { 1, 3, 3, 5 };

        ClassicAssert.AreEqual(4, LongestIncreasingSubsequence.Length(data, allowEqual: true));
    }

    [Test]
    public void SortedInput_LengthIsN_ReverseSorted_IsOne()
    {
        ClassicAssert.AreEqual(5, LongestIncreasingSubsequence.Length(new[] { 1, 2, 3, 4, 5 }));
        ClassicAssert.AreEqual(1, LongestIncreasingSubsequence.Length(new[] { 5, 4, 3, 2, 1 }));
        ClassicAssert.AreEqual(5, LongestIncreasingSubsequence.Length(new[] { 1, 2, 3, 4, 5 }, allowEqual: true));
        ClassicAssert.AreEqual(1, LongestIncreasingSubsequence.Length(new[] { 5, 4, 3, 2, 1 }, allowEqual: true));
    }

    [Test]
    public void AllEqual_StrictOne_NonStrictN()
    {
        ClassicAssert.AreEqual(1, LongestIncreasingSubsequence.Length(new[] { 7, 7, 7, 7 }));
        ClassicAssert.AreEqual(4, LongestIncreasingSubsequence.Length(new[] { 7, 7, 7, 7 }, allowEqual: true));
    }

    [Test]
    public void EmptyAndSingleton()
    {
        ClassicAssert.AreEqual(0, LongestIncreasingSubsequence.Length(Array.Empty<int>()));
        ClassicAssert.AreEqual(1, LongestIncreasingSubsequence.Length(new[] { 42 }));
        ClassicAssert.AreEqual(0, LongestIncreasingSubsequence.Sequence(Array.Empty<int>()).Count);
    }

    [Test]
    public void Sequence_IsValidAndLongest_ClassicCase()
    {
        var data = new[] { 10, 9, 2, 5, 3, 7, 101, 18 };

        var strict = LongestIncreasingSubsequence.Sequence(data);
        var nonStrict = LongestIncreasingSubsequence.Sequence(data, allowEqual: true);

        ClassicAssert.AreEqual(4, strict.Count);
        ClassicAssert.IsTrue(IsValidSubsequence(data, strict, strictIncrease: true), $"[{string.Join(",", strict)}]");
        ClassicAssert.AreEqual(LongestIncreasingSubsequence.Length(data), strict.Count);
        ClassicAssert.IsTrue(IsValidSubsequence(data, nonStrict, strictIncrease: false), $"[{string.Join(",", nonStrict)}]");
        ClassicAssert.AreEqual(LongestIncreasingSubsequence.Length(data, allowEqual: true), nonStrict.Count);
    }

    [Test]
    public void CustomComparer_DecreasingLIS()
    {
        var data = new[] { 1, 2, 3, 2, 1 };

        // Longest DECREASING subsequence = LIS under reversed order: 3,2,1.
        ClassicAssert.AreEqual(3, LongestIncreasingSubsequence.Length(data, Comparer<int>.Create((a, b) => b.CompareTo(a))));
    }

    [Test]
    public void RandomInputs_MatchQuadraticReference_BothModes()
    {
        var generator = ListGenerator.Instance(seed: 20261121);

        for (var attempt = 0; attempt < 50; attempt++)
        {
            var data = generator.RandomList(generator.RandomList(1, 1, 60)[0], 0, 9).ToArray();

            var expectedStrict = QuadraticLis(data, strict: true);
            var expectedNonStrict = QuadraticLis(data, strict: false);
            var actualStrict = LongestIncreasingSubsequence.Length(data);
            var actualNonStrict = LongestIncreasingSubsequence.Length(data, allowEqual: true);

            ClassicAssert.AreEqual(expectedStrict, actualStrict, $"attempt {attempt} strict");
            ClassicAssert.AreEqual(expectedNonStrict, actualNonStrict, $"attempt {attempt} non-strict");
        }
    }

    [Test]
    public void RandomInputs_ReconstructionIsValid_BothModes()
    {
        var generator = ListGenerator.Instance(seed: 20261122);

        for (var attempt = 0; attempt < 50; attempt++)
        {
            var data = generator.RandomList(generator.RandomList(1, 1, 60)[0], 0, 9).ToArray();

            var strict = LongestIncreasingSubsequence.Sequence(data);
            var nonStrict = LongestIncreasingSubsequence.Sequence(data, allowEqual: true);

            ClassicAssert.IsTrue(IsValidSubsequence(data, strict, strictIncrease: true), $"attempt {attempt} strict");
            ClassicAssert.IsTrue(IsValidSubsequence(data, nonStrict, strictIncrease: false), $"attempt {attempt} non-strict");
            ClassicAssert.AreEqual(LongestIncreasingSubsequence.Length(data), strict.Count);
            ClassicAssert.AreEqual(LongestIncreasingSubsequence.Length(data, allowEqual: true), nonStrict.Count);
        }
    }

    private static bool IsValidSubsequence(int[] data, List<int> subsequence, bool strictIncrease)
    {
        if (subsequence.Count == 0)
        {
            return data.Length == 0;
        }

        var dataIndex = 0;
        for (var i = 0; i < subsequence.Count; i++)
        {
            while (dataIndex < data.Length && data[dataIndex] != subsequence[i])
            {
                dataIndex++;
            }

            if (dataIndex == data.Length)
            {
                return false; // element not found in order — not a subsequence
            }

            dataIndex++;
        }

        for (var i = 1; i < subsequence.Count; i++)
        {
            if (strictIncrease ? subsequence[i - 1] >= subsequence[i] : subsequence[i - 1] > subsequence[i])
            {
                return false;
            }
        }

        return true;
    }

    private static int QuadraticLis(int[] data, bool strict)
    {
        if (data.Length == 0)
        {
            return 0;
        }

        var best = new int[data.Length];
        var result = 1;
        for (var i = 0; i < data.Length; i++)
        {
            best[i] = 1;
            for (var j = 0; j < i; j++)
            {
                var improves = strict ? data[j] < data[i] : data[j] <= data[i];
                if (improves && best[j] + 1 > best[i])
                {
                    best[i] = best[j] + 1;
                }
            }

            result = Math.Max(result, best[i]);
        }

        return result;
    }
}
