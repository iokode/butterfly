namespace IOKode.Butterfly.GitHubService.Models;

public class FeedModel
{
    public string Title { get; init; }
    public string HtmlContent { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public Uri PostUrl { get; init; }
}