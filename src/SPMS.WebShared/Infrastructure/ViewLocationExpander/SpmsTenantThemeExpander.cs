using System.Linq;
using SPMS.WebShared.Infrastructure.Extensions;

namespace SPMS.WebShared.Infrastructure.ViewLocationExpander
{
    public class SpmsTenantThemeExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var theme = HttpContextExtensions.GetTenant(context.ActionContext.HttpContext)?.Theme ?? "Default";
            context.Values[StaticValues.ThemeKey] = theme;
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.Values.TryGetValue(StaticValues.ThemeKey, out var theme) && string.IsNullOrEmpty(context.AreaName))
            {
                viewLocations = new[]
                {
                    $"/Themes/{theme}/Views/{{1}}/{{0}}.cshtml",
                    $"/Themes/{theme}/Views/Shared/{{0}}.cshtml",
                }
                .Concat<string>(viewLocations);
            }

            return viewLocations;
        }
    }
}
