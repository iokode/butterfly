using Markdig;

namespace IOKode.Butterfly.GitHubService;

public class LegalService
{
    private readonly HttpClient httpClient;

    public LegalService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<string> GetLegalTextInHtmlAsync(CancellationToken cancellationToken)
    {
        string legalTextInMarkdown = await GetLegalTextInMarkdownAsync(cancellationToken);
        string legalTextInHtml = Markdown.ToHtml(legalTextInMarkdown);

        return legalTextInHtml;
    }

    private async Task<string> GetLegalTextInMarkdownAsync(CancellationToken cancellationToken)
    {
        string legalMarkdown =
            await this.httpClient.GetStringAsync("https://raw.githubusercontent.com/iokode/blog/main/legal.md",
                cancellationToken);

        return legalMarkdown;
    }
}