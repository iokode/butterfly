using IOKode.Butterfly.GitHubService;
using IOKode.Butterfly.Options;
using Microsoft.Extensions.Options;
using Octokit.GraphQL;

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddRazorPages();
    services.AddHttpClient();
    services.AddTransient<PostService>();
    services.AddHttpClient<PostService>();
    services.Configure<GitHubOptions>(configuration.GetSection("GitHub"));
    services.AddTransient(sp =>
    {
        var options = sp.GetRequiredService<IOptions<GitHubOptions>>().Value;
        return new Connection(new ProductHeaderValue("IOKode.Blog"), options.AuthToken);
    });
    services.AddTransient<ResumeService>();
    services.AddTransient<ArchiveService>();
}

void ConfigureApplication(WebApplication app)
{
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
}

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
ConfigureApplication(app);

await app.RunAsync();