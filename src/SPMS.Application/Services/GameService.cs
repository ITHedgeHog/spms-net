using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Common;
using SPMS.Domain.Models;

namespace SPMS.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly ISpmsContext _context;

        public GameService(ISpmsContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        private async Task<Game> GetGameAsync()
        {
            // Get Current URL
            var url = _httpContext.HttpContext.Request.Host.Host;

            // Get Matching Game

            var game = await _context.GameUrl.Include(gd => gd.Game).FirstOrDefaultAsync(gu => gu.Url == url);

            if (game != default(GameUrl))
            {
                return game.Game;
            }
            // Return Btd if nothing matches

            var g = await _context.Game.FirstAsync(gm => gm.Name == StaticValues.DefaultGameName);
            return g;
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

        public async Task<int> GetGameIdAsync()
        {
            return (await GetGameAsync()).Id;
        }
    }

    public interface IGameService
    {
        Task<int> GetGameIdAsync();
        Task<string> GetGameNameAsync();
        Task<string> GetSiteTitleAsync();
        Task<string> GetSiteDisclaimerAsync();
        Task<bool> GetReadonlyStatusAsync();
        Task<string> GetAnalyticsAsyncTask();
    }
}
