using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SPMS.Application.ViewModels;

namespace SPMS.Web.Controllers
{
    public class ApplicationController : Controller
    {
        public IActionResult Index()
        {
            var vm = new Common.ViewModels.ViewModel();
            return View(vm);
        }
    }
}
