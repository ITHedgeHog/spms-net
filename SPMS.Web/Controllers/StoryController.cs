using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SPMS.Application.Services;
using SPMS.Application.Story.Query;
using SPMS.ViewModel.Story;

namespace SPMS.Web.Controllers
{
    public class StoryController : Controller
    {
        private readonly IStoryService _storyService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
       

        public StoryController(IStoryService storyService, IMediator mediator, IMapper mapper)
        {
            _storyService = storyService;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var query = new StoryOverviewQuery();
            var vm = await _mediator.Send(query, cancellationToken); 
            return View(vm);
        }

        public IActionResult Sofar()
        {
            var vm = new Common.ViewModels.BaseViewModel();
            return View(vm);
        }

        public async Task<IActionResult> Show(string id, CancellationToken cancellationToken)
        {
            var dto = await _mediator.Send(new StoryPostQuery() {Id = id}, cancellationToken);
            var vm = _mapper.Map<StoryPostViewModel>(dto);
            return View(vm);
        }
    }
}
