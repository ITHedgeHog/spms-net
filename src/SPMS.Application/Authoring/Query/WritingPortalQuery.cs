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

            public WritingPortalQueryHandler(ISpmsContext db, IUserService userService, IMapper mapper)
            {
                _db = db;
                _userService = userService;
                _mapper = mapper;
            }

            public async Task<WritingPortalDto> Handle(WritingPortalQuery request, CancellationToken cancellationToken)
            {
                var vm = new WritingPortalDto
                {
                    IsCreateCharacterEnabled = _userService.IsPlayer(),
                    HasEpisode = _db.Episode.Include(e => e.Status).Any(e => e.Status.Name == StaticValues.Published),
                };

                var tmp = _db.EpisodeEntry
                    .Include(e => e.EpisodeEntryType)
                    .Include(e => e.Episode)
                    .Include(e => e.EpisodeEntryStatus)
                    .Include(p => p.EpisodeEntryPlayer).ThenInclude(p => p.Player)
                    .Where(e => e.EpisodeEntryType.Name == StaticValues.Post && e.EpisodeEntryStatus.Name == StaticValues.Pending).ToList();

                var owner = _userService.GetAuthId();
                vm.DraftPosts = _db.EpisodeEntry
                    .Include(e => e.EpisodeEntryType)
                    .Include(e => e.Episode)
                    .Include(p => p.EpisodeEntryPlayer).ThenInclude(p => p.Player)
                    .Include(e => e.EpisodeEntryStatus)
                    .Where(e => e.EpisodeEntryType.Name == StaticValues.Post && e.EpisodeEntryStatus.Name == StaticValues.Draft)
                    .ProjectTo<PostDto>(_mapper.ConfigurationProvider).ToList();
                vm.PendingPosts = _db.EpisodeEntry
                    .Include(e => e.EpisodeEntryType)
                    .Include(e => e.Episode)
                    .Include(p => p.EpisodeEntryPlayer).ThenInclude(p => p.Player)
                    .Include(e => e.EpisodeEntryStatus)
                    .Where(e => e.EpisodeEntryType.Name == StaticValues.Post && e.EpisodeEntryStatus.Name == StaticValues.Pending)
                    .ProjectTo<PostDto>(_mapper.ConfigurationProvider).ToList();
                var bios = _db.Biography.Include(b => b.Player).Where(x => x.Player.AuthString == owner);
                foreach (var bio in bios)
                {
                    vm.Characters.Add(bio.Id, bio.Firstname + " " + bio.Surname);
                }



                return new WritingPortalDto();
            }
        }
    }
}
