namespace DKey.Algorithms.DataStructures.Graph.PrefixTrie;

/// <summary>
/// One node of the prefix trie (<see cref="Trie"/>): a child per next
/// character plus the end-of-word marker.
/// </summary>
public class TrieNode
{
    /// <summary>Child nodes keyed by the character that leads to them.</summary>
    public Dictionary<char, TrieNode> Children;
    /// <summary>True when a word inserted into the trie ends at this node.</summary>
    public bool IsEndOfWord;

    /// <summary>Creates an empty node with no children.</summary>
    public TrieNode()
    {
        Children = new Dictionary<char, TrieNode>();
        IsEndOfWord = false;
    }
}
