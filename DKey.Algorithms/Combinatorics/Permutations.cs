namespace DKey.Algorithms.Combinatorics;

public class Permutations
{
    
    public static int[] Inverse( IList<int> permutation)
    {
        var n = permutation.Count;
        var res = new int[n];
        for (var i = 0; i < n; i++)
            res[permutation[i]] = i;

        return res;
    }
    
    public static int[] Multiply( IList<int> permutation1, IList<int> permutation2)
    {
        var n = permutation1.Count;
        var res = new int[n];
        for (var i = 0; i < n; i++)
            res[i] = permutation2[permutation1[i]];
        return res;
    }
}