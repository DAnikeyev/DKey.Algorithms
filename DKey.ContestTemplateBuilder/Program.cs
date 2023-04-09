﻿using DKey.ContestTemplateBuilder;

namespace ContestTemplateBuilder;

public class Program
{
    /// <summary>
    /// Creates a folder in DKey.CodeForces with a template for problems A-F in the contest.
    /// </summary>
    public static void Main()
    {
        var folder = Path.Combine(Config.repoPath, "Contest" + Config.contestID);
        if(Directory.Exists(folder))
            return;
        Directory.CreateDirectory(folder);
        var template = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "ProblemTemplate.txt"));
        foreach (var problem in Enumerable.Range(0, 6).Select(x => (char)('A' + x)))
        {
            var solverTemplate = template.Replace("XXXY", Config.contestID + problem).Replace("XXX", Config.contestID);
            File.WriteAllText(Path.Combine(folder, "Solver" + Config.contestID + problem + ".cs"), solverTemplate);
        }
        
        var mainTemplate = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "MainTemplate.txt"));
        mainTemplate = mainTemplate.Replace("XXX", Config.contestID);
        File.WriteAllText(Path.Combine(Config.repoPath, "Program.cs"), mainTemplate);
    }
}