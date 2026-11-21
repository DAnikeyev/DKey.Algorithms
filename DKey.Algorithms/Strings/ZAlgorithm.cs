namespace DKey.Algorithms.Strings;

/// <summary>
/// Z-algorithm: for every position, the length of the longest common prefix of
/// the sequence and its suffix starting there (the Z-array), plus substring
/// search built on it. Linear time, no sentinel elements required.
/// </summary>
public static class ZAlgorithm
{
    /// <summary>
    /// The Z-array of the sequence: z[0] = data.Count by convention, and for
    /// i &gt; 0 z[i] is the length of the longest common prefix of data and
    /// data[i..]. Complexity: O(n) time, O(n) memory.
    /// </summary>
    /// <param name="data">Input sequence.</param>
    /// <param name="comparer">Element equality; defaults to
    /// <see cref="EqualityComparer{T}.Default"/>.</param>
    public static int[] ZFunction<T>(IReadOnlyList<T> data, IEqualityComparer<T>? comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        var count = data.Count;
        var z = new int[count];
        if (count == 0)
        {
            return z;
        }

        z[0] = count;
        var left = 0;
        var right = 0;
        for (var i = 1; i < count; i++)
        {
            if (i < right)
            {
                z[i] = Math.Min(right - i, z[i - left]);
            }

            while (i + z[i] < count && comparer.Equals(data[z[i]], data[i + z[i]]))
            {
                z[i]++;
            }

            if (i + z[i] > right)
            {
                left = i;
                right = i + z[i];
            }
        }

        return z;
    }

    /// <summary>
    /// All start indexes where <paramref name="pattern"/> occurs in
    /// <paramref name="text"/>, in increasing order — including overlapping
    /// occurrences; empty when absent. An empty pattern yields every position
    /// 0..text.Count (same convention as <see cref="KmpSearch.AllIndexes"/>).
    /// Uses the pattern's Z-array plus a single left-to-right scan of the
    /// text — no concatenated sentinel needed. Complexity: O(n + m).
    /// </summary>
    public static List<int> AllIndexes<T>(IReadOnlyList<T> text, IReadOnlyList<T> pattern, IEqualityComparer<T>? comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        var matches = new List<int>();
        if (pattern.Count == 0)
        {
            for (var i = 0; i <= text.Count; i++)
            {
                matches.Add(i);
            }

            return matches;
        }

        var patternZ = ZFunction(pattern, comparer);
        var textCount = text.Count;
        var patternCount = pattern.Count;
        var left = 0;
        var right = 0;
        for (var i = 0; i < textCount; i++)
        {
            int matched;
            if (i < right)
            {
                matched = Math.Min(right - i, patternZ[i - left]);
                if (i + matched < right)
                {
                    // Fully inside the Z-box: no extension past patternZ is possible.
                    if (matched == patternCount)
                    {
                        matches.Add(i);
                    }

                    continue;
                }
            }
            else
            {
                matched = 0;
            }

            while (i + matched < textCount && matched < patternCount &&
                   comparer.Equals(text[i + matched], pattern[matched]))
            {
                matched++;
            }

            if (i + matched > right)
            {
                left = i;
                right = i + matched;
            }

            if (matched == patternCount)
            {
                matches.Add(i);
            }
        }

        return matches;
    }
}
