using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SPMS.Web.Models;

namespace SPMS.Web.Service
{
    public class GameService : IGameService
    {
        private readonly IHttpContextAccessor _httpContext;
        private SpmsContext _context;

        public GameService(SpmsContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        private async Task<Game> GetGameAsync()
        {
            // Get Current URL
            var url = _httpContext.HttpContext.Request.Host.Host;

            // Get Matching Game

            var game = await _context.GameUrl.Include(g => g.Game).FirstOrDefaultAsync(gu => gu.Url == url);

            if (game != default(GameUrl))
            {
                return game.Game;
            }
            // Return Btd if nothing matches

            return await _context.Game.FirstAsync(g => g.Name == StaticValues.DefaultGameName);
        }

        public async Task<string> GetGameNameAsync()
        {
            return (await GetGameAsync()).Name;
        }

        public async Task<string> GetSiteTitleAsync()
        {
            return (await GetGameAsync()).SiteTitle;
        }

        public async Task<string> GetSiteDisclaimerAsync()
        {
            return (await GetGameAsync()).Disclaimer;
        }

        public async Task<bool> GetReadonlyStatusAsync()
        {
            return (await GetGameAsync()).IsReadonly;
        }

        public async Task<string> GetAnalyticsAsyncTask()
        {
            return (await GetGameAsync()).SiteAnalytics;
        }
    }

    public interface IGameService
    {
        Task<string> GetGameNameAsync();
        Task<string> GetSiteTitleAsync();
        Task<string> GetSiteDisclaimerAsync();
        Task<bool> GetReadonlyStatusAsync();
        Task<string> GetAnalyticsAsyncTask();
    }
}
