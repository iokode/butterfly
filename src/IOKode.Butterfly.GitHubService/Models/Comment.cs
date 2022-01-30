namespace IOKode.Butterfly.GitHubService.Models;

public class Comment
{
    public DateTime CreatedAt { get; set; }
    public string Author { get; set; }
    public string AvatarUrl { get; set; }
    public string BodyHtml { get; set; }
}