namespace DKey.Algorithms.DataStructures.Graph;

public class Tree
{
    public int Root;
    public int VerticesCount;
    public TreeVertex[] Vertices;
    public GraphContext Context;

    protected internal void CreateVertexInDFS(GraphContext context)
    {
        var parent = -1;
        if (context.Parents.Any())
        {
            parent = context.Parents.Peek();
            Vertices[parent].Children.Add(context.CurrentVertex);
        }
            
        Vertices[context.CurrentVertex] = new TreeVertex(parent, context.CurrentVertex, new List<int>());
    }

    protected internal Tree(int verticesCount, int root, GraphContext context)
    {
        VerticesCount = verticesCount;
        Vertices = new TreeVertex[verticesCount];
        Root = root;
        Context = context;
    }

    public static Tree Build(List<int>[] Graph, int n, int root)
    {
        var context = new GraphContext(Graph, n, new HashSet<int>(), root);
        var tree = new Tree(n, root, context);
        DepthFirstSearch.Iterative(context, tree.CreateVertexInDFS);
        return tree;
    }
}