using IOKode.Butterfly.GitHubService.Models;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;

namespace IOKode.Butterfly.GitHubService;

public class FeedService
{
    private readonly Connection gitHubClient;

    public FeedService(Connection gitHubClient)
    {
        this.gitHubClient = gitHubClient;
    }

    public async Task<IEnumerable<FeedModel>> GetFeedModels()
    {
        var query = new Query()
            .Repository("blog", "iokode")
            .Discussions(
                categoryId: new ID("DIC_kwDOFLQiwM4CA0l-"),
                orderBy: new DiscussionOrder
                {
                    Direction = OrderDirection.Desc,
                    Field = DiscussionOrderField.CreatedAt
                })
            .AllPages()
            .Select(discussion => new FeedModel()
            {
                Title = discussion.Title,
                CreatedAt = discussion.CreatedAt.LocalDateTime,
                HtmlContent = discussion.BodyHTML,
                PostUrl = new Uri($"https://github.com/iokode/blog/discussions/{discussion.Number}")
            }).Compile();

        var result = this.gitHubClient.Run(query);
        return await result;
    }
}