using DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

namespace DKey.Algorithms.DataStructures.Graph;

public class TreeGraph
{
    public int Root;
    public int VerticesCount;
    public TreeVertex[] Vertices;
    public GraphContext Context;

    protected internal void CreateVertexInDFS(DFSContext context)
    {
        var parent = -1;
        if (context.Parents.Any())
        {
            parent = context.Parents.Peek();
            Vertices[parent].Children.Add(context.CurrentVertex);
        }
            
        Vertices[context.CurrentVertex] = new TreeVertex(parent, context.CurrentVertex, new List<int>());
    }

    protected internal TreeGraph(int verticesCount, int root, GraphContext context)
    {
        VerticesCount = verticesCount;
        Vertices = new TreeVertex[verticesCount];
        Root = root;
        Context = context;
    }

    public static TreeGraph Build(List<int>[] Graph, int n, int root)
    {
        var context = new DFSContext(Graph, new HashSet<int>(), root);
        var tree = new TreeGraph(n, root, context);
        DepthFirstSearch.DepthFirstSearch.Iterative(context, tree.CreateVertexInDFS);
        return tree;
    }
}