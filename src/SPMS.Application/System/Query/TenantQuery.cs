using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Common;

namespace SPMS.Application.System.Query
{
    public class TenantQuery : IRequest<TenantDto>
    {
        public string Url { get; set; }

        public class TenantQueryHandler : IRequestHandler<TenantQuery, TenantDto>
        {
            private readonly ISpmsContext _db;
            private readonly IUserService _userService;
            private readonly IMapper _mapper;

            public TenantQueryHandler(ISpmsContext db, IUserService userService, IMapper mapper)
            {
                _db = db;
                _userService = userService;
                _mapper = mapper;
            }

            public async Task<TenantDto> Handle(TenantQuery request, CancellationToken cancellationToken)
            {
                // Get Matching Game
                var game = await _db.Game.Include(gd => gd.Url).Where(x => x.Url.Any(y => y.Url == request.Url)).FirstOrDefaultAsync(cancellationToken: cancellationToken) ??
                           await _db.Game.Include(g => g.Url).FirstAsync(gm => gm.Name == StaticValues.TestGame, cancellationToken: cancellationToken);



                var dto = new TenantDto();
                //model.SiteTitle = await _gameService.GetSiteTitleAsync();
                //model.SiteDisclaimer = await _gameService.GetSiteDisclaimerAsync();
                //model.IsReadOnly = await _gameService.GetReadonlyStatusAsync();
                //model.SiteAnalytics = await _gameService.GetAnalyticsAsyncTask();
                //model.UseAnalytics = !string.Isnu;

                _mapper.Map(game, dto);
                

                if (_userService.IsAuthenticated())
                {
                    dto.IsAdmin = _userService.IsAdmin();
                    dto.IsPlayer = _userService.IsPlayer();
                    dto.gravatar = await _userService.GetEmailAsync(CancellationToken.None);
                }

                var sha = Environment.GetEnvironmentVariable("COMMIT_SHA");
                if (!string.IsNullOrEmpty(sha))
                {
                    var last = 7;
                    if (sha.Length < 7)
                    {
                        last = sha.Length;
                    }
                    dto.CommitShaLink = "https://github.com/ITHedgeHog/getspms/commit/" + sha.Substring(0, last);
                    dto.CommitSha = sha.Substring(0, last);
                }

                var i = 1;

                return dto;


            }
        }
    }
}
