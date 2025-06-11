using CountingWordBlazor.Domain.Contracts;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class RegexTextTokenizer : ITextTokenizer
{
    public IEnumerable<string> Tokenize(string? text)
    {
        if (string.IsNullOrEmpty(text))
            yield break;

        var matches = Regex.Matches(text, @"\b[\p{L}']+\b");
        foreach (Match match in matches)
            yield return match.Value.ToLowerInvariant();
    }
}
