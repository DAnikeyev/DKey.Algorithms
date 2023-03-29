using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Graph.SuffixTree;

namespace DKey.CodeForcesExamples._1780G;

public class WeightedSfxTree : WeightedVertexesTree
{
    public List<int> Order = new List<int>();
    protected WeightedSfxTree(int verticesCount, int root, GraphContext context, int[] weights) : base(verticesCount, root, context, weights)
    {
    }
    
    public static WeightedSfxTree Build (string s)
    {
        var sfxTree = SuffixTree<char>.Build(s.ToCharArray(), Char.MinValue);
        var Graph = sfxTree.Nodes.Select(x => x.VertexIndex == 0 ? x.children.Values.ToList() : x.children.Values.Append(x.ParentIndex).ToList()).ToArray();
        var n = Graph.Length;
        var root = 0;
        var context = new WeightedContext(Graph, n, new HashSet<int>(), root, new int[n]);
        var tree = new WeightedSfxTree(n, root, context, new int[n]);
        DepthFirstSearch.Iterative(context, tree.ProcessVertexInDFS);
        tree.Order.Reverse();
        foreach (var vertex in tree.Order)
        {
            tree.Weights[vertex] = Math.Max(tree.Vertices[vertex].Children.Sum(x => tree.Weights[x]),1);
        }
        tree.Weights[0] = Math.Max(tree.Vertices[0].Children.Sum(x => tree.Weights[x]),1);
        return tree;
    }
    
    private void ProcessVertexInDFS(WeightedContext context)
    {
        var parent = -1;
        if (context.Parents.Any())
        {
            parent = context.Parents.Peek();
            Vertices[parent].Children.Add(context.CurrentVertex);
        }
        Order.Add(context.CurrentVertex);
            
        Vertices[context.CurrentVertex] = new TreeVertex(parent, context.CurrentVertex, new List<int>());
    }
}