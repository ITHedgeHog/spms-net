using System.Linq;

namespace SPMS.WebShared.Infrastructure
{
    public class RouteTranslator : DynamicRouteValueTransformer
    {
        public async override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            var slug = ((string)values["slug"]);

            if (string.IsNullOrEmpty(slug))
            {
                values["controller"] = "Home";
                values["action"] = "Index";
                return values;
            }


            List<string> match = ((WhiteList[])Enum.GetValues(typeof(WhiteList))).Select(x => x.ToString())
                                                                                 .ToList();
            if (Enumerable.Any<string>(match, x => x.ToLower() == slug.ToLower()))
            {
                var i = 1;
                values["controller"] = slug;
                values["action"] = "Index";
                return values;
            }

            values["controller"] = "Page";
            values["action"] = "Show";
            
            return values;
        }

       

        public enum WhiteList
        {
            home,
        }
    }
}
