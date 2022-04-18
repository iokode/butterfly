using System.Text;
using Newtonsoft.Json;

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
        var bodyObject = new
        {
            text = markDownString
        };
        string body = JsonConvert.SerializeObject(bodyObject);

        using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.github.com/markdown");
        request.Headers.Add("User-Agent", "iokode/butterfly");
        request.Content = new StringContent(body);

        using var response = await _HttpClient.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    private async Task<string> GetLegalMarkdownAsync(CancellationToken cancellationToken)
    {
        return await _HttpClient.GetStringAsync("https://raw.githubusercontent.com/iokode/blog/main/legal.md",
            cancellationToken);
    }
}