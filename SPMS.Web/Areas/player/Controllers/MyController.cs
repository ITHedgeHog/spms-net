using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Services;
using SPMS.Application.ViewModels;
using SPMS.Application.ViewModels.Story;
using SPMS.Common;
using SPMS.Web.Models;
using SPMS.Web.Service;

namespace SPMS.Web.Controllers
{
    [Authorize(Policy = "Player")]
    [Area("player")]
    public class MyController : Controller
    {
        private readonly ISpmsContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public MyController(ISpmsContext context, IUserService userService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("player/character-portal")]
        public IActionResult Characters()
        {
            var vm = new MyCharactersViewModel
            {
                IsCreateCharacterEnabled = _userService.IsPlayer(),
                HasEpisode = _context.Episode.Include(e => e.Status).Any(e => e.Status.Name == StaticValues.Published)
            };


            var owner = _userService.GetAuthId();
            var bios = _context.Biography.Include(b => b.Player).Where(x => x.Player.AuthString == owner);
            foreach (var bio in bios)
            {
                vm.Characters.Add(bio.Id, bio.Firstname + " " + bio.Surname);
            }

            return View(vm);
        }

        [HttpGet("player/writing-portal")]
        public IActionResult Writing()
        {
            var vm = new MyWritingViewModel
            {
                IsCreateCharacterEnabled = _userService.IsPlayer(),
                HasEpisode = _context.Episode.Include(e => e.Status).Any(e => e.Status.Name == StaticValues.Published)
            };

            var owner = _userService.GetAuthId();
            vm.DraftPosts = _context.EpisodeEntry
                .Include(e => e.EpisodeEntryType)
                .Include(e => e.Episode)
                .Include(p => p.EpisodeEntryPlayer).ThenInclude(p=>p.Player)
                .Where(e => e.EpisodeEntryType.Name == StaticValues.Post && e.EpisodeEntryStatus.Name == StaticValues.Draft)
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
