using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SPMS.Web.Controllers
{
    public class StatusController : Controller
    {
        public IActionResult Index()
        {
            var vm = new Common.ViewModels.BaseViewModel();
            return View(vm);
        }
    }
}
