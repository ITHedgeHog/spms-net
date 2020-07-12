using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace SPMS.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class Routes2Controller : Controller
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public Routes2Controller(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            this._actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        [HttpGet]
        [HttpPut]
        public IActionResult Index()
        {
            var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Select(x => new {
                Controller = x.RouteValues["controller"],
                Action = x.RouteValues["action"],
                Parameters = x.Parameters?.Select(param => new
                {
                    Name = param.Name,
                    Type = param.ParameterType.Name,
                }),
                Template = x.AttributeRouteInfo?.Template,
                Name = x.AttributeRouteInfo?.Name,
                Contraint = x.ActionConstraints,
            }).OrderBy(x => x.Controller).ToList();

            return Json(routes, new JsonSerializerOptions
            {
                WriteIndented = true,
            });
        }
    }
}
