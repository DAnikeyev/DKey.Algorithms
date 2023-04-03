namespace DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;

public static class DFS
{
    /// <summary>
    /// Iterative depth-first search with some action on the current context.
    /// Use this for big graphs to avoid stack overflow.
    /// Can be more memory efficient if you push vertices 1 by 1 tracking how many children ha been pushed, but this is easier to understand.
    /// </summary>
    public static void Iterative<TContext>(TContext context, Action<TContext>? action = default) where TContext : DFSContext
    {
        if(context.stopFlag)
            return;
        var stack = new Stack<int>(context.Graph.Length);
        stack.Push(context.CurrentVertex);

        while (stack.Count > 0)
        {
            var currentVertex = stack.Peek();
            context.CurrentVertex = currentVertex;
            
            if (!context.Used.Contains(currentVertex))
            {
                //On entering the vertex.
                action?.Invoke(context);
                context.Used.Add(currentVertex);
                context.Parents.Push(currentVertex);

                //Pushing children to the stack of vertices to visit.
                for (int i = context.Graph[currentVertex].Count - 1; i >=0 ; i--)
                {
                    var nextAdjacent = context.Graph[currentVertex][i];
                    if (!context.Used.Contains(nextAdjacent))
                        stack.Push(nextAdjacent);
                }
            }
            else
            {
                //On exiting the vertex. If graph is not tree, we need to check current parent, to avoid removing vertex if it already was removed, but got visited through another branch.
                if(context.Parents.Peek() == currentVertex)
                    context.Parents.Pop();
                stack.Pop();
            }
        }
    }

    /// <summary>
    /// Depth-first search with some action on current context
    /// Use for n, less than 10000, otherwise there is a chance for stack overflow.
    /// </summary>
    public static void Recursive<TContext>(TContext context, Action<TContext>? action = default) where TContext : DFSContext
    {
        if (context.Used.Contains(context.CurrentVertex))
            return;
        action?.Invoke(context);
        context.Used.Add(context.CurrentVertex);
        context.Parents.Push(context.CurrentVertex);
        foreach (var adjacent in context.Graph[context.CurrentVertex])
        {
            context.CurrentVertex = adjacent;
            Recursive(context, action);
        }
        context.Parents.Pop();
    }
}