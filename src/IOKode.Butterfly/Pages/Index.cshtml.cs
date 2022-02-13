using IOKode.Butterfly.GitHubService;
using IOKode.Butterfly.GitHubService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IOKode.Butterfly.Pages;

public class IndexPage : PageModel
{
    private readonly IndexPostService _IndexPostService;

    public IEnumerable<PostEntry> Resumes { get; private set; }

    [FromQuery(Name = "page")]
    public int CurrentPage { get; set; } = 1;

    public IndexPage(IndexPostService indexPostService)
    {
        _IndexPostService = indexPostService;
    }

    public async Task OnGetAsync()
    {
        int offset = (CurrentPage - 1) * 2;
        Resumes = await _IndexPostService.GetResumesAsync(offset);
    }
}