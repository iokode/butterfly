using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IOKode.Butterfly.GitHubService;
using IOKode.Butterfly.GitHubService.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IOKode.Butterfly.Pages;

public class ArchivePage : PageModel
{
    private readonly ArchiveService _ArchiveService;

    public IEnumerable<ArchiveGroup> Archive { get; set; }

    public ArchivePage(ArchiveService archiveService)
    {
        _ArchiveService = archiveService;
    }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Archive = await _ArchiveService.GetGroupsAsync(cancellationToken);
    }
}