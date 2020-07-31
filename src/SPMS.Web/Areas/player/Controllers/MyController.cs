using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using SPMS.Application.Authoring.Query;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Application.Dtos.Story;
using SPMS.Application.Services;
using SPMS.Common;
using SPMS.Web.Models;
using SPMS.WebShared.Infrastructure.Extensions;


namespace SPMS.Web.Controllers
{
    [Authorize(Policy = "Player")]
    [Area("player")]
    public class MyController : Controller
    {
        private readonly ISpmsContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IFeatureManager _featureManager;

        public MyController(ISpmsContext context, IUserService userService, IMapper mapper, IMediator mediator, IFeatureManager featureManager)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
            _mediator = mediator;
            _featureManager = featureManager;
        }

        [HttpGet("player/character-portal")]
        public IActionResult Characters()
        {
            var vm = new MyCharactersViewModel
            {
                IsCreateCharacterEnabled = _userService.IsPlayer(),
                HasEpisode = _context.Episode.Include(e => e.Status).Include(e => e.Series)
                    .Any(e => e.Status.Name == StaticValues.Published && e.Series.GameId == HttpContext.GetTenant().Id),
            };
            var owner = _userService.GetAuthId();
            var bios = _context.Biography.Include(b => b.Player).Where(x => x.Player.AuthString == owner && x.GameId.HasValue && x.GameId.Value == HttpContext.GetTenant().Id);

            foreach (var bio in bios)
            {
                vm.Characters.Add(bio.Id, bio.Firstname + " " + bio.Surname);
            }

            return View(vm);
        }

        /// <summary>
        /// Renders the writing portal.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet("player/writing-portal")]
        public async Task<IActionResult> Writing(CancellationToken cancellationToken)
        {
            var dto = await _mediator.Send(new WritingPortalQuery(), cancellationToken).ConfigureAwait(true);
            var vm = _mapper.Map<MyWritingViewModel>(dto);
            return View(vm);
        }
    }
}
