using CountingWordBlazor.Domain.Contracts;

public class TrieWordCounter : IWordCounter
{
    private readonly ITrie _trie;

    public TrieWordCounter(ITrie trie)
    {
        _trie = trie;
    }

    public Dictionary<string, int> CountWords(IEnumerable<string> words)
    {
        var results = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        var uniqueWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var word in words)
        {
            if (uniqueWords.Add(word))
                _trie.Insert(word);

            if (_trie.Search(word))
                results[word] = results.TryGetValue(word, out var count) ? count + 1 : 1;
        }

        return results;
    }
}
