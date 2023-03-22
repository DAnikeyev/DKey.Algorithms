namespace DKey.Algorithms.DataStructures.Graph;

public static class DepthFirstSearch
{
    /// <summary>
    /// Iterative depth-first search with some action on the current context.
    /// </summary>
    public static void Iterative<TContext>(TContext context, Action<TContext>? action = default) where TContext : GraphContext
    {
        var stack = new Stack<(int, int)>();
        stack.Push((context.CurrentVertex, 0));

        while (stack.Count > 0)
        {
            var (currentVertex, adjacentIndex) = stack.Pop();

            if (!context.Used.Contains(currentVertex))
            {
                context.CurrentVertex = currentVertex;
                action?.Invoke(context);
                context.Used.Add(currentVertex);
            }

            if (adjacentIndex < context.Graph[currentVertex].Count)
            {
                var nextAdjacent = context.Graph[currentVertex][adjacentIndex];
                stack.Push((currentVertex, adjacentIndex + 1));

                if (!context.Used.Contains(nextAdjacent))
                {
                    context.Parents.Push(currentVertex);
                    stack.Push((nextAdjacent, 0));
                }
            }
            else
            {
                if (context.Parents.Count > 0 && context.Parents.Peek() == currentVertex)
                {
                    context.Parents.Pop();
                }
            }
        }
    }
    
    /// <summary>
    /// Depth-first search with some action on current context
    /// Use for n, less than 10000, otherwise there is a chance for stackoverflow.
    /// </summary>
    public static void Recursive<TContext>(GraphContext context, Action<GraphContext>? action = default) where TContext : GraphContext
    {
        if (context.Used.Contains(context.CurrentVertex))
            return;
        action?.Invoke(context);
        context.Used.Add(context.CurrentVertex);
        context.Parents.Push(context.CurrentVertex);
        foreach (var adjacent in context.Graph[context.CurrentVertex])
        {
            context.CurrentVertex = adjacent;
            Recursive<TContext>(context, action);
        }
        context.Parents.Pop();
    }
}