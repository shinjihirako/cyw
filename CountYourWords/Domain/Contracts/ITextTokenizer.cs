namespace CountingWordBlazor.Domain.Contracts
{
    public interface ITextTokenizer
    {
        IEnumerable<string> Tokenize(string text);
    }
}
