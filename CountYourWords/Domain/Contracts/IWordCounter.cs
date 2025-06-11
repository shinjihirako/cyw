namespace CountingWordBlazor.Domain.Contracts
{
    public interface IWordCounter
    {
        Dictionary<string, int> CountWords(IEnumerable<string> words);
    }
}
