using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos.Story;
using SPMS.Application.Story.Query;
using SPMS.Common;
using SPMS.ViewModel.Story;

namespace SPMS.Application.Services
{
    public class StoryService : IStoryService
    {
        private readonly IUserService _userService;
        private readonly ISpmsContext _context;
        private readonly IMapper _mapper;
        private readonly IGameService _gameService;
        private readonly IMediator _mediator;
        public StoryService(ISpmsContext spmsContext, IUserService userService, IMapper mapper, IGameService gameService, IMediator mediator)
        {
            _context = spmsContext;
            _userService = userService;
            _mapper = mapper;
            _gameService = gameService;
            _mediator = mediator;
        }

        public async Task<StoryOverviewDto> GetOverview()
        {
            var vm = new StoryOverviewDto();
            //TODO Pull real data
            vm.CurrentSeries = new SeriesDto() { Id = 1, Title = "Series One: Beyond the Darkness", Description = "**Markdown** *Content*" };
            vm.CurrentEpisode = new EpisodeDto() { Id = 1, Title = "Episode 1 - Discovery", Description = "**The crew** of Starbase Gamma begin to unravel the mysteries of the *Gamma Quadrant*.", Banner = "https://dummyimage.com/120x240/000000/fff.gif&text=Episode+One" };

            var dtos = await _mediator.Send(new TopStoryPostQuery() {Id = vm.CurrentEpisode.Id}, CancellationToken.None);

            vm.CurrentEpisode.Story = _mapper.Map<List<StoryPostDto>>(dtos);

            return vm;
        }

        public async Task<List<PostViewModel>> GetEpisodeStory(int activeEpisode)
        {
            var gameId = await _gameService.GetGameIdAsync();
            //TODO: Re-add publish date: .OrderBy(x => x.PublishedAt)
            return await _context.EpisodeEntry.Include(e => e.Episode).ThenInclude(e => e.Series).Include(e => e.EpisodeEntryPlayer).Include(e => e.EpisodeEntryStatus).Include(e => e.EpisodeEntryType).Where(e => e.EpisodeEntryType.Name == StaticValues.Post && e.EpisodeEntryStatus.Name == StaticValues.Published && e.Episode.Id == activeEpisode && e.Episode.Series.GameId == gameId).ProjectTo<PostViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }

    public interface IStoryService
    {
        Task<List<PostViewModel>> GetEpisodeStory(int activeEpisode);
        Task<StoryOverviewDto> GetOverview();
    }
}
