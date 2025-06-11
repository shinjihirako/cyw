
namespace CountingWordBlazor.Domain.Contracts
{
    public interface ITrie
    {
        void Insert(string word);
        bool Search(string word);
    }
}
