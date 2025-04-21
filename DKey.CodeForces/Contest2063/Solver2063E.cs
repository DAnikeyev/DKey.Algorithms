using DKey.Algorithms;
using DKey.Algorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DKey.CodeForces.Contest2063
{
    /// <summary>
    /// https://codeforces.com/contest/2063/problem/E
    /// Efficient merging approach for multiple children in a tree to reduce time complexity.
    /// </summary>
    public class Solver2063E : MultiSolver
    {
        private static List<int>[] adj;   // adjacency list
        private static bool[] visited;    // visited array for DFS
        private static int[] depth;       // store depths of nodes
        private static long answer;       // global answer for each test case

        public Solver2063E() : base(new[] { typeof(List<int>) })
        {
        }

        public override void Solve(object[] objects)
        {
            // "seq" is expected to contain input data; seq[0] = number of vertices (n).
            var seq = (List<int>)objects[0];
            var n = seq[0];
            answer = 0;

            // Edges: each line has two nodes, converting them to zero-based indices
            List<(int, int)> edges = IOHelper.Read2dList(n - 1)
                .Select(x => (x[0] - 1, x[1] - 1))
                .ToList();

            // Build an undirected adjacency list
            adj = GraphBuilder.Undirected(edges, n, true);

            // Initialize depth and visited arrays
            visited = new bool[n + 1];
            depth = new int[n + 1];
            answer = 0;

            // Run DFS from node 0 (root in zero-based index)
            Dfs(0, -1);

            // Print the result for this test case
            output.AddLine(answer);
        }

        /// <summary>
        /// DFS that returns a sorted list of subtree depths from the current node.
        /// Uses the "small-to-large" technique to merge subtree lists.
        /// </summary>
        private static List<int> Dfs(int u, int parent)
        {
            visited[u] = true;
            depth[u] = (parent == -1) ? 0 : depth[parent] + 1;

            // List of child-subtree depths (relative to node u)
            List<int> mergedList = new List<int>();

            // Explore children
            foreach (int nxt in adj[u])
            {
                if (nxt == parent) continue;
                if (!visited[nxt])
                {
                    // DFS into the child
                    var childList = Dfs(nxt, u);

                    // If both lists have elements, calculate pair contributions
                    if (mergedList.Count > 0 && childList.Count > 0)
                    {
                        long contribution = CalculatePairs(mergedList, childList);
                        answer += contribution;
                    }

                    // Merge smaller list into bigger (small-to-large optimization)
                    if (childList.Count > mergedList.Count)
                    {
                        var temp = mergedList;
                        mergedList = childList;
                        childList = temp;
                    }

                    // Merge them in sorted order
                    mergedList = MergeSorted(mergedList, childList);
                }
            }

            // Include the current node in this subtree's list
            mergedList.Add(0);

            // Increment all depths by 1 to adjust for the parent's perspective
            for (int i = 0; i < mergedList.Count; i++)
            {
                mergedList[i]++;
            }

            return mergedList;
        }

        /// <summary>
        /// Calculate the sum of (2 * min(a, b) - 1) for every pair (a, b) in A x B.
        /// </summary>
        private static long CalculatePairs(List<int> A, List<int> B)
        {
            long sumOfMin = 0;
            int i = 0, j = 0;
            while (i < A.Count && j < B.Count)
            {
                if (A[i] < B[j])
                {
                    sumOfMin += (long)A[i] * (B.Count - j);
                    i++;
                }
                else
                {
                    sumOfMin += (long)B[j] * (A.Count - i);
                    j++;
                }
            }
            long pairCount = (long)A.Count * B.Count;
            return 2 * sumOfMin - pairCount;
        }

        /// <summary>
        /// Merges two sorted lists into one sorted list.
        /// </summary>
        private static List<int> MergeSorted(List<int> larger, List<int> smaller)
        {
            var merged = new List<int>(larger.Count + smaller.Count);
            int i = 0, j = 0;
            while (i < larger.Count && j < smaller.Count)
            {
                if (larger[i] <= smaller[j])
                {
                    merged.Add(larger[i]);
                    i++;
                }
                else
                {
                    merged.Add(smaller[j]);
                    j++;
                }
            }
            while (i < larger.Count) merged.Add(larger[i++]);
            while (j < smaller.Count) merged.Add(smaller[j++]);

            return merged;
        }
    }
}