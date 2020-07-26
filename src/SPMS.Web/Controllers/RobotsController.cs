using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SPMS.Application.Admin.Query;

namespace SPMS.Web.Controllers
{

    public class RobotsController : Controller
    {
        private readonly IMediator _mediator;

        public RobotsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("robots.txt")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var dto = await _mediator.Send(new SeoQuery(), cancellationToken);
            var block = "User-Agent: *\n\rDisallow: /";

            var crawl = "User-Agent: *\n\rAllow: /";
            if (dto.IsSpiderable)
                return Content(crawl);

            return Content(block);
        }
    }
}
