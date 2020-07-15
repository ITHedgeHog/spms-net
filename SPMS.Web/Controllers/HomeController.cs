using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPMS.Application.Services;
using SPMS.ViewModel.Home;
using SPMS.Web.Models;
using SPMS.Web.Service;

namespace SPMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameService _gameService;

        public HomeController(ILogger<HomeController> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        //[Route("")]
        //[Route("Home")]
        //[Route("Home/Index")]
        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel();
            return View(vm);
        }

        //[Route("Home/Privacy")]
        public async Task<IActionResult> Privacy()
        {
            var vm = new HomeViewModel();
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
