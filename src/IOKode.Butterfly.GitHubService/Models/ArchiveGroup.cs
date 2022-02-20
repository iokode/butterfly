namespace IOKode.Butterfly.GitHubService.Models;

public record ArchiveGroup
{
    public int Year { get; }
    public IEnumerable<PostArchiveEntry> Entries { get; }

    public ArchiveGroup(int year, IEnumerable<PostArchiveEntry> entries)
    {
        Year = year;
        Entries = entries.ToArray();
    }
}