using DKey.Algorithms.DataStructures.Graph.ShortSuffixTree;
using DKey.Algorithms.DataStructures.Graph.SuffixTree;
using DKey.Algorithms.NumberTheory;
using DKey.Algorithms.RandomData;

namespace DKey.CodeForces.Problem1780G;

/// <summary>
/// https://codeforces.com/contest/1780/problem/G
/// </summary>
public class Solver1780G : Solver
{
    public static int shortOpt = 20;
    
    public Solver1780G() : base(new []{typeof(int), typeof(string)})
    {
    }

    public override void Solve(object[] args)
    {
        var txt = (string)args[1];
        
        //SuffixTree<T> is ~20-30% too slow for time constraints, so we use ShortSuffixTree instead with some optimizations.
        var tree = ShortSuffixTree.Build(txt.Select(x => x-'a'+2).ToArray(), 0);
        var nodes = tree.Nodes;
        
        //Let's write number of occurences of substring = number of leafs for each vertex.
        //Can use written DFS instead of sorting, but this is more memory efficient and we already use 400/512MB.
        var weights = new int[tree.NodesSize];
        foreach (var nodeIndex in Enumerable.Range(0, tree.NodesSize).OrderByDescending(x => nodes[x].Depth))
            weights[nodeIndex] = Math.Max(nodes[nodeIndex].Children.Where(x => x != 0).Sum(x => weights[x]), 1);
        
        long result = 0;
        for (var i = 1; i < tree.NodesSize; i++)
        {
            //As suffix tree edge encodes multiple substrings, but every substring lead to the same number of leafs (= weight in vertex under this edge)
            //we need to check only divisors of weight, if their depth achievable on edge. For small edges, it's faster to check all possible depth, then getting divisors.
            var node = tree.Nodes[i];
            var parent = tree.Nodes[node.ParentIndex];
            var occurrences = weights[i];
            var mindepth = parent.Depth + 1;
            var maxdepth = node.Depth;
            if (maxdepth - mindepth < shortOpt)
                result += GetIntervalOccurences(mindepth, maxdepth, occurrences);
            else
                result += GetDividerOccurences(mindepth, maxdepth, occurrences);
        }
        
        //SuffixTree has terminal vertex with minChar, which we must exclude.
        output.AddL(result - 1);
    }

    private int GetDividerOccurences(int mindepth, int maxdepth, int occurrences)
    {
        return occurrences * PrimeArithmetics.GetAllDividers(occurrences).Count(x => x >= mindepth && x <= maxdepth);
    }

    private int GetIntervalOccurences(int mindepth, int maxdepth, int occurrences)
    {
        return occurrences * Enumerable.Range(mindepth, maxdepth - mindepth + 1).Count(x => occurrences % x == 0);
    }
}