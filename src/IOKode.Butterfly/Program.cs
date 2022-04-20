using System.Globalization;
using IOKode.Butterfly.GitHubService;
using IOKode.Butterfly.Options;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
using Octokit.GraphQL;

var culture = new CultureInfo("es-ES");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.All;
    });
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
    services.AddTransient<LegalService>();
}

void ConfigureApplication(WebApplication app)
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseForwardedHeaders();
    }

    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();
    app.MapRazorPages();
}

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(options =>
{
    var socketConfig = builder.Configuration.GetSection("Kestrel").GetSection("UnixSocket");
    if (socketConfig.Exists())
    {
        options.ListenUnixSocket(socketConfig.Value);
    }
});
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
ConfigureApplication(app);

await app.RunAsync();