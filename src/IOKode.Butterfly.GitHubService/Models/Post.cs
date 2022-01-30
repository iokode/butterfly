namespace IOKode.Butterfly.GitHubService.Models;

public class Post
{
    public string Slug { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
    public string BodyHtml { get; set; }
}