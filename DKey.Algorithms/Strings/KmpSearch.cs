namespace DKey.Algorithms.Strings;

/// <summary>
/// Knuth-Morris-Pratt substring search: the prefix function of the pattern is
/// precomputed once (O(m) time, O(m) memory) and the text is then scanned in a
/// single pass (O(n)) — O(n + m) total, no backtracking.
/// </summary>
public static class KmpSearch
{
    /// <summary>
    /// Prefix function (failure links) of the sequence: for every position i,
    /// the length of the longest proper prefix of data[0..i] that is also its
    /// suffix. Complexity: O(n) time, O(n) memory.
    /// </summary>
    /// <param name="data">Input sequence.</param>
    /// <param name="comparer">Element equality; defaults to
    /// <see cref="EqualityComparer{T}.Default"/>.</param>
    public static int[] PrefixFunction<T>(IReadOnlyList<T> data, IEqualityComparer<T>? comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        var result = new int[data.Count];
        for (var i = 1; i < data.Count; i++)
        {
            var length = result[i - 1];
            while (length > 0 && !comparer.Equals(data[i], data[length]))
            {
                length = result[length - 1];
            }

            if (comparer.Equals(data[i], data[length]))
            {
                length++;
            }

            result[i] = length;
        }

        return result;
    }

    /// <summary>
    /// Index of the first occurrence of <paramref name="pattern"/> in
    /// <paramref name="text"/> (element-wise), or -1 when absent. An empty
    /// pattern matches at 0, as in <c>string.IndexOf(string.Empty)</c>.
    /// Complexity: O(n + m).
    /// </summary>
    public static int IndexOf<T>(IReadOnlyList<T> text, IReadOnlyList<T> pattern, IEqualityComparer<T>? comparer = null)
    {
        if (pattern.Count == 0)
        {
            return 0;
        }

        var prefix = PrefixFunction(pattern, comparer);
        comparer ??= EqualityComparer<T>.Default;
        var matched = 0;
        for (var i = 0; i < text.Count; i++)
        {
            while (matched > 0 && !comparer.Equals(text[i], pattern[matched]))
            {
                matched = prefix[matched - 1];
            }

            if (comparer.Equals(text[i], pattern[matched]))
            {
                matched++;
            }

            if (matched == pattern.Count)
            {
                return i + 1 - pattern.Count;
            }
        }

        return -1;
    }

    /// <summary>
    /// All start indexes where <paramref name="pattern"/> occurs in
    /// <paramref name="text"/>, in increasing order — including overlapping
    /// occurrences. An empty pattern yields every position 0..text.Count.
    /// Complexity: O(n + m).
    /// </summary>
    public static List<int> AllIndexes<T>(IReadOnlyList<T> text, IReadOnlyList<T> pattern, IEqualityComparer<T>? comparer = null)
    {
        if (pattern.Count == 0)
        {
            var all = new List<int>(text.Count + 1);
            for (var i = 0; i <= text.Count; i++)
            {
                all.Add(i);
            }

            return all;
        }

        var matches = new List<int>();
        var prefix = PrefixFunction(pattern, comparer);
        comparer ??= EqualityComparer<T>.Default;
        var matched = 0;
        for (var i = 0; i < text.Count; i++)
        {
            while (matched > 0 && !comparer.Equals(text[i], pattern[matched]))
            {
                matched = prefix[matched - 1];
            }

            if (comparer.Equals(text[i], pattern[matched]))
            {
                matched++;
            }

            if (matched == pattern.Count)
            {
                matches.Add(i + 1 - pattern.Count);
                matched = prefix[matched - 1];
            }
        }

        return matches;
    }
}
