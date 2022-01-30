using System.Text.Json;
using GraphQL.Client.Abstractions;
using IOKode.Butterfly.GitHubService.Models;

namespace IOKode.Butterfly.GitHubService;

public class GitHubPostService
{
    private readonly IGraphQLClient _GraphQlClient;

    public GitHubPostService(IGraphQLClient graphQlClient)
    {
        _GraphQlClient = graphQlClient;
    }
}