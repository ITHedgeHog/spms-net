using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Story.Query;
using SPMS.Web.Infrastructure.Extensions;

namespace SPMS.Web.Controllers
{
    public class SyndicationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IIdentifierMask _mask;

        public SyndicationController(IMediator mediator, IIdentifierMask mask)
        {
            _mediator = mediator;
            _mask = mask;
        }

        [HttpGet("rss")]
        public async Task<IActionResult> Rss(CancellationToken cancellationToken)
        {
            var posts = await _mediator.Send(new SyndicationQuery(), cancellationToken).ConfigureAwait(true);

            var feed = new SyndicationFeed();
            feed.Copyright = new TextSyndicationContent($"{DateTime.Now.Year} {HttpContext.GetTenant().SiteTitle}");
            var items = new List<SyndicationItem>();

            foreach (var item in posts)
            {
                var hiddenId = _mask.HideId(item.Id);
                var postUrl = Url.Action("Show", "Story", new { id = hiddenId}, HttpContext.Request.Scheme);
                var title = item.Title;
                var description = $"New {item.Type} posted by {item.LastAuthor}";
                items.Add(new SyndicationItem(title, description, new Uri(postUrl), hiddenId, (DateTimeOffset)item.PublishedAt.Value ));
            }

            feed.Items = items;


            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                NewLineHandling = NewLineHandling.Entitize,
                NewLineOnAttributes = true,
                Indent = true,
                Async = true,
            };
            await using var stream = new MemoryStream();
            using (var xmlWriter = XmlWriter.Create(stream, settings))
            {
                var rssFormatter = new Rss20FeedFormatter(feed, false);
                rssFormatter.WriteTo(xmlWriter);
                await xmlWriter.FlushAsync().ConfigureAwait(true);
            }

            return File(stream.ToArray(), "application/rss+xml; charset=utf-8");
        }
    }
}
