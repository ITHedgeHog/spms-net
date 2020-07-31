using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.ServiceModel.Syndication;
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
using SPMS.Common;

namespace SPMS.Application.Story.Query
{
    public class SyndicationQuery : IRequest<List<PostDto>>
    {

        public class SyndicationQueryHandler : IRequestHandler<SyndicationQuery, List<PostDto>>
        {
            private readonly ISpmsContext _context;
            private TenantDto _tenant;

            private readonly IMapper _mapper;

            public SyndicationQueryHandler(ISpmsContext context, ITenantAccessor<TenantDto> accessor, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
                _tenant = accessor.Instance;
            }

            public async Task<List<PostDto>> Handle(SyndicationQuery request, CancellationToken cancellationToken)
            {
                var episodeIds = await _context.Episode.Include(e => e.Series).Include(e => e.Status)
                     .Where(e => e.Series.GameId == _tenant.Id && e.Status.Name == StaticValues.Published)
                     .Select(x => x.Id).ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(true);

                var posts = await _context.EpisodeEntry.Include(e => e.EpisodeEntryStatus)
                    .Include(e => e.EpisodeEntryType)
                    .Include(e => e.EpisodeEntryPlayer)
                    .Where(e => episodeIds.Contains(e.EpisodeId) && e.PublishedAt <= DateTime.UtcNow && e.EpisodeEntryStatus.Name == StaticValues.Published).OrderByDescending(e => e.Timeline)
                    .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken).ConfigureAwait(true);
                return posts;
            }
        }
    }
}
