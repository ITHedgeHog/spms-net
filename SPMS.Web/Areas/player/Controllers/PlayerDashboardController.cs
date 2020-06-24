using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPMS.Web.ViewModels;

namespace SPMS.Web.Areas.player.Controllers
{
    [Area("player")]
    [Authorize(Policy = "Player")]

    public class PlayerDashboardController : Controller
    {
        [HttpGet("player/dashboard")]
        public IActionResult Index()
        {
            var vm = new ViewModel();
            return View(vm);
        }
    }
}
