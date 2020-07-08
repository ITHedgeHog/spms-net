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

namespace SPMS.Application.Story.Query
{
    public class StoryPostQuery : IRequest<StoryPostDto>
    {
        public string Id { get; set; }

        public class StoryPostQueryHandler : IRequestHandler<StoryPostQuery, StoryPostDto>
        {
            private readonly ISpmsContext _db;
            private readonly IMapper _mapper;
            private readonly IIdentifierMask _mask;

            public StoryPostQueryHandler(ISpmsContext db, IMapper mapper, IIdentifierMask mask)
            {
                _db = db;
                _mapper = mapper;
                _mask = mask;
            }

            public async Task<StoryPostDto> Handle(StoryPostQuery request, CancellationToken cancellationToken)
            {
                var unmaskedId = _mask.RevealId(request.Id);
                var publishedId = _db.EpisodeEntryStatus.First(x => x.Name == StaticValues.Published).Id;
                var entity = await _db.EpisodeEntry.Where(x => x.Id == unmaskedId && x.EpisodeEntryStatusId == publishedId)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                var dto = _mapper.Map<StoryPostDto>(entity);

                return dto;
            }
        }
    }
}
