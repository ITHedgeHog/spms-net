using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Application.Dtos.Player;
using SPMS.Application.Dtos.Story;
using SPMS.Common;

namespace SPMS.Application.Authoring.Query
{
    public class WritingPortalQuery  : IRequest<WritingPortalDto>
    {

        public class WritingPortalQueryHandler : IRequestHandler<WritingPortalQuery, WritingPortalDto>
        {
            private readonly ISpmsContext _db;
            private readonly IUserService _userService;
            private readonly IMapper _mapper;
            private readonly ITenantAccessor<TenantDto> _tenant;

            public WritingPortalQueryHandler(ISpmsContext db, IUserService userService, IMapper mapper, ITenantAccessor<TenantDto> tenant)
            {
                _db = db;
                _userService = userService;
                _mapper = mapper;
                _tenant = tenant;
            }

            public async Task<WritingPortalDto> Handle(WritingPortalQuery request, CancellationToken cancellationToken)
            {
                var gameId = _tenant.Instance.Id;
                var userId = _userService.GetId();
                var dto = new WritingPortalDto
                {
                    CanPost = _db.Episode
                        .Include(e => e.Status)
                        .Include(e => e.Series)
                        .Any(e => e.Status.Name == StaticValues.Published && e.Series.GameId == gameId),
                    DraftPosts = GetPostsByStatus(userId, StaticValues.Draft, gameId),
                    PendingPosts = GetPostsByStatus(userId, StaticValues.Pending, gameId),
                };


                return dto;
            }

            private List<PostDto> GetPostsByStatus(int userId, string statusName, int gameId)
            {
                return _db.EpisodeEntry
                    .Include(e => e.EpisodeEntryType)
                    .Include(e => e.Episode).ThenInclude(e => e.Series)
                    .Include(e => e.EpisodeEntryStatus)
                    .Include(p => p.EpisodeEntryPlayer).ThenInclude(p => p.Player)
                    .Where(e => e.EpisodeEntryStatus.Name == statusName
                                 && e.Episode.Series.GameId == gameId
                                 && e.EpisodeEntryPlayer.Any(x => x.PlayerId == userId)
                               )
                    .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                    .ToList();
            }
        }
    }
}
