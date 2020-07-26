using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SPMS.Application.Story.Query;
using SPMS.ViewModel.Story;

namespace SPMS.Web.Controllers
{
    /// <summary>
    /// Story Controller
    /// </summary>
    [Route("story")]
    public class StoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator Pipeline.</param>
        /// <param name="mapper">AutoMapper Instance.</param>
        public StoryController(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        /// <summary>
        /// Display Index.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("")]
        [Route("index")]

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var query = new StoryOverviewQuery();
            var vm = await _mediator.Send(query, cancellationToken).ConfigureAwait(true);
            return View(vm);
        }

        /// <summary>
        /// Show the Story So Far Page.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet("SoFar")]
        public IActionResult Sofar()
        {
            var vm = new Common.ViewModels.BaseViewModel();
            return View(vm);
        }

        /// <summary>
        /// Show a post entry.
        /// </summary>
        /// <param name="id">Id of entry.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Show(string id, CancellationToken cancellationToken)
        {
            var dto = await _mediator.Send(new StoryPostQuery() {Id = id}, cancellationToken).ConfigureAwait(true);
            var vm = _mapper.Map<StoryPostViewModel>(dto);
            return View(vm);
        }
    }
}
