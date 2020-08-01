using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Common;
using SPMS.Domain.Models;

namespace SPMS.Application.Authoring.Command.CreatePost
{
    public class CreatePost : IRequest<int>
    {


        public class CreatePostHandler : IRequestHandler<CreatePost, int>
        {
            private readonly ISpmsContext _db;
            private readonly ITenantAccessor<TenantDto> _tenant;
            private readonly IUserService _userService;

            public CreatePostHandler(ISpmsContext db, ITenantAccessor<TenantDto> tenant, IUserService userService)
            {
                _db = db;
                _tenant = tenant;
                _userService = userService;
            }

            public async Task<int> Handle(CreatePost request, CancellationToken cancellationToken)
            {
                var gameId = _tenant.Instance.Id;
                var activeEpisodeId = await _db.Episode
                    .Include(e => e.Series)
                    .Include(e => e.Status)
                    .CountAsync(e => e.Series.GameId == gameId && (e.Status.Name == StaticValues.Published || e.Status.Name == StaticValues.Archived), cancellationToken: cancellationToken);
                
                var entity = new EpisodeEntry();
                var episodeEntryStatus = 
                entity.EpisodeEntryStatusId = (await _db.EpisodeEntryStatus.FirstAsync(
                    x => x.Name == StaticValues.Draft,
                    cancellationToken: cancellationToken)).Id;
                entity.EpisodeEntryTypeId = (await _db.EpisodeEntryType.FirstAsync(x => x.Name == StaticValues.Post,
                    cancellationToken: cancellationToken)).Id;
                entity.EpisodeId = activeEpisodeId;
                await _db.EpisodeEntry.AddAsync(entity, cancellationToken: cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
                entity.EpisodeEntryPlayer =
                    new Collection<EpisodeEntryPlayer>()
                        {new EpisodeEntryPlayer() {PlayerId = _userService.GetId(), EpisodeEntryId = entity.Id}};
                _db.EpisodeEntry.Update(entity);
                await _db.SaveChangesAsync(cancellationToken);


                return entity.Id;
            }
        }
    }
}
