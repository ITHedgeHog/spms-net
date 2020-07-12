using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SPMS.Application.Common.Interfaces;
using SPMS.Common;
using SPMS.Domain.Models;
using NotImplementedException = System.NotImplementedException;

namespace SPMS.Application.Services
{
    public class GameService : IGameService
    {
        private readonly HttpContext _httpContext;
        private readonly ISpmsContext _context;

        public GameService(ISpmsContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext.HttpContext;
        }

        public async Task<Game> GetGameAsync()
        {
            // Get Current URL
            var url = _httpContext.Request.Host.Host;

            // Get Matching Game

            var game = await _context.Game.Include(gd => gd.Url).Where(x =>x.Url.Any(y =>y.Url == url)).FirstOrDefaultAsync();

            if (game != null)
            {
                return game;
            }
            
            var defaultGame = await _context.Game.Include(g => g.Url).FirstAsync(gm => gm.Name == StaticValues.TestGame);
            return defaultGame;
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

        public byte[] GetGameKey(byte[] generateKey)
        {
            var game = GetGameAsync().Result;

            if (game.GameKey == null || game.GameKey.Length == 0)
            {
                game.GameKey = generateKey;
                _context.Game.Update(game);
                _context.SaveChanges();
                return generateKey;
            }

            return game.GameKey;
        }

        public async Task<int> GetGameIdAsync()
        {
            return (await GetGameAsync()).Id;
        }
    }

    public interface IGameService
    {
        Task<Game> GetGameAsync();
        Task<int> GetGameIdAsync();
        Task<string> GetGameNameAsync();
        Task<string> GetSiteTitleAsync();
        Task<string> GetSiteDisclaimerAsync();
        Task<bool> GetReadonlyStatusAsync();
        Task<string> GetAnalyticsAsyncTask();
        byte[] GetGameKey(byte[] generateKey);
    }
}
