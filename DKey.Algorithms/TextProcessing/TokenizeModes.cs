namespace DKey.Algorithms.TextProcessing;

/// <summary>Selects the token pattern <see cref="Tokenizer.Split"/> uses.</summary>
public enum TokenizerMode
{
    /// <summary>Token = a maximal run of letters delimited by non-word
    /// characters; a letter run adjacent to a digit is not matched
    /// (the \b word boundary) — e.g. "abc123" yields no tokens.</summary>
    TakeOnlyLetters = 1,
    /// <summary>Token = a maximal run of letters and digits.</summary>
    TakeOnlyLettersOrDigit = 2,
    /// <summary>Token = a maximal run of non-whitespace characters.</summary>
    WhiteSpaces = 3,
}
