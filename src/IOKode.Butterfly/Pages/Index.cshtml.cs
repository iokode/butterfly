using IOKode.Butterfly.GitHubService;
using IOKode.Butterfly.GitHubService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IOKode.Butterfly.Pages;

public class IndexModel : PageModel
{
    private readonly IndexPostService _IndexPostService;
    
    public IEnumerable<PostResume> Resumes { get; private set; }

    public IndexModel(IndexPostService indexPostService)
    {
        _IndexPostService = indexPostService;
    }

    public async Task OnGetAsync([FromQuery] int page = 1)
    {
        int offset = (page - 1) * 2;
        Resumes = await _IndexPostService.GetResumesAsync(offset);
    }
}