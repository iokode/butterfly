namespace IOKode.Butterfly.GitHubService.Models;

public class PostArchiveEntry
{
    public string Slug { get; init; }
    public string Title { get; init; }
    public DateTime Date { get; init; }

    public string Url => $"/post/{Date.Year}/{Slug}";
}