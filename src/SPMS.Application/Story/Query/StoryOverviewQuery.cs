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
using SPMS.Application.Dtos.Story;
using SPMS.Application.Services;
using SPMS.Common;
using SPMS.Domain.Models;
using SPMS.ViewModel.Story;

namespace SPMS.Application.Story.Query
{
    public class StoryOverviewQuery : IRequest<StoryOverviewDto>
    {
        public StoryOverviewQuery()
        {
            OrderByTimeline = false;
        }

        public bool OrderByTimeline { get; set; }
        public class StoryOverviewQueryHandler : IRequestHandler<StoryOverviewQuery, StoryOverviewDto>
        {
            private readonly ISpmsContext _db;
            private readonly IMapper _mapper;
            private readonly IGameService _gameService;

            public StoryOverviewQueryHandler(ISpmsContext db, IMapper mapper, IGameService gameService)
            {
                _db = db;
                _mapper = mapper;
                _gameService = gameService;
            }

            public async Task<StoryOverviewDto> Handle(StoryOverviewQuery request, CancellationToken cancellationToken)
            {
                var gameId = await _gameService.GetGameIdAsync().ConfigureAwait(true);
                var episodeEntryStatusPublished = await _db.EpisodeEntryStatus.FirstAsync(x => x.Name == StaticValues.Published, cancellationToken: cancellationToken).ConfigureAwait(true);
                var currentSeries = await _db.Series.Where(s => s.GameId == gameId && s.IsActive)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken).ConfigureAwait(true);

                var activeStatus = await _db.EpisodeStatus.FirstAsync(x => x.Name == StaticValues.Published, cancellationToken: cancellationToken).ConfigureAwait(true);
                var archivedStatus = await _db.EpisodeStatus.FirstAsync(x => x.Name == StaticValues.Archived, cancellationToken: cancellationToken).ConfigureAwait(true);
                var currentEpisode =
                    await _db.Episode.FirstAsync(x => x.SeriesId == currentSeries.Id && x.StatusId == activeStatus.Id, cancellationToken: cancellationToken).ConfigureAwait(true);
                var currentStories =  _db.EpisodeEntry.Include(e => e.EpisodeEntryStatus)
                    .Include(e => e.EpisodeEntryType)
                    .Where(x => x.EpisodeId == currentEpisode.Id  && x.EpisodeEntryStatusId == episodeEntryStatusPublished.Id);

                if (request.OrderByTimeline)
                {
                    await currentStories.OrderByDescending(x => x.Timeline).Take(5).ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(true);
                }
                else
                {
                    await currentStories.OrderByDescending(x => x.PublishedAt).Take(5).ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(true);
                }

                var vm = new StoryOverviewDto();
                vm.SeriesNumber = await _db.Series.Where(s => s.GameId == gameId && s.IsActive)
                    .CountAsync(cancellationToken: cancellationToken).ConfigureAwait(true);
                vm.EpisodeNumber = await _db.Episode.CountAsync(
                    x => x.SeriesId == currentSeries.Id && (x.StatusId == activeStatus.Id || x.StatusId == archivedStatus.Id) ,
                    cancellationToken: cancellationToken).ConfigureAwait(true);
                vm.CurrentSeries = _mapper.Map<SeriesDto>(currentSeries);
                vm.CurrentEpisode = _mapper.Map<EpisodeDto>(currentEpisode);
                vm.CurrentEpisode.Story = _mapper.Map<List<StoryPostDto>>(currentStories);

                return vm;
            }
        }
    }
}
