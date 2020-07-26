using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SPMS.Web.Controllers
{
    [Authorize(Policy = "administrator")]
    public class DebugController : Controller
    {
        public IActionResult Index()
        {
            var vm = new Common.ViewModels.BaseViewModel();
            return View(vm);
        }
    }
}
