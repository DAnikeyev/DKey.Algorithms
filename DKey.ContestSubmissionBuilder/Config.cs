using System.Reflection;

namespace SubmissionBuilder;

internal class Config
{
    public static string RepoPath = @"C:\Repos\GitHub\DKey.Algorithms";
    public static string[] FoldersToExplore = new []{"DKey.Algorithms", "DKey.CodeForces"};
    public static string[] FoldersToExclude = new []{@"\obj\", @"\bin\"};
    public static string Root = "Program";
    public static string SubmissionPath = @"C:\Repos\CodeForcesSubmission\CodeForcesSubmission\Program.cs";
    public static string[] ExcludedClasses = new []{"AssemblyInfo"};
    public static string[] PrimitiveExtensions = new []{"IOHelper", "LinqExtension"};
}