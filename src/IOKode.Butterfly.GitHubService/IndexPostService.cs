using System.Text;
using System.Text.Json;
using GraphQL.Client.Abstractions;
using IOKode.Butterfly.GitHubService.Models;
using YamlDotNet.Serialization;

namespace IOKode.Butterfly.GitHubService;

public class IndexPostService
{
    private readonly HttpClient _HttpClient;
    private readonly IGraphQLClient _GraphQlClient;

    public IndexPostService(HttpClient httpClient, IGraphQLClient graphQlClient)
    {
        _HttpClient = httpClient;
        _GraphQlClient = graphQlClient;
    }

    public async Task<IEnumerable<PostResume>> GetResumesAsync(int offset = 0)
    {
        var resumes = (await _GetIndexFileAsync(offset)).ToArray();
        string query = _BuildQuery(resumes.Select(resume => resume.Id));
        await _RunQueryAsync(query, resumes);

        return resumes;
    }

    private async Task<IEnumerable<PostResume>> _GetIndexFileAsync(int offset = 0)
    {
        var response = await _HttpClient.GetAsync("https://raw.githubusercontent.com/iokode/blog/main/archive.yml");
        response.EnsureSuccessStatusCode();
        string yamlString = await response.Content.ReadAsStringAsync();

        var deserializer = new DeserializerBuilder().Build();
        var resume = deserializer.Deserialize<PostResume[]>(yamlString);
        return resume.Reverse().Skip(offset).Take(2);
    }

    private static string _BuildQuery(IEnumerable<int> ids)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append("query{");
        stringBuilder.Append("repository(owner:\"iokode\",name:\"blog\"){");
        foreach (int id in ids)
        {
            stringBuilder.Append($"d{id}:discussion(number:{id}){{");
            stringBuilder.Append("createdAt,title,bodyHTML,url,comments{totalCount}},");
        }
        stringBuilder.Length--;
        stringBuilder.Append("}}");

        return stringBuilder.ToString();
    }

    private async Task _RunQueryAsync(string query, PostResume[] resumes)
    {
        var response = await _GraphQlClient.SendQueryAsync<JsonDocument>(query);
        if (response.Errors?.Any() ?? false)
        {
            throw new Exception("Something went wrong while querying GitHub API");
        }

        var posts = response.Data.RootElement
            .GetProperty("repository")
            .EnumerateObject();

        for (int i = 0; i < resumes.Length; i++)
        {
            var resume = resumes[i];
            var post = posts.ToArray()[i];
            resume.CreatedAt = post.Value.GetProperty("createdAt").GetDateTimeOffset().LocalDateTime;
            resume.Title = post.Value.GetProperty("title").GetString()!;
            resume.Resume = post.Value.GetProperty("bodyHTML").GetString()!;
            resume.Comments = post.Value.GetProperty("comments").GetProperty("totalCount").GetInt32();
            resume.Url = post.Value.GetProperty("url").GetString()!;
        }
    }
}