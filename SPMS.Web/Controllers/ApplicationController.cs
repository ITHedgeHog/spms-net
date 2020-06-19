using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SPMS.Web.ViewModels;

namespace SPMS.Web.Controllers
{
    public class ApplicationController : Controller
    {
        public IActionResult Index()
        {
            var vm = new ViewModel();
            return View(vm);
        }
    }
}
