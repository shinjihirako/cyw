using CountingWordBlazor.Domain;
using CountingWordBlazor.Domain.Contracts;
using System.Text.RegularExpressions;

namespace CountingWordBlazor.Domain
{
    public class TrieNode 
    {
        public Dictionary<char, TrieNode> Children { get; set; } = new();
        public bool IsEndOfWord { get; set; } = false;
    }
}

public class Trie : ITrie
{
    private readonly TrieNode _root = new();

    public void Insert(string word)
    {
        var current = _root;
        foreach (var c in word.ToLowerInvariant())
        {
            current = current.Children.TryGetValue(c, out var child)
                ? child
                : current.Children[c] = new TrieNode();
        }

        current.IsEndOfWord = true;
    }

    public bool Search(string word)
    {
        var current = _root;
        foreach (var c in word.ToLowerInvariant())
        {
            if (!current.Children.TryGetValue(c, out current))
                return false;
        }
        return current.IsEndOfWord;
    }
}
