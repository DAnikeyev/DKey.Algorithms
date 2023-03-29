namespace DKey.Algorithms.DataStructures.Graph.PrefixTrie;

public class Trie
{
    private readonly TrieNode _root;

    public Trie()
    {
        _root = new TrieNode();
    }

    public void Build(List<string> words)
    {
        foreach (var word in words)
            Insert(word);
    }

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
    
    public string SearchLongestPrefix(string input)
    {
        var current = _root;
        string longestMatch = null;
        string currentMatch = string.Empty;

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
