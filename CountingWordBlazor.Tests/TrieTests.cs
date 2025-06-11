using Xunit;
using CountingWordBlazor.Domain;

namespace CountingWordBlazor.Tests;

public class TrieTests
{
   [Fact]
    public void Insert_AndSearchWordReturnsTrue()
    {
        // Arrange
        var trie = new Trie();
        var word = "Basic";

        // Act
        trie.Insert(word);
        var result = trie.Search(word.ToLowerInvariant());

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Search_NonExistentWordReturnsFalse()
    {
        // Arrange
        var trie = new Trie();
        trie.Insert("Basic");

        // Act
        var result = trie.Search("UnFit");

        // Assert
        Assert.False(result);
    }
}
