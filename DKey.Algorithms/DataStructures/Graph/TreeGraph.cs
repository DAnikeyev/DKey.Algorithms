using DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

namespace DKey.Algorithms.DataStructures.Graph;

/// <summary>
/// A tree rebuilt from a graph by a DFS from the chosen root: per-vertex
/// parent/children records over <see cref="TreeVertex"/>.
/// </summary>
public class TreeGraph
{
    /// <summary>Index of the root vertex.</summary>
    public int Root;
    /// <summary>Number of vertices.</summary>
    public int VerticesCount;
    /// <summary>Per-vertex records (parent + children), indexed by vertex.</summary>
    public TreeVertex[] Vertices;
    /// <summary>The traversal context the tree was built from.</summary>
    public GraphContext Context;

    /// <summary>DFS callback: on entering a vertex, registers it under its DFS parent.</summary>
    protected internal void CreateVertexInDFS(DFSContext context)
    {
        var parent = -1;
        if (context.Parents.Any())
        {
            parent = context.Parents.Peek();
            Vertices[parent].Children!.Add(context.CurrentVertex);
        }

        Vertices[context.CurrentVertex] = new TreeVertex(parent, context.CurrentVertex, new List<int>());
    }

    /// <summary>Prepares the storage for a tree of <paramref name="verticesCount"/> vertices rooted at <paramref name="root"/>.</summary>
    protected internal TreeGraph(int verticesCount, int root, GraphContext context)
    {
        VerticesCount = verticesCount;
        Vertices = new TreeVertex[verticesCount];
        Root = root;
        Context = context;
    }

    /// <summary>
    /// Builds the tree by running an iterative DFS from <paramref name="root"/>.
    /// Complexity: O(V + E).
    /// </summary>
    public static TreeGraph Build(List<int>[] Graph, int n, int root)
    {
        var context = new DFSContext(Graph, root);
        var tree = new TreeGraph(n, root, context);
        DFS.Iterative(context, tree.CreateVertexInDFS);
        return tree;
    }
}
