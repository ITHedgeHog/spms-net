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
       

        public StoryController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = await _storyService.GetOverview(); 
            return View(vm);
        }

        public IActionResult Sofar()
        {
            var vm = new ViewModel();
            return View(vm);
        }
    }
}
