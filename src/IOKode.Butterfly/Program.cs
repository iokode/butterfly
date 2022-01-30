using System.Net.Http.Headers;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using IOKode.Butterfly.GitHubService;
using IOKode.Butterfly.Options;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddTransient<IndexPostService>();
builder.Services.AddHttpClient<IndexPostService>();
builder.Services.Configure<GitHubOptions>(builder.Configuration.GetSection("GitHub"));
builder.Services.AddTransient<IGraphQLClient>(sp =>
{
    var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient();
    var ghOptions = sp.GetRequiredService<IOptions<GitHubOptions>>();
    httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"bearer {ghOptions.Value.AuthToken}");
    var graphQlHttpClient = new GraphQLHttpClient(new GraphQLHttpClientOptions()
    {
        EndPoint = new Uri("https://api.github.com/graphql"),
    }, new SystemTextJsonSerializer(), httpClient);

    return graphQlHttpClient;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

await app.RunAsync();