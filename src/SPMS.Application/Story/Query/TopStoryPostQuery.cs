using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos.Story;
using SPMS.Application.Services;
using SPMS.Common;
using SPMS.Domain.Models;

namespace SPMS.Application.Story.Query
{
    public class TopStoryPostQuery : IRequest<List<StoryPostDto>>
    {
        public int Id { get; set; }

        public class TopStoryPostQueryHandler : IRequestHandler<TopStoryPostQuery, List<StoryPostDto>>
        {
            private readonly ISpmsContext _db;
            private readonly IMapper _mapper;
            private readonly IIdentifierMask _mask;

            public TopStoryPostQueryHandler(ISpmsContext db, IMapper mapper, IIdentifierMask mask)
            {
                _db = db;
                _mapper = mapper;
                _mask = mask;
            }

            public async Task<List<StoryPostDto>> Handle(TopStoryPostQuery request, CancellationToken cancellationToken)
            {
                var publishedId = _db.EpisodeEntryStatus.First(x => x.Name == StaticValues.Published).Id;
                var entity = _db.EpisodeEntry
                    .Where(x => x.EpisodeId == request.Id && x.EpisodeEntryStatusId == publishedId)
                    .OrderByDescending(x => x.Id).Take(5).ToList();

                var dto = _mapper.Map<List<EpisodeEntry>, List<StoryPostDto>>(entity);

                return dto;
            }
        }
    }
}
