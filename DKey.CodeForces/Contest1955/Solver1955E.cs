using DKey.Algorithms;

namespace DKey.CodeForces.Contest1955;

public class Solver1955E : MultiSolver
{
    public Solver1955E() : base( new Type[]{typeof(List<int>), typeof(string)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var n = seq[0];
        var dataStr = (string)objects[1];
        var i = n;
        for(i = n; i >=0; i--)
        {
            var q = new Queue<int>();
            for(var j = 0; j + i <= n; j++)
            {
                if (q.Count > 0 && q.Peek() <= j - i)
                    q.Dequeue();
                if ((dataStr[j] == '1') != (q.Count() % 2 == 0))
                    q.Enqueue(j);
            }
            var flag = true;
            for(var j = n-i; j < n; j++)
            {
                if (q.Count > 0 && q.Peek() <= j - i)
                    q.Dequeue();
                if ((dataStr[j] == '1') != (q.Count() % 2 == 0))
                    flag = false;
            }

            if (flag)
                break;
        }
        output.AddLine(i);
    }
}