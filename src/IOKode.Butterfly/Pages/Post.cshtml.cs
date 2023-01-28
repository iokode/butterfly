using System.Threading.Tasks;
using IOKode.Butterfly.GitHubService;
using IOKode.Butterfly.GitHubService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IOKode.Butterfly.Pages;

public class PostPage : PageModel
{
    private readonly PostService _PostService;
    public Post Post { get; private set; }

    public PostPage(PostService postService)
    {
        _PostService = postService;
    }

    public async Task OnGetAsync([FromRoute] int year, [FromRoute] string slug)
    {
        Post = await _PostService.GetBySlugAsync(year, slug);
    }
}