using DKey.ContestTemplateBuilder;

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
        foreach (var problem in Enumerable.Range(0, Config.problemCount).Select(x => (char)('A' + x)))
        {
            var solverTemplate = template.Replace("{ContestNumber}", Config.contestID).Replace("{ProblemLetter}", problem.ToString());
            File.WriteAllText(Path.Combine(folder, "Solver" + Config.contestID + problem + ".cs"), solverTemplate);
        }
        
        var mainTemplate = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "MainTemplate.txt"));
        mainTemplate = mainTemplate.Replace("{ContestNumber}", Config.contestID).Replace("{ProblemLetter}", "A");
        File.WriteAllText(Path.Combine(Config.repoPath, "Program.cs"), mainTemplate);
    }
}