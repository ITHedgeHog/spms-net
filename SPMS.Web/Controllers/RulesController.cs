using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPMS.Application.Services;
using SPMS.ViewModel.Rules;
using SPMS.Web.Models;
using SPMS.Web.Service;

namespace SPMS.Web.Controllers
{
    public class RulesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var vm = new RulesViewModel();
            return View(vm);
        }
    }
}
