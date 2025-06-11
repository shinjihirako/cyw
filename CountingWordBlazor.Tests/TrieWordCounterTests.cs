using System.Collections.Generic;
using CountingWordBlazor.Domain;
using CountingWordBlazor.Domain.Contracts;
using Moq;
using Xunit;

namespace CountingWordBlazor.Tests;
public class TrieWordCounterTests
{
    [Fact]
    public void CountWords_ReturnsCorrectCounts_ForUniqueWords()
    {
        // Arrange
        var trieMock = MockFactory.GetTrieMock(true);
        var counter = new TrieWordCounter(trieMock.Object);
        var words = new List<string> { "basic", "fit", "security", "azure", "azurE", "Basic", "Azure" };

        // Act
        var result = counter.CountWords(words);

        // Assert
        Assert.Equal(3, result["azure"]);
        Assert.Equal(2, result["basic"]);
        Assert.Equal(1, result["fit"]);
    }

    [Fact]
    public void CountWords_DoesNotCountWordsNotInTrie()
    {
        // Arrange
        var trieMock = new Mock<ITrie>();
        trieMock.Setup(t => t.Insert(It.IsAny<string>()));
        trieMock.Setup(t => t.Search("basic")).Returns(true);
        trieMock.Setup(t => t.Search("fit")).Returns(false);

        var counter = new TrieWordCounter(trieMock.Object);
        var words = new List<string> { "basic", "fit", "apple" };

        // Act
        var result = counter.CountWords(words);

        // Assert
        Assert.Equal(1, result["basic"]);
        Assert.False(result.ContainsKey("banana"));
    }

    [Fact]
    public void CountWords_IsCaseInsensitive()
    {
        // Arrange
        var trieMock = new Mock<ITrie>();
        trieMock.Setup(t => t.Insert(It.IsAny<string>()));
        trieMock.Setup(t => t.Search(It.IsAny<string>())).Returns(true);

        var counter = new TrieWordCounter(trieMock.Object);
        var words = new List<string> { "NotFit", "notfit", "Notfit" };

        // Act
        var result = counter.CountWords(words);

        // Assert
        Assert.Single(result);
        Assert.Equal(3, result["notfit"]);
    }

    [Fact]
    public void CountWords_ReturnsEmptyResult_WhenGivenEmptyList()
    {
        var trieMock = new Mock<ITrie>();
        var counter = new TrieWordCounter(trieMock.Object);
        var result = counter.CountWords(new List<string>());
        Assert.Empty(result);
    }


}
