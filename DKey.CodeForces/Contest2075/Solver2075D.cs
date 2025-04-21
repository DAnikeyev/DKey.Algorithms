using DKey.Algorithms;

namespace DKey.CodeForces.Contest2075;


/// <summary>
/// https://codeforces.com/contest/2075/problem/D
/// </summary>
public class Solver2075D : MultiSolver
{
    public Solver2075D() : base( new []{typeof(List<long>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<long>) objects[0];
        var x = seq[0];
        var y = seq[1];
        if (x > y)
        {
            (x, y) = (y, x);
        }

        var xbin = ConvertLongToBoolArray(x);
        var ybin = ConvertLongToBoolArray(y);
        var opt = 0;
        foreach (var pair in xbin.Zip(ybin, (x, y) => (x, y)))
        {
            if (pair.x == pair.y)
                opt++;
            else
                break;
        }

        var v1 = xbin.Length - opt;
        var v2 = ybin.Length - opt;
        var total = v1 + v2;
        if(x == 0 || y == 0)
        {
            total = Math.Max(v1, v2);
            v2 = 0;
            v1 = total;
        }

        if (x == 0 && y == 0)
        {
            v1 = 0;
            v2 = 0;
            total = 0;
        }
        var root = 0;
        while (root * (root + 1) / 2 < total)
        {
            root++;
        }

        var dp = new Dictionary<(int, int, int), (long cost, int lastChoice)>();

        dp[(0, 0, 0)] = (0, 0);

        for (int i = 1; i <= root + 1; i++)
        {
            var newDp = new Dictionary<(int, int, int), (long cost, int lastChoice)>();

            foreach (var kvp in dp)
            {
                var (prevI, sumX, sumY) = kvp.Key;
                var (cost, lastChoice) = kvp.Value;
                newDp[(i, sumX, sumY)] = (cost, 0);
            }

            foreach (var kvp in dp)
            {
                var (prevI, sumX, sumY) = kvp.Key;
                var (cost, _) = kvp.Value;

                if (sumX + i <= v1)
                {
                    var newState = (i, sumX + i, sumY);
                    if (sumX + i > v1 + 1)
                        continue;
                    var newCost = cost + (1 << i);

                    if (!newDp.ContainsKey(newState) || newCost < newDp[newState].cost)
                    {
                        newDp[newState] = (newCost, 1);
                    }
                }

                if (sumY + i <= v2)
                {
                    var newState = (i, sumX, sumY + i);
                    if (sumY + i > v2 + 1)
                        continue;
                    var newCost = cost + (1 << i);

                    if (!newDp.ContainsKey(newState) || newCost < newDp[newState].cost)
                    {
                        newDp[newState] = (newCost, 2);
                    }
                }
            }

            dp = newDp;
        }

        var result = (0l, 0l);
        var extracted = dp.TryGetValue((root + 1, v1, v2), out var value);
        result = value;
        if (!extracted)
        {
            dp.TryGetValue((root + 1, v1 + 1, v2 + 1), out var value2);
            result = value2;
        }
        output.AddLine(result.Item1);

        ////var extra = root * (root + 1) / 2 - total;
        ////var cost = 0l;
        ////if ((v1 != 2 && v2 !=2) || extra != 2)
        ////{
        ////    for (var i = 1; i <= root; i++)
        ////    {
        ////        if (i == extra)
        ////            continue;
        ////        cost += 1l << (i);
        ////    }
        ////}
        ////else
        ////{
        ////    if((x >> (v1 + 1)) == (y >> (v2)) || (x >> (v1)) == (y >> (v2 + 1)))
        ////    {
        ////        total += 1;
        ////    }
        ////    else
        ////    {
        ////        total += 2;
        ////    }
        ////    root = 0;
        ////    while (root * (root + 1) / 2 < total)
        ////    {
        ////        root++;
        ////    }
        ////    extra = root * (root + 1) / 2 - total;
        ////    cost = 0l;
        ////    for (var i = 1; i <= root; i++)
        ////    {
        ////        if (i == extra)
        ////            continue;
        ////        cost += 1l << (i);
        ////    }
        ////}
    }


    public static bool[] ConvertLongToBoolArray(long value)
    {
        if (value == 0)
        {
            return new bool[] { false };
        }

        List<bool> boolList = new List<bool>();

        while (value > 0)
        {
            boolList.Add((value & 1) == 1);

            value >>= 1;
        }

        boolList.Reverse();

        return boolList.ToArray();
    }
}