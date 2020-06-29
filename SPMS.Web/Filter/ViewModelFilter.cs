using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using SPMS.Application.Services;
using SPMS.Application.ViewModels;
using SPMS.Web.Service;

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
            if (resultContext.Controller is Controller controller)
            {
                if (controller.ViewData.Model is ViewModel model)
                {
                    model.GameName = await _gameService.GetGameNameAsync();
                    model.SiteTitle = await _gameService.GetSiteTitleAsync();
                    model.SiteDisclaimer = await _gameService.GetSiteDisclaimerAsync();
                    model.IsReadOnly = await _gameService.GetReadonlyStatusAsync();
                    model.SiteAnalytics = await _gameService.GetAnalyticsAsyncTask();
                    model.UseAnalytics = _useAnalytics;
                    if (_userService.IsAuthenticated())
                    {
                        model.IsAdmin = _userService.IsAdmin();
                        model.IsPlayer = _userService.IsPlayer();
                        model.gravatar = await _userService.GetEmailAsync(CancellationToken.None);
                    }

                    var sha = Environment.GetEnvironmentVariable("COMMIT_SHA");
                    if (!string.IsNullOrEmpty(sha))
                    {
                        var last = 7;
                        if (sha.Length < 7)
                        {
                            last = sha.Length;
                        }
                        model.CommitShaLink = "https://github.com/ITHedgeHog/getspms/commit/" + sha.Substring(0, last);
                        model.CommitSha = sha.Substring(0, last);
                    }

                    var i = 1;
                }
            }
        }
    }
}
