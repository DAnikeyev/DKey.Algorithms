using DKey.Algorithms;
using DKey.Algorithms.Combinatorics;
using DKey.Algorithms.NumberTheory;

namespace DKey.CodeForces.Contest1816;

public class Solver1816D : MultiSolver
{
    public Solver1816D() : base( new Type[]{typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var n = seq[0];
        
        //Requesting to create a line graph n---1---(n-1)---2...
        var input = GetResponse($"+ {n+1}");
        input = GetResponse($"+ {n}");
        var furthest = 0;
        var furthestIndex = 0;
        
        //Find one of the ends of the graph.
        for (var i = 1; i < n; i++)
        {
            input = GetResponse($"? 1 {1+i}");
            if (input > furthest)
            {
                furthestIndex = i;
                furthest = input;
            }
        }

        //Perm[x] defines distances from p[x] to the fixed furthest vertex.
        var perm = new int[n];
        for (var i = 0; i < n; i++)
        {
            if (i == furthestIndex)
                continue;
            input = GetResponse($"? {furthestIndex + 1} {1+i}");
            perm[i] = input;
        }
        perm[furthestIndex] = 0;
        var line = new List<int>();
        for (var i = 0; i < n; i++)
        {
            if(i % 2 == 0)
                line.Add(n - i/2 - 1);
            else
                line.Add(1+i/2 - 1);
        }
        
        //Solving equation p^{-1}(line(x))=perm(x).
        //line has 2 possible orientations.
        var ans = Permutations.Inverse(Permutations.Multiply(Permutations.Inverse(line), Permutations.Inverse(perm)));
        line.Reverse();
        var ansr = Permutations.Inverse(Permutations.Multiply(Permutations.Inverse(line), Permutations.Inverse(perm)));
        var req = "! " + string.Join(" ", ans.Select(x => x+1)) + " " + string.Join(" ", ansr.Select(x => x+1));
        input = GetResponse(req);
        if (input == -2)
            throw new Exception("WA");
    }

    public int GetResponse(string request)
    {
        Console.WriteLine(request);
        return IOHelper.ReadInt();
    }
}