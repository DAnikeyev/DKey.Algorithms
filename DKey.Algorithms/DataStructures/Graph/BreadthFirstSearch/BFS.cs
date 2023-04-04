namespace DKey.Algorithms.DataStructures.Graph.BreadthFirstSearch;

public class BFS
{
    /// <summary>
    /// Breadth-first search with some action on the current context.
    /// </summary>
    public static void Traverse<TContext>(TContext context, Action<TContext>? action = default) where TContext : TraverseContext
    {
        if(context.stopFlag)
            return;
        var queue = new Queue<int>();
        var currentDepth = 0;

        queue.Enqueue(context.CurrentVertex);
        context.Used.Add(context.CurrentVertex);
        context.VertexInfo[context.CurrentVertex] = (-1, 0);

        while (queue.Count > 0)
        {
            var currentVertex = queue.Dequeue();
            context.CurrentVertex = currentVertex;
            action?.Invoke(context);

            foreach (var adjacent in context.Graph[currentVertex])
            {
                if (!context.Used.Contains(adjacent))
                {
                    queue.Enqueue(adjacent);
                    context.Used.Add(adjacent);
                    context.VertexInfo[adjacent] = (currentVertex, currentDepth + 1);
                }
            }

            if (queue.Count > 0 && context.VertexInfo[queue.Peek()].depth > currentDepth)
            {
                currentDepth++;
            }
        }
    }
}