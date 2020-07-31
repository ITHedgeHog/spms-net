using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SPMS.Web.Areas.player.Controllers
{
    [Area("player")]
    [Authorize(Policy = "Player")]

    public class PlayerDashboardController : Controller
    {
        [HttpGet("player/dashboard")]
        public IActionResult Index()
        {
            var vm = new Common.ViewModels.BaseViewModel();
            return View(vm);
        }
    }
}
