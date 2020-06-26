using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPMS.Web.Models;
using SPMS.Web.ViewModels.Authoring;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SPMS.Web.Areas.player.ViewModels;

namespace SPMS.Web.Service
{
    public class AuthoringService : IAuthoringService
    {
        private readonly SpmsContext _context;
        private readonly IUserService _userService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public AuthoringService(SpmsContext context, IMapper mapper, IGameService gameService, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _gameService = gameService;
            _userService = userService;
        }

        public async Task<AuthorPostViewModel> GetPost(int id)
        {
            var vm = new AuthorPostViewModel();
            vm = await _context.EpisodeEntry.ProjectTo<AuthorPostViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);

            vm.Statuses = await _context.EpisodeEntryStatus.Select(x => new SelectListItem(x.Name, x.Id.ToString(), x.Name == StaticValues.Draft)).ToListAsync();
            vm.TypeId = _context.EpisodeEntryType.First(x => x.Name == StaticValues.Post).Id;
            vm.PostTypes =
                await _context.EpisodeEntryType.Select(x => new SelectListItem(x.Name, x.Id.ToString(), x.Id == vm.TypeId)).ToListAsync();


            if (vm.Authors.All(x => x.Name != _userService.GetName()))
            {
                vm.Authors.Add(new AuthorViewModel(_userService.GetId(), _userService.GetName(), await _userService.GetEmailAsync()));
            }

            return vm;
        }

        public async Task<bool> HasActiveEpisodeAsync()
        {
            var gameId = await _gameService.GetGameIdAsync();
            var exists = await _context.Episode.Include(e => e.Status).Include(e => e.Series).ThenInclude(s => s.Game).AnyAsync(x => x.Status.Name == StaticValues.Active && x.Series.Game.Id == gameId);
            return exists;
        }

        public async Task<AuthorPostViewModel> NewPost()
        {
            var vm = new AuthorPostViewModel();
            // TODO: Find active episode 
            var activeEpisodeId = await  _context.Episode.Include(e => e.Status).CountAsync(e => e.Status.Name == StaticValues.Active || e.Status.Name == StaticValues.Archived);

            vm = new AuthorPostViewModel(activeEpisodeId);

            //vm.Authors.Add(_userService.GetId());
            vm.Statuses = await _context.EpisodeEntryStatus.Select(x => new SelectListItem(x.Name, x.Id.ToString(), x.Name == StaticValues.Draft)).ToListAsync();
            vm.StatusId = int.Parse(vm.Statuses.First(x => x.Text == StaticValues.Draft).Value);
            vm.TypeId = _context.EpisodeEntryType.First(x => x.Name == StaticValues.Post).Id;
            vm.PostTypes =
                await _context.EpisodeEntryType.Select(x => new SelectListItem(x.Name, x.Id.ToString(), x.Id == vm.TypeId)).ToListAsync();

            if (vm.Authors.All(x => x.Name != _userService.GetName()))
            {
                vm.Authors.Add(new AuthorViewModel(_userService.GetId(), _userService.GetName(), await _userService.GetEmailAsync()));
            }

            return vm;

        }

        public async Task<bool> PostExists(int id)
        {
            var gameId = await _gameService.GetGameIdAsync();
            return await _context.EpisodeEntry.Include(e => e.Episode).ThenInclude(e => e.Series).ThenInclude(s => s.Game).AnyAsync( x => x.Id == id && x.Episode.Series.Game.Id == gameId);
        }

        private void HandleSubmit(EpisodeEntry entity, AuthorPostViewModel model)
        {
            var submitValue = model.submitpost;
            if (string.IsNullOrEmpty(submitValue))
                return;

            if(submitValue.Equals("publish"))
            {
                entity.PublishedAt = DateTime.UtcNow;
                entity.EpisodeEntryStatusId = _context.EpisodeEntryStatus.First(x => x.Name == StaticValues.Active).Id;
            }

            if (submitValue.Equals("schedule"))
            {
                entity.PublishedAt = model.PostAt;
                entity.EpisodeEntryStatusId = _context.EpisodeEntryStatus.First(x => x.Name == StaticValues.Pending).Id;
            }
        }

        public async Task<int> SavePostAsync(AuthorPostViewModel model)
        {
            if (_context.EpisodeEntry.Any(x => x.Id == model.Id))
            {
                var entity = await _context.EpisodeEntry.Include(e => e.EpisodeEntryPlayer).Include(e => e.EpisodeEntryStatus).Include(e => e.EpisodeEntryType).FirstOrDefaultAsync(e => e.Id == model.Id);
                _mapper.Map(model, entity);
                entity.UpdatedAt = DateTime.UtcNow;
                HandleSubmit(entity, model);
                await _context.SaveChangesAsync();
            }
            else
            {
               
                var pId = _userService.GetId();
                if (model.Authors.All(x => x.Id != pId))
                    model.Authors.Add(new AuthorViewModel(pId, _userService.GetName(), await _userService.GetEmailAsync()));

                var entity = _mapper.Map<EpisodeEntry>(model);
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                HandleSubmit(entity, model);

                if (entity.EpisodeEntryTypeId == 0)
                {
                    entity.EpisodeEntryTypeId =
                        _context.EpisodeEntryType.First(x => x.Name == StaticValues.Post).Id;
                }
                

                await _context.EpisodeEntry.AddAsync(entity);
                await _context.SaveChangesAsync();
               

                model.Id = entity.Id;
            }

            return model.Id;
        }

        public async Task<List<AuthorToInviteViewModel>> GetAuthorsAsync(int id)
        {
            var post = await _context.EpisodeEntry.Include(p => p.EpisodeEntryPlayer).FirstAsync(x => x.Id == id);

            var authors = await _context.Player.Include(x => x.EpisodeEntries).ProjectTo<AuthorToInviteViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            foreach (var a in authors.Where(a => post.EpisodeEntryPlayer.Any(x => x.PlayerId == a.Id)))
            {
                a.IsSelected = true;
            }


            return authors;
        }

        public async Task UpdateAuthors(InviteAuthorViewModel model)
        {
            var post = await _context.EpisodeEntry.Include(x => x.EpisodeEntryPlayer).FirstAsync(x => x.Id == model.Id);

            post.EpisodeEntryPlayer.Clear();
            foreach (var author in model.Authors.Where(x => x.IsSelected))
            {
                post.EpisodeEntryPlayer.Add(new EpisodeEntryPlayer() {EpisodeEntryId = post.Id, PlayerId = author.Id});

            }

            await _context.SaveChangesAsync();
        }
    }

    public interface IAuthoringService
    {
        Task<bool> HasActiveEpisodeAsync();
        Task<bool> PostExists(int id);
        Task<AuthorPostViewModel> NewPost();
        Task<AuthorPostViewModel> GetPost(int id);
        Task<int> SavePostAsync(AuthorPostViewModel model);
        Task<List<AuthorToInviteViewModel>> GetAuthorsAsync(int id);
        Task UpdateAuthors(InviteAuthorViewModel model);
    }
}
