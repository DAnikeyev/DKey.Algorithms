using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SubmissionBuilder;

public class Program2
{

    class Program
    {
        public static async Task Main(string[] args)
        {
            string rootPath = @"C:\Repos\github\DKey.Algorithms\DKey.CodeForces";
            string programFilePath = Path.Combine(rootPath, "Program.cs");

            var sourceText = await File.ReadAllTextAsync(programFilePath);
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceText);
            var root = syntaxTree.GetRoot();

            var combinedCode = new StringBuilder();

            CombineFiles(root, rootPath, combinedCode);

            string outputPath = Path.Combine(rootPath, "submission.cs");
            await File.WriteAllTextAsync(outputPath, combinedCode.ToString());
        }

        private static void CombineFiles(SyntaxNode node, string rootPath, StringBuilder combinedCode)
        {
            if (node is UsingDirectiveSyntax usingDirective)
            {
                var path = GetPathFromNamespace(usingDirective, rootPath);
                if (File.Exists(path))
                {
                    var sourceText = File.ReadAllText(path);
                    var syntaxTree = CSharpSyntaxTree.ParseText(sourceText);
                    var root = syntaxTree.GetRoot();
                    combinedCode.AppendLine(sourceText);
                    foreach (var childNode in root.ChildNodes())
                    {
                        CombineFiles(childNode, rootPath, combinedCode);
                    }
                }
            }
            else
            {
                foreach (var childNode in node.ChildNodes())
                {
                    CombineFiles(childNode, rootPath, combinedCode);
                }
            }
        }

        private static string GetPathFromNamespace(UsingDirectiveSyntax usingDirective, string rootPath)
        {
            var namespaceName = usingDirective.Name.ToString();
            var filePath = namespaceName.Replace('.', Path.DirectorySeparatorChar) + ".cs";
            return Path.Combine(rootPath, filePath);
        }
    }
}