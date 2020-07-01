﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPMS.Application.ViewModels;

namespace SPMS.Web.Controllers
{
    [Authorize(Policy = "administrator")]
    public class DebugController : Controller
    {
        public IActionResult Index()
        {
            var vm = new Common.ViewModels.ViewModel();
            return View(vm);
        }
    }
}
