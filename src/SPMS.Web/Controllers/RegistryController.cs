using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using SPMS.Common;

namespace SPMS.Web.Controllers
{
    [FeatureGate(FeatureFlags.Registry)]
    public class RegistryController : Controller
    {
        public IActionResult Index()
        {
            var vm = new Common.ViewModels.BaseViewModel();
            return View(vm);
        }
    }
}
