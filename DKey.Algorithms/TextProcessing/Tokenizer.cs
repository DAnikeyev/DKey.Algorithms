using System.Text.RegularExpressions;

namespace DKey.Algorithms.TextProcessing;

public class Tokenizer
{
    public static Dictionary<TokenizerMode, string> Patterns = new Dictionary<TokenizerMode, string>()
    {
        {TokenizerMode.TakeOnlyLetters, @"\b[a-zA-Z]+\b"},
        {TokenizerMode.TakeOnlyLettersOrDigit, @"\b[a-zA-Z0-9]+\b"},
        {TokenizerMode.WhiteSpaces, @"\S+"},
    };
    
    public static List<string> Split(string text, TokenizerMode mode)
    {
        List<string> result = new List<string>();
        var matches = Regex.Matches(text, Patterns[mode]);
        foreach (Match match in matches)
            result.Add(match.Value);
        return result;
    }
}