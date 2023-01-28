using System;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using IOKode.Butterfly.GitHubService;
using Microsoft.AspNetCore.Mvc;

namespace IOKode.Butterfly.Controllers;

[Route("/rss.xml")]
public class RssController : Controller
{
    private readonly FeedService _FeedService;

    public RssController(FeedService feedService)
    {
        _FeedService = feedService;
    }

    [HttpGet]
    public async Task<IActionResult> GetFeedAsync()
    {
        var feed = new SyndicationFeed("IOKode", "El blog de desarrollo de software de Ivan Montilla", new Uri("https://iokode.blog"));

        var items = (await _FeedService.GetFeedModels()).Select(post => new SyndicationItem(
            post.Title,
            post.HtmlContent,
            post.PostUrl)
        {
            Authors = { new SyndicationPerson("ivan@iokode.blog", "Ivan Montilla", "https://montyclt.net") },
            PublishDate = post.CreatedAt
        });

        feed.Items = items;

        var feedStream = await _GetFeedStreamAsync();
        return File(feedStream.ToArray(), "application/rss+xml; charset=utf-8");

        async Task<MemoryStream> _GetFeedStreamAsync()
        {
            var feedStream = new MemoryStream();
            await using var xmlWriter = XmlWriter.Create(feedStream, new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                NewLineHandling = NewLineHandling.Entitize,
                NewLineOnAttributes = true,
                Indent = true,
                Async = true
            });
            var rssFormatter = feed.GetRss20Formatter();
            rssFormatter.WriteTo(xmlWriter);
            await xmlWriter.FlushAsync();
            return feedStream;
        }
    }
}