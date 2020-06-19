using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using SPMS.Web.Service;
using SPMS.Web.ViewModels;

namespace SPMS.Web.Filter
{
    public class ViewModelFilter : ActionFilterAttribute
    {
        private readonly IGameService _gameService;
        private readonly IUserService _userService;
        private readonly bool _useAnalytics;

        public ViewModelFilter(IGameService gameService, IConfiguration config, IUserService userService)
        {
            _gameService = gameService;
            _userService = userService;
            _useAnalytics = config.GetValue("UseAnalytics", false);
        }

        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            // Do something before the action executes.

            // next() calls the action method.
            var resultContext = await next();
            // resultContext.Result is set.
            // Do something after the action executes.
            if (resultContext.Controller is Controller controller && controller.ViewData.Model is ViewModel)
            {
                var model = controller.ViewData.Model;
                ((ViewModel) model).GameName = await _gameService.GetGameNameAsync();
                ((ViewModel) model).SiteTitle = await _gameService.GetSiteTitleAsync();
                ((ViewModel) model).SiteDisclaimer = await _gameService.GetSiteDisclaimerAsync();
                ((ViewModel) model).IsReadOnly = await _gameService.GetReadonlyStatusAsync();
                ((ViewModel) model).SiteAnalytics = await _gameService.GetAnalyticsAsyncTask();
                 ((ViewModel) model).UseAnalytics = _useAnalytics;
                 ((ViewModel) model).IsAdmin = _userService.IsAdmin();
                 ((ViewModel) model).IsPlayer = _userService.IsPlayer();
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //if (filterContext.Controller is Controller controller)
            //{
            //    var model = controller.ViewData.Model;
            //    ((ViewModel) model).GameName = await _gameService.GetGameNameAsync(),
            //    ((ViewModel) model).SiteTitle = await _gameService.GetSiteTitleAsync(),
            //    (model as ViewModel).SiteDisclaimer = await _gameService.GetSiteDisclaimerAsync(),
            //    (model as ViewModel).IsReadOnly = await _gameService.GetReadonlyStatusAsync(),
            //    (model as ViewModel).SiteAnalytics = await _gameService.GetAnalyticsAsyncTask()
            //}

            base.OnActionExecuted(filterContext);
        }

    }
}
