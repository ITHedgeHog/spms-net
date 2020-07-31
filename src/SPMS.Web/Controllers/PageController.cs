using Microsoft.AspNetCore.Mvc;
using SPMS.ViewModel;

namespace SPMS.Web.Controllers
{
    /// <summary>
    /// The page controller.
    /// </summary>
    public class PageController : Controller
    {
        /// <summary>
        /// Show the page
        /// </summary>
        /// <param name="slug">
        /// The page slug.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Show(string slug)
        {
            // todo: grab from db.
            var page = new PageViewModel()
            {
                UrlSlug = slug,
                Content = "**Markdown Test** Blah blah blah",
            };

            return View(page);
        }
    }
}
