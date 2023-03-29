using System.Text;
using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.TextProcessing;

namespace SubmissionBuilder;

public class Program
{
    public static Dictionary<string, string> classToPath= new ();
    public static Dictionary<string, int> classToIndex= new ();
    public static Dictionary<string, string> classToImplementation= new ();
    public static Dictionary<string, HashSet<string>> classToUsings= new ();
    public static List<string> Keys = new ();
    
    public static void Main()
    {
        var graph = BuildDependencies();
        var context = new GraphContext(graph, new HashSet<int>(), classToIndex[Config.Root]);
        DepthFirstSearch.Iterative(context);
        var classIndexes = context.Used;
        File.WriteAllText(Config.SubmissionPath, BuildSubmission(classIndexes));
    }

    private static string BuildSubmission(HashSet<int> classIndexes)
    {
        var sb = new StringBuilder();

        var usings = classIndexes.Select(x => classToUsings[Keys[x]]).SelectMany(x => x).ToHashSet();
        foreach (var u in usings)
        {
            sb.AppendLine(u);
        }

        sb.AppendLine();
        sb.AppendLine("namespace Submission;");
        sb.AppendLine();
        sb.AppendLine(string.Join("\n", classIndexes.Select(x => classToImplementation[Keys[x]])));
        return sb.ToString();
    }

    public static List<int>[] BuildDependencies()
    {
        foreach (var folder in Config.FoldersToExplore)
        {
            var folderPath = Path.Combine(Config.RepoPath, folder);
            foreach (var filePath in Directory.EnumerateFiles(folderPath, "*.cs", SearchOption.AllDirectories))
            {
                var className = Path.GetFileNameWithoutExtension(filePath);
                if (Config.FoldersToExclude.Any(filePath.Contains) || Config.ExcludedClasses.Any(className.Contains) || classToPath.ContainsKey(className))
                    continue;
                Keys.Add(className);
                classToIndex.Add(className, Keys.Count - 1);
                classToPath.Add(className, filePath);
            }
        }

        var size = Keys.Count;
        var graph = new List<int>[size];
        for(var i = 0; i < Keys.Count; i++)
        {
            var key = Keys[i];
            graph[i] = ParseClass(key, File.ReadAllLines(classToPath[key]));
        }

        return graph;
    }

    private static List<int> ParseClass(string key, string[] lines)
    {
        classToUsings[key] = new HashSet<string>();
        var index = 0;
        while (true)
        {
            var line = lines[index].Trim();
            if (line.Contains("using") && !line.Contains("DKey"))
                classToUsings[key].Add(line);
            if (line.Contains("namespace") || lines.Contains("class") || lines.Contains("enum"))
                break;
            index++;
        }

        while (index < lines.Length && (!lines[index].Contains("class") && !lines[index].Contains("enum") ))
            index++;

        var implementation = string.Join("\n", lines.Skip(index));
        classToImplementation[key] = implementation;
        var tokens = Tokenizer.Split(implementation, TokenizerMode.TakeOnlyLettersOrDigit);
        var indexes = new HashSet<int>();
        foreach (var t in tokens)
        {
            if (t == key)
                continue;
            if(classToIndex.TryGetValue(t, out var dependencyIndex))
                indexes.Add(dependencyIndex);
        }

        return indexes.ToList();
    }
}