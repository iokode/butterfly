namespace IOKode.Butterfly.GitHubService.Models;

public class Post
{
    public int Number { get; init; }
    public string Title { get; init; }
    public DateTime CreatedAt { get; init; }
    public string BodyHtml { get; init; }
    public IEnumerable<Comment> Comments { get; init; }
}