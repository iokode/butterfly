namespace IOKode.Butterfly.GitHubService.Models;

public class PostResume
{
    public int Number { get; internal set; }
    public string Slug { get; internal set; }
    public string Resume { get; internal set; }

    public string Title { get; internal set; }
    public DateTime CreatedAt { get; internal set; }
    public int Comments { get; internal set; }

    public string GitHubUrl => $"https://github.com/iokode/blog/discussions/{Number}";
    public string Url => $"/post/{CreatedAt.Year}/{Slug}";
}