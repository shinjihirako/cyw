using CountingWordBlazor.Domain.Contracts;
using Moq;

public static class MockFactory
{

    public static Mock<ITrie> GetTrieMock(bool searchResult)
    {
        var mock = new Mock<ITrie>();
        mock.Setup(t => t.Search(It.IsAny<string>())).Returns(searchResult);
        mock.Setup(t => t.Insert(It.IsAny<string>())).Verifiable();
        return mock;
    }

    public static Mock<ITextTokenizer> GetTokenizerMock(string[] tokens)
    {
        var mock = new Mock<ITextTokenizer>();
        mock.Setup(t => t.Tokenize(It.IsAny<string>())).Returns(tokens);
        return mock;
    }

    public static Mock<IWordCounter> GetWordCounterMock(Dictionary<string, int> fakeResult)
    {
        var mock = new Mock<IWordCounter>();
        mock.Setup(w => w.CountWords(It.IsAny<IEnumerable<string>>())).Returns(fakeResult);
        return mock;
    }
}
