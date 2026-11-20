namespace DKey.Algorithms.Combinatorics;

/// <summary>
/// Operations on permutations given as sequences of {0..n-1}.
/// </summary>
public class Permutations
{

    /// <summary>Returns the inverse permutation: res[permutation[i]] = i.</summary>
    public static int[] Inverse(IList<int> permutation)
    {
        var n = permutation.Count;
        var res = new int[n];
        for (var i = 0; i < n; i++)
            res[permutation[i]] = i;

        return res;
    }

    /// <summary>Returns the composition permutation2 ∘ permutation1:
    /// res[i] = permutation2[permutation1[i]].</summary>
    public static int[] Multiply(IList<int> permutation1, IList<int> permutation2)
    {
        var n = permutation1.Count;
        var res = new int[n];
        for (var i = 0; i < n; i++)
            res[i] = permutation2[permutation1[i]];
        return res;
    }
}
