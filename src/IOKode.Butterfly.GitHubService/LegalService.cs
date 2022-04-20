using Markdig;

namespace IOKode.Butterfly.GitHubService;

public class LegalService
{
    private readonly HttpClient _HttpClient;

    public LegalService(HttpClient httpClient)
    {
        _HttpClient = httpClient;
    }

    public async Task<string> GetLegalHtmlAsync(CancellationToken cancellationToken)
    {
        string markDownString = await GetLegalMarkdownAsync(cancellationToken);
        return Markdown.ToHtml(markDownString);
    }

    private async Task<string> GetLegalMarkdownAsync(CancellationToken cancellationToken)
    {
        return await _HttpClient.GetStringAsync("https://raw.githubusercontent.com/iokode/blog/main/legal.md",
            cancellationToken);
    }
}