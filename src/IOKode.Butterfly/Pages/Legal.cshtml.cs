using System.Threading;
using System.Threading.Tasks;
using IOKode.Butterfly.GitHubService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IOKode.Butterfly.Pages;

public class Legal : PageModel
{
    public string LegalInformation { get; set; }
    
    public async Task OnGetAsync([FromServices] LegalService legalService, CancellationToken cancellationToken)
    {
        LegalInformation = await legalService.GetLegalTextInHtmlAsync(cancellationToken);
    }
}