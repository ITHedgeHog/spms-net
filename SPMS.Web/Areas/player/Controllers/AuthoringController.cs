using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;
using SPMS.Application.Authoring.Command.CreatePost;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Services;
using SPMS.Application.ViewModels;
using SPMS.Application.ViewModels.Authoring;
using SPMS.Common;
using SPMS.Persistence;
using SPMS.Web.Areas.player.ViewModels;
using SPMS.Web.Models;
using SPMS.Web.Service;

namespace SPMS.Web.Controllers
{
    [Authorize(Policy = "Player")]
    [Area("player")]
    public class AuthoringController : Controller
    {
        private readonly ISpmsContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IAuthoringService _authoringService;
        private readonly IMediator _mediator;

        public AuthoringController(ISpmsContext context, IMapper mapper, IUserService userService, IAuthoringService authoringService, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            _authoringService = authoringService;
            _mediator = mediator;
        }


        [HttpPost("player/author/post/new")]
        public async Task<IActionResult> NewPost(CancellationToken cancellationToken)
        {
            if (!(await _authoringService.HasActiveEpisodeAsync()))
            {
                TempData["message"] = "No active episode";
                return RedirectToAction("Writing", "My");
            }

            var newPost = await _mediator.Send(new CreatePost(), cancellationToken);

            return RedirectToAction("Post", new { Id = newPost });
        }

        [HttpGet("player/author/post/{id}/invite")]
        public async Task<IActionResult> Invite(int id, CancellationToken cancellationToken)
        {

            if (!await _authoringService.PostExists(id))
            {
                TempData["message"] = "Post does not exist";
                return RedirectToAction("Writing", "My");
            }

            var vm = new InviteAuthorViewModel();
            //var post = _authoringService.GetPost(id);
            vm.Authors = await _authoringService.GetAuthorsAsync(id, cancellationToken);

            return View(vm);
        }

        [HttpPost("player/author/post/invite/process")]
        public async Task<IActionResult> ProcessInvite(InviteAuthorViewModel model, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                //TODO: Need to notify users of the JP. This is where we use mediatr

                await _authoringService.UpdateAuthors(model, token);

                if (model.nextaction.Equals("save"))
                {
                    return RedirectToAction("Writing", "My");
                }

                return RedirectToAction("Post", new {id = model.Id});
            }

            return View("Invite", model);
        }

        [HttpGet("player/author/post/{id}")]
        public async Task<IActionResult> Post(int? id, CancellationToken cancellationToken)
        {
            if (!(await _authoringService.HasActiveEpisodeAsync()))
            {
                TempData["message"] = "No active episode";
                return RedirectToAction("Writing", "My");
            }

            if (id.HasValue && !(await _authoringService.PostExists(id.Value)))
            {
                TempData["message"] = "Post does not exist";
                return RedirectToAction("Writing", "My");
            }

            if (id.HasValue)
            {


                return View(await _authoringService.GetPost(id.Value, cancellationToken));
            }


            return View(await _authoringService.NewPost(cancellationToken));
        }


        [HttpPost("player/author/post")]
        public async Task<IActionResult> ProcessPostData(AuthorPostViewModel model, CancellationToken token)
        {
            if (!ModelState.IsValid) return View("Post", model);

            var id = await _authoringService.SavePostAsync(model, token);

            TempData["Message"] = "Yay it saved";
            return RedirectToAction("Writing", "My");

        }

        [HttpPost("player/author/post/autosave")]
        public async Task<IActionResult> ProcessAutoSave(AuthorPostViewModel model, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                model.Id = await _authoringService.SavePostAsync(model, token);
            }
            return Ok(model.Id);
        }


        [HttpGet("player/authoring/{id}/delete")]
        // GET: Authoring/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var episodeEntry = await _context.EpisodeEntry.Include(x => x.EpisodeEntryStatus)
                .FirstOrDefaultAsync(m => m.Id == id && m.EpisodeEntryStatus.Name == StaticValues.Draft);
            if (episodeEntry == null)
            {
                return NotFound();
            }

            ViewData["Id"] = episodeEntry.Id;
            ViewData["PostTitle"] = episodeEntry.Title;
            ViewData["PostContent"] = episodeEntry.Content;
            return View(new ViewModel());
        }

        // POST: Authoring/Delete/5
        [HttpPost("player/authoring/{id}/delete/confirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            var episodeEntry = await _context.EpisodeEntry.FindAsync(id);
            _context.EpisodeEntry.Remove(episodeEntry);
            await _context.SaveChangesAsync(cancellationToken);
            return RedirectToAction("Writing", "My");
        }
    }
}
