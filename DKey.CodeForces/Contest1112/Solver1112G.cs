using DKey.Algorithms;
using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

namespace DKey.CodeForces.Contest1112;


/// <summary>
/// https://codeforces.com/contest/1112/problem/G
/// </summary>
public class Solver1112G : MultiSolver
{
    public Solver1112G() : base( new []{typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var n = seq[0];
        if (n == 2)
        {
            output.AddLine(1);
            return;
        }
        var graph = GraphBuilder.Undirected(IOHelper.Read2dList(n).Select(x => (x[0], x[1])).ToList(), n, false);

        var leafCount = new Dictionary<int, int>();
        var optimal = new Dictionary<int, int>();
        var weight = new Dictionary<int, int>();
        for (int i = 0; i < n; i++)
        {
            leafCount[i] = 0;
        }
        var leafs = new HashSet<int>();

        for (var i = 0; i < graph.Length; i++)
        {
            if (graph[i].Count == 1)
            {
                leafCount[graph[i][0]]++;
                leafs.Add(i);
            }
        }

        var order = new List<int>();
        var context = new DFSContext(graph, 0);
        DFS.Iterative(context, x => order.Add(x.CurrentVertex));
        order.Reverse();
        foreach (var v in order)
        {
            if (leafs.Contains(v))
                continue;
            var addedweight = leafCount[v];
            foreach (var neighbour in graph[v])
            {
                var localWeights = new List<(int, int)>();
                if (weight.ContainsKey(neighbour))
                {
                    localWeights.Add((neighbour, weight[neighbour]));
                }

                localWeights = localWeights.OrderByDescending(x => x.Item2).ToList();
                var m1 = localWeights.Count > 0 ? localWeights[0].Item2 : 0;
                var m2 = localWeights.Count > 1 ? localWeights[1].Item2 : 0;
                optimal[v] = Math.Max(optimal[v], m1 + m2 + addedweight);
                weight[v] = Math.Max(weight[v], m1 + addedweight);
            }
        }
    }
}