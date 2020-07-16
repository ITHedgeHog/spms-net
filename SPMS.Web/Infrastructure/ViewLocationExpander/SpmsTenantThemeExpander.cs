using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;
using SPMS.Common;
using SPMS.Web.Infrastructure.Extensions;

namespace SPMS.Web.Infrastructure.ViewLocationExpander
{
    public class SpmsTenantThemeExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values[StaticValues.ThemeKey] = context.ActionContext.HttpContext.GetTenant().Theme;
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.Values.TryGetValue(StaticValues.ThemeKey, out var theme))
            {
                viewLocations = new[] {
                        $"/Themes/{theme}/{{1}}/{{0}}.cshtml",
                        $"/Themes/{theme}/Shared/{{0}}.cshtml",
                        "/Views/{{1}}/{{0}}.cshtml",
                        "/Views/Shared/{{1}}/{{0}}.cshtml"
                    }
                    .Concat(viewLocations);
            }
            
            return viewLocations;
        }
    }
}
