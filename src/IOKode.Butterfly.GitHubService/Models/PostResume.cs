namespace IOKode.Butterfly.GitHubService.Models;

public class PostResume
{
    public int Id { get; set; }
    public string Slug { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Resume { get; set; }
    public int Comments { get; set; }
    public string Url { get; set; }
}