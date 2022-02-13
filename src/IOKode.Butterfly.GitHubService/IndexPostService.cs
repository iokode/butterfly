using IOKode.Butterfly.GitHubService.Models;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using YamlDotNet.Serialization;

namespace IOKode.Butterfly.GitHubService;

public class IndexPostService
{
    private readonly Connection _GitHubClient;

    public IndexPostService(Connection gitHubClient)
    {
        _GitHubClient = gitHubClient;
    }

    public async Task<IEnumerable<PostEntry>> GetResumesAsync(int offset = 0, int take = 2)
    {
        var filesQuery = new Query()
            .Repository("blog", "iokode")
            .Object("HEAD:posts").Cast<Tree>()
            .Entries
            .Select(x => new
            {
                Name = x.Name,
                Content = x.Object.Cast<Blob>().Text
            })
            .Compile();

        var files = await _GitHubClient.Run(filesQuery);
        files = files.Reverse();

        var entries = new List<PostEntry>();
        foreach (var file in files)
        {
            var deserializer = new DeserializerBuilder().Build();
            entries.AddRange(deserializer.Deserialize<PostEntry[]>(file.Content));
        }

        entries = entries.Skip(offset).Take(take).ToList();
        foreach (var entry in entries)
        {
            var query = new Query()
                .Repository("blog", "iokode")
                .Discussion(entry.Number)
                .Select(x => new
                {
                    x.Title,
                    CreatedAt = x.CreatedAt.LocalDateTime,
                    Comments = x.Comments(null, null, null, null).TotalCount
                })
                .Compile();

            var response = await _GitHubClient.Run(query);

            entry.Title = response.Title;
            entry.CreatedAt = response.CreatedAt;
            entry.Comments = response.Comments;
        }

        return entries;
    }
}