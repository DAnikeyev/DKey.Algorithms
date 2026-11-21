namespace DKey.Algorithms.DynamicProgramming;

/// <summary>
/// Levenshtein edit distance: the minimum number of single-element insertions,
/// deletions and substitutions needed to turn one sequence into another.
/// </summary>
public static class Levenshtein
{
    /// <summary>
    /// Unit-cost edit distance between <paramref name="left"/> and
    /// <paramref name="right"/> (insert = delete = substitute = 1), zero when
    /// the sequences are equal. Works over any element type via
    /// <paramref name="comparer"/> (defaults to
    /// <see cref="EqualityComparer{T}.Default"/>) — e.g. chars for strings.
    /// Complexity: O(n*m) time, O(min(n, m)) memory (two rolling rows over the
    /// shorter sequence).
    /// </summary>
    public static int Distance<T>(IReadOnlyList<T> left, IReadOnlyList<T> right, IEqualityComparer<T>? comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        if (left.Count == 0)
        {
            return right.Count;
        }

        if (right.Count == 0)
        {
            return left.Count;
        }

        // Roll the DP over the shorter sequence so the rows are O(min(n, m)).
        if (right.Count > left.Count)
        {
            (left, right) = (right, left);
        }

        var previous = new int[right.Count + 1];
        var current = new int[right.Count + 1];
        for (var j = 0; j <= right.Count; j++)
        {
            previous[j] = j;
        }

        for (var i = 1; i <= left.Count; i++)
        {
            current[0] = i;
            for (var j = 1; j <= right.Count; j++)
            {
                var substitutionCost = comparer.Equals(left[i - 1], right[j - 1]) ? 0 : 1;
                current[j] = Math.Min(Math.Min(
                        current[j - 1] + 1,          // insertion into left
                        previous[j] + 1),            // deletion from left
                    previous[j - 1] + substitutionCost); // substitution/match
            }

            (previous, current) = (current, previous);
        }

        return previous[right.Count];
    }
}
