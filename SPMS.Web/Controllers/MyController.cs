using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPMS.Web.Models;
using SPMS.Web.Service;
using SPMS.Web.ViewModels;

namespace SPMS.Web.Controllers
{
    [Authorize(Policy = "Player")]
    public class MyController : Controller
    {
        private readonly SpmsContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public MyController(SpmsContext context, IUserService userService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Characters()
        {
            var vm = new MyCharactersViewModel
            {
                IsCreateCharacterEnabled = _userService.IsPlayer(),
                HasEpisode = _context.Episode.Include(e => e.Status).Any(e => e.Status.Name == StaticValues.Active)
            };


            var owner = _userService.GetAuthId();
            var bios = _context.Biography.Include(b => b.Player).Where(x => x.Player.AuthString == owner);
            foreach (var bio in bios)
            {
                vm.Characters.Add(bio.Id, bio.Firstname + " " + bio.Surname);
            }

            return View(vm);
        }

        public IActionResult Writing()
        {
            var vm = new MyWritingViewModel
            {
                IsCreateCharacterEnabled = _userService.IsPlayer(),
                HasEpisode = _context.Episode.Include(e => e.Status).Any(e => e.Status.Name == StaticValues.Active)
            };

            var owner = _userService.GetAuthId();

            var posts = _context.EpisodeEntry
                .Include(e => e.EpisodeEntryType)
                .Include(e => e.Episode)
                .Include(p => p.EpisodeEntryPlayer).ThenInclude(p => p.Player)
                .ToList();
            vm.DraftPosts = _context.EpisodeEntry
                .Include(e => e.EpisodeEntryType)
                .Include(e => e.Episode)
                .Include(p => p.EpisodeEntryPlayer).ThenInclude(p=>p.Player)
                .Where(e => e.EpisodeEntryType.Name == StaticValues.Post )
                .ProjectTo<PostViewModel>(_mapper.ConfigurationProvider).ToList();
            vm.PendingPosts = _context.EpisodeEntry
                .Include(e => e.EpisodeEntryType)
                .Include(e => e.Episode)
                .Where(e => e.EpisodeEntryType.Name == StaticValues.Post && e.EpisodeEntryStatus.Name == StaticValues.Pending)
                .ProjectTo<PostViewModel>(_mapper.ConfigurationProvider).ToList();
            var bios = _context.Biography.Include(b => b.Player).Where(x => x.Player.AuthString == owner);
            foreach (var bio in bios)
            {
                vm.Characters.Add(bio.Id, bio.Firstname + " " + bio.Surname);
            }
            return View(vm);
        }
    }
}
