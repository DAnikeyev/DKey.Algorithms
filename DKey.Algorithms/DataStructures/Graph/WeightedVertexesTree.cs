﻿namespace DKey.Algorithms.DataStructures.Graph;

public class WeightedVertexesTree : Tree
{
    public int[] Weights;
    
    protected WeightedVertexesTree(int verticesCount, int root, GraphContext context, int[] weights) : base(verticesCount, root, context)
    {
        Weights = weights;
    }
    
    public static Tree Build(List<int>[] Graph, int n, int root, int[] weights)
    {
        var context = new WeightedContext(Graph, n, new HashSet<int>(), root, weights);
        var tree = new WeightedVertexesTree(n, root, context, weights);
        DepthFirstSearch.Iterative(context, tree.CreateVertexInDFS);
        return tree;
    }
}