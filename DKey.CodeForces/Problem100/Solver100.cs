﻿using DKey.Algorithms;

namespace DKey.CodeForces.Problem100;

public class Solver100 : Solver
{
    public Solver100() : base( new []{typeof(List<int>)})
    {
    }

    public override void Solve(object[] objects)
    {
        var seq = (List<int>) objects[0];
        var a = seq[0];
        var b = seq[1];
        output.AddLine(a+b);
    }
}