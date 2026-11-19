namespace DKey.Algorithms.DataStructures.Graph.PrefixTrie;

/// <summary>
/// Prefic trie implementation. Useful for searching for longest prefix in a list of words.
/// Can achieve same or better results with suffix tree, but it's more complicated and require words to not be a prefix of another word.
/// </summary>
public class Trie
{
    private readonly TrieNode _root;

    public Trie()
    {
        _root = new TrieNode();
    }

    /// <summary>Inserts every word into the trie. Complexity: O(total length).</summary>
    public void Build(List<string> words)
    {
        foreach (var word in words)
            Insert(word);
    }

    /// <summary>Inserts one word, creating missing nodes along its path.</summary>
    private void Insert(string word)
    {
        TrieNode current = _root;

        foreach (char c in word)
        {
            if (!current.Children.ContainsKey(c))
                current.Children[c] = new TrieNode();

            current = current.Children[c];
        }

        current.IsEndOfWord = true;
    }

    /// <summary>True when any inserted word starts with <paramref name="prefix"/>.
    /// Complexity: O(prefix length).</summary>
    public bool SearchPrefix(string prefix)
    {
        TrieNode current = _root;

        foreach (char c in prefix)
        {
            if (!current.Children.ContainsKey(c))
                return false;
            current = current.Children[c];
        }

        return true;
    }

    /// <summary>Returns the longest inserted word that is a prefix of
    /// <paramref name="input"/>; an empty string when none matches.
    /// Complexity: O(input length).</summary>
    public string SearchLongestPrefix(string input)
    {
        var current = _root;
        var longestMatch = string.Empty;
        var currentMatch = string.Empty;

        foreach (var c in input)
        {
            if (!current.Children.TryGetValue(c, out var child))
                break;
            currentMatch += c;
            current = child;

            if (current.IsEndOfWord)
                longestMatch = currentMatch;
        }

        return longestMatch;
    }
}
