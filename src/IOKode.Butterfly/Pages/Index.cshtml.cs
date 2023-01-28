using System.Collections.Generic;
using System.Threading.Tasks;
using IOKode.Butterfly.GitHubService;
using IOKode.Butterfly.GitHubService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IOKode.Butterfly.Pages;

public class IndexPage : PageModel
{
    private readonly ResumeService _ResumeService;

    public IEnumerable<PostResume> Resumes { get; private set; }

    [FromQuery(Name = "page")]
    public int CurrentPage { get; set; } = 1;

    public IndexPage(ResumeService resumeService)
    {
        _ResumeService = resumeService;
    }

    public async Task OnGetAsync()
    {
        int offset = (CurrentPage - 1) * 2;
        Resumes = await _ResumeService.GetResumesAsync(offset);
    }
}