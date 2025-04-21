using DKey.Algorithms;
using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

namespace DKey.CodeForces.Examples.Contest1797;

/// <summary>
/// https://codeforces.com/contest/1797/problem/D
/// </summary>
public class Solver1797D : Solver
{
    public Solver1797D() : base( new []{typeof(List<int>), typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var n = seq[0];
        var m = seq[1];
        var a = (List<int>) objects[1];
        
        var edges = IOHelper.Read2dList(n-1).Select(x => (x[0] - 1, x[1] - 1)).ToList();
        var graph = GraphBuilder.Undirected(edges,n);
        var tree = TreeGraph.Build(graph, n, 0);
        var context = new DFSContext(graph, 0);
        
        //Order from list to root to fill weights and sum of subtree.
        var order = new List<int>();
        
        var weights = new long[n];
        var subssize = new long[n];
        DFS.Iterative(context, x => order.Add(x.CurrentVertex));
        
        //SortedSet to dynamicly remove/add children in O(log(n)).
        var structure = new SortedSet<int>[n];
        order.Reverse();
        foreach (var vertex in order)
        {
            weights[vertex] = tree.Vertices[vertex].Children.Sum(x => weights[x]) + a[vertex];
            subssize[vertex] = tree.Vertices[vertex].Children.Sum(x => subssize[x]) + 1;
            //Comparer makes maximum of set to have the max importance.
            structure[vertex] = new SortedSet<int>(Comparer<int>.Create((i, i1) => 
            {
                var importanceComparison = subssize[i].CompareTo(subssize[i1]);
                return importanceComparison > 0 ? 1 : (importanceComparison < 0 ? -1 : i1.CompareTo(i));
            }));
            foreach (var child in tree.Vertices[vertex].Children)
                structure[vertex].Add(child);
        }
        
        var queries = IOHelper.Read2dList(m);
        foreach (var query in queries)
        {
            if(query[0] == 1)
                output.AddLine(weights[query[1] - 1]);
            else
            {
                var v = query[1] - 1;
                if (structure[v].Count == 0)
                    continue;
                var vSon = structure[v].Max;
                var vFather = tree.Vertices[v].ParentIndex;
                
                //Updating tree.
                tree.Vertices[v].ParentIndex = vSon;
                tree.Vertices[vSon].ParentIndex = vFather;
                structure[vFather].Remove(v);
                structure[v].Remove(vSon);
                weights[v] -= weights[vSon];
                weights[vSon] += weights[v];
                subssize[v] -= subssize[vSon];
                subssize[vSon] += subssize[v];
                structure[vFather].Add(vSon);
                structure[vSon].Add(v);
            }
        }
    }
}