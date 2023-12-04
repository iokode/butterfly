using IOKode.Butterfly.GitHubService.Models;
using Octokit.GraphQL;
using YamlDotNet.Serialization;

namespace IOKode.Butterfly.GitHubService;

public class PostService
{
    private readonly Connection gitHubClient;
    private readonly HttpClient httpClient;

    public PostService(Connection gitHubClient, HttpClient httpClient)
    {
        this.gitHubClient = gitHubClient;
        this.httpClient = httpClient;
    }

    public async Task<Post> GetBySlugAsync(int year, string slug)
    {
        var response = await httpClient.GetAsync($"https://raw.githubusercontent.com/iokode/blog/main/posts/{year}.yml");
        response.EnsureSuccessStatusCode();
        string ymlContent = await response.Content.ReadAsStringAsync();
        var deserializer = new DeserializerBuilder().Build();
        var entries = deserializer.Deserialize<PostResume[]>(ymlContent);
        var entry = entries.SingleOrDefault(x => x.Slug == slug);

        if (entry is null)
        {
            return null;
        }

        var query = new Query()
            .Repository("blog", "iokode")
            .Discussion(entry.Number)
            .Select(discussion => new Post
            {
                Title = discussion.Title,
                CreatedAt = discussion.CreatedAt.LocalDateTime,
                BodyHtml = discussion.BodyHTML,
                Number = entry.Number,
                Comments = discussion.Comments(null, null, null, null)
                    .AllPages()
                    .Select(comment => new Comment
                    {
                        Author = new Author
                        {
                            Username = comment.Author.Login,
                            AvatarUrl = comment.Author.AvatarUrl(null)
                        },
                        BodyHtml = comment.BodyHTML,
                        CreatedAt = comment.CreatedAt.LocalDateTime,
                        Replies = comment.Replies(null, null, null, null)
                            .AllPages()
                            .Select(reply => new CommentReply
                            {
                                Author = new Author
                                {
                                    Username = reply.Author.Login,
                                    AvatarUrl = reply.Author.AvatarUrl(null)
                                },
                                BodyHtml = reply.BodyHTML,
                                CreatedAt = reply.CreatedAt.LocalDateTime
                            }).ToList()
                    }).ToList()
            }).Compile();

        return await gitHubClient.Run(query);
    }
}