using Xunit;
using CountingWordBlazor.Domain;
using Moq;

namespace CountingWordBlazor.Tests;

public class TextTokenizerTests
{
    [Fact]
    public void Tokenize_HandlesWhitespace()
    {
        // Arrange
        var tokenizer = new RegexTextTokenizer();
        string input = "Basic Fit video Surveillance!";

        // Act
        var tokens = tokenizer.Tokenize(input).ToArray();

        // Assert
        Assert.Equal(4, tokens.Length);
        Assert.Equal("basic", tokens[0]);
        Assert.Equal("fit", tokens[1]);
        Assert.Equal("video", tokens[2]);
        Assert.Equal("surveillance", tokens[3]);
    }

    [Fact]
    public void Tokenize_HandlesPunctuation()
    {
        // Arrange
        var tokenizer = new RegexTextTokenizer();
        var input = "Hello, Basic Fit; video Surveillance!";

        // Act
        var tokens = tokenizer.Tokenize(input).ToArray();

        // Assert
        Assert.Contains("hello", tokens);
        Assert.Contains("basic", tokens);
        Assert.Contains("fit", tokens);
        Assert.Contains("video", tokens);
        Assert.Contains("surveillance", tokens);
        Assert.DoesNotContain("hello,", tokens);
        Assert.DoesNotContain("Fit;", tokens);
        Assert.DoesNotContain("Surveillance!", tokens);
    }

    [Fact]
    public void Tokenize_HandlesEmptyString()
    {
        // Arrange
        var tokenizer = new RegexTextTokenizer();
        var input = "";

        // Act
        var tokens = tokenizer.Tokenize(input).ToArray();

        // Assert
        Assert.Empty(tokens);
    }

    [Fact]
    public void Tokenize_HandlesNullInput()
    {
        // Arrange
        var tokenizer = new RegexTextTokenizer();

        // Act
        var tokens = tokenizer.Tokenize(null).ToArray();

        // Assert
        Assert.Empty(tokens);
    }
}
