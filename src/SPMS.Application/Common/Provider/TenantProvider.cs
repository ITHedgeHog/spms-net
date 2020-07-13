using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Common;
using SPMS.Domain.Models;

namespace SPMS.Application.Common.Provider
{
    public class TenantProvider : ITenantProvider
    {
        private readonly ISpmsContext _db;

        public TenantProvider(ISpmsContext db)
        {
            _db = db;
        }

        public async Task<Game> GetTenantAsync(string url, CancellationToken cancellationToken)
        {
            var game = await _db.Game.Include(gd => gd.Url).Where(x => x.Url.Any(y => y.Url == url)).FirstOrDefaultAsync(cancellationToken: cancellationToken) ??
                       await _db.Game.Include(g => g.Url).FirstAsync(gm => gm.Name == StaticValues.TestGame, cancellationToken: cancellationToken);

            return game;
        }
    }
}
