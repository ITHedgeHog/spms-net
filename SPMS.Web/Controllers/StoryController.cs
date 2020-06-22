using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SPMS.Web.Service;
using SPMS.Web.ViewModels;
using SPMS.Web.ViewModels.Story;

namespace SPMS.Web.Controllers
{
    public class StoryController : Controller
    {
        private readonly IStoryService _storyService;
        private readonly IMarkdownService _markdown;

        public StoryController(IStoryService storyService, IMarkdownService markdown)
        {
            _storyService = storyService;
            _markdown = markdown;
        }

        public IActionResult Index()
        {
            var vm = new SeriesOverviewViewModel();
            vm.CurrentSeries = new SeriesViewModel() { Id = 1, Title = "Series One: Beyond the Darkness", Description = _markdown.Render("**Markdown** *Content*") };
            vm.CurrentEpisode = new EpisodeViewModel() { Id = 1, Title = "Episode 1 - Discovery", Description = _markdown.Render("**The crew** of Starbase Gamma begin to unravel the mysteries of the *Gamma Quadrant*."), Banner = "https://dummyimage.com/120x240/000000/fff.gif&text=Episode+One"};
            return View(vm);
        }

        public IActionResult Sofar()
        {
            var vm = new ViewModel();
            return View(vm);
        }
    }
}
