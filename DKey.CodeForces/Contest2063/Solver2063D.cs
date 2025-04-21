using DKey.Algorithms;

namespace DKey.CodeForces.Contest2063;


/// <summary>
/// https://codeforces.com/contest/2063/problem/D
/// </summary>
public class Solver2063D : MultiSolver
{
    private int curTop = 0;
    private int curBottom = 0;
    public Solver2063D() : base( new []{typeof(List<int>), typeof(List<int>), typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var n = seq[0];
        var m = seq[1];
        var top = (List<int>) objects[1];
        var bottom = (List<int>) objects[2];
        top = top.OrderBy(x => x).ToList();
        bottom = bottom.OrderBy(x => x).ToList();
        var topSorted = new List<int>();
        for(var i = 0; i < top.Count/2; i++)
        {
            topSorted.Add(top[n - i - 1] - top[i]);
        }
        var bottomSorted = new List<int>();
        for(var i = 0; i < bottom.Count/2; i++)
        {
            bottomSorted.Add(bottom[m - i - 1] - bottom[i]);
        }

        curTop = 0;
        curBottom = 0;
        long curSum = 0;
        var answers = new List<long>();
        while (true)
        {

            var topBottleNeck = n - usedTop;
            var bottomBottleNeck = m - usedBottom;
            if(topBottleNeck + bottomBottleNeck < 3)
                break;
            if(bottomBottleNeck >= 1 && topBottleNeck >= 2 && ((curBottom >= bottomSorted.Count) || (topSorted[curTop] > bottomSorted[curBottom])))
            {
                curSum += topSorted[curTop];
                answers.Add(curSum);
                curTop+=1;
            }
            else if (topBottleNeck >=1 && bottomBottleNeck >= 2 && ((curTop >= topSorted.Count) || (topSorted[curTop] <= bottomSorted[curBottom])))
            {
                curSum += bottomSorted[curBottom];
                answers.Add(curSum);
                curBottom+=1;
            }
            else if(topBottleNeck >=2 && bottomBottleNeck >= 1)
            {
                curSum += topSorted[curTop];
                answers.Add(curSum);
                curTop++;
            }
            else if(topBottleNeck >=1 && bottomBottleNeck >= 2)
            {
                curSum += bottomSorted[curBottom];
                answers.Add(curSum);
                curBottom++;
            }
            else if (topBottleNeck == 0 && curTop > 0)
            {
                curTop--;
                curSum -= topSorted[curTop];
                curSum += bottomSorted[curBottom];
                curBottom += 1;
                curSum += bottomSorted[curBottom];
                curBottom += 1;
                answers.Add(curSum);
            }
            else if (bottomBottleNeck == 0 && curBottom > 0)
            {
                curBottom--;
                curSum -= bottomSorted[curBottom];
                curSum += topSorted[curTop];
                curTop += 1;
                curSum += topSorted[curTop];
                curTop += 1;
                answers.Add(curSum);
            }
            else
            {
                break;
            }
        }
        output.AddLine(answers.Count);
        output.AddListLine(answers);

    }

    private int usedTop => 2*curTop + curBottom;
    private int usedBottom => 2*curBottom + curTop;
}