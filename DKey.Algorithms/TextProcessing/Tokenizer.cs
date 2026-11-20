using System.Text.RegularExpressions;

namespace DKey.Algorithms.TextProcessing;

/// <summary>
/// Regex-based text splitter driven by <see cref="TokenizerMode"/>.
/// </summary>
public class Tokenizer
{
    private static Dictionary<TokenizerMode, string> Patterns = new Dictionary<TokenizerMode, string>()
    {
        {TokenizerMode.TakeOnlyLetters, @"\b[a-zA-Z]+\b"},
        {TokenizerMode.TakeOnlyLettersOrDigit, @"\b[a-zA-Z0-9]+\b"},
        {TokenizerMode.WhiteSpaces, @"\S+"},
    };

    /// <summary>Splits <paramref name="text"/> into tokens per the mode's pattern,
    /// in match order. Complexity: O(text length).</summary>
    public static List<string> Split(string text, TokenizerMode mode)
    {
        List<string> result = new List<string>();
        var matches = Regex.Matches(text, Patterns[mode]);
        foreach (Match match in matches)
            result.Add(match.Value);
        return result;
    }
}
