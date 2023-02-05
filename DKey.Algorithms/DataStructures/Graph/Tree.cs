namespace DKey.Algorithms.DataStructures.Graph;

public class Tree
{
    public int Root;
    public int VerticesCount;
    public TreeVertex[] Vertices;
    public GraphContext Context;

    private void ProcessVertexInDFS(GraphContext context)
    {
        var parent = -1;
        if (context.ParentList.Any())
        {
            parent = context.ParentList.Last();
            Vertices[parent].Children.Add(context.CurrentVertex);
        }
            
        Vertices[context.CurrentVertex] = new TreeVertex(parent, context.CurrentVertex, new HashSet<int>());
        context.Process();
    }

    private Tree(int verticesCount, int root, GraphContext context)
    {
        VerticesCount = verticesCount;
        Vertices = new TreeVertex[verticesCount];
        Root = root;
        Context = context;
    }

    public static Tree Build(List<int>[] Graph, int n, int root)
    {
        var context = new TreeContext(Graph, n, new HashSet<int>(), root);
        var tree = new Tree(n, root, context);
        Helper.DFS(context, tree.ProcessVertexInDFS);
        return tree;
    }
}