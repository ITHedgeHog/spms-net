using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos.Story;
using SPMS.Common;

namespace SPMS.Application.Services
{
    public class StoryService : IStoryService
    {
        private readonly IUserService _userService;
        private readonly ISpmsContext _context;
        private readonly IMapper _mapper;
        private readonly IGameService _gameService;
        public StoryService(ISpmsContext spmsContext, IUserService userService, IMapper mapper, IGameService gameService)
        {
            _context = spmsContext;
            _userService = userService;
            _mapper = mapper;
            _gameService = gameService;
        }

        public async Task<SeriesOverviewViewModel> GetOverview()
        {
            var vm = new SeriesOverviewViewModel();
            //TODO Pull real data
            vm.CurrentSeries = new SeriesViewModel() { Id = 1, Title = "Series One: Beyond the Darkness", Description = "**Markdown** *Content*" };
            vm.CurrentEpisode = new EpisodeViewModel() { Id = 1, Title = "Episode 1 - Discovery", Description = "**The crew** of Starbase Gamma begin to unravel the mysteries of the *Gamma Quadrant*.", Banner = "https://dummyimage.com/120x240/000000/fff.gif&text=Episode+One" };

            vm.CurrentEpisode.Story = await GetEpisodeStory(vm.CurrentEpisode.Id);

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
        Task<SeriesOverviewViewModel> GetOverview();
    }
}
