using IOKode.Butterfly.GitHubService.Models;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using YamlDotNet.Serialization;

namespace IOKode.Butterfly.GitHubService;

public class ArchiveService
{
    private readonly Connection gitHubClient;

    public ArchiveService(Connection gitHubClient)
    {
        this.gitHubClient = gitHubClient;
    }

    public async Task<IEnumerable<ArchiveGroup>> GetGroupsAsync(CancellationToken cancellationToken)
    {
        var query = new Query()
            .Repository("blog", "iokode")
            .Object("HEAD:posts").Cast<Tree>()
            .Entries
            .Select(entry => new
            {
                entry.Name,
                Yaml = entry.Object.Cast<Blob>().Text
            })
            .Compile();

        var discussionQuery = new Query()
            .Repository("blog", "iokode")
            .Discussions()
            .AllPages()
            .Select(discussion => new
            {
                discussion.CreatedAt,
                discussion.Title,
                discussion.Number
            })
            .Compile();

        var discussionsTask = this.gitHubClient.Run(discussionQuery, cancellationToken: cancellationToken);
        var filesTask = this.gitHubClient.Run(query, cancellationToken: cancellationToken);

        var groups = new List<ArchiveGroup>();
        foreach (var file in await filesTask)
        {
            var deserializer = new DeserializerBuilder().Build();
            var resumes = deserializer.Deserialize<PostResume[]>(file.Yaml);

            var entries = new List<PostArchiveEntry>();
            foreach (var resume in resumes)
            {
                var discussion = (await discussionsTask).First(discussion => discussion.Number == resume.Number);
                var entry = new PostArchiveEntry
                {
                    Date = discussion.CreatedAt.LocalDateTime,
                    Slug = resume.Slug,
                    Title = discussion.Title
                };
                entries.Add(entry);
            }

            var group = new ArchiveGroup(entries[0].Date.Year, entries);
            groups.Add(group);
        }

        groups.Reverse();
        return groups;
    }
}