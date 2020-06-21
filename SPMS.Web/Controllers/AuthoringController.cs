using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPMS.Web.Models;
using SPMS.Web.Service;
using SPMS.Web.ViewModels.Authoring;

namespace SPMS.Web.Controllers
{
    [Authorize(Roles = StaticValues.PlayerRole)]
    public class AuthoringController : Controller
    {
        private readonly SpmsContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AuthoringController(SpmsContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }



        [HttpGet("author/post/{id?}")]
        public IActionResult Post(int? id)
        {
            AuthorPostViewModel vm;
            if (id.HasValue)
            {
                vm = _context.EpisodeEntry.ProjectTo<AuthorPostViewModel>(_mapper.ConfigurationProvider).FirstOrDefault(x => x.Id == id);
                if (vm == default(AuthorPostViewModel))
                {
                    return NotFound("Could not find post");
                }
                vm.Statuses = _context.EpisodeEntryStatus.Select(x => new SelectListItem(x.Name, x.Id.ToString(), x.Name == StaticValues.Draft));
                vm.TypeId = _context.EpisodeEntryType.First(x => x.Name == StaticValues.Post).Id;
                vm.PostTypes =
                    _context.EpisodeEntryType.Select(x => new SelectListItem(x.Name, x.Id.ToString(), x.Id == vm.TypeId)).ToList();

                return View("Post", vm);
            }

            // TODO: Find active episode 
            var activeEpisode = _context.Episode.Include(e => e.Status).FirstOrDefault(e => e.Status.Name == StaticValues.Active);

            if (activeEpisode == default(Episode))
                return RedirectToAction("Writing", "My");

            vm = new AuthorPostViewModel(activeEpisode.Id);
            vm.Authors.Add(_userService.GetId());
            vm.Statuses = _context.EpisodeEntryStatus.Select(x => new SelectListItem(x.Name, x.Id.ToString(), x.Name == StaticValues.Draft));
            vm.TypeId = _context.EpisodeEntryType.First(x => x.Name == StaticValues.Post).Id;
            vm.PostTypes =
                _context.EpisodeEntryType.Select(x => new SelectListItem(x.Name, x.Id.ToString(), x.Id == vm.TypeId)).ToList();


            return View(vm);
        }


        [HttpPost("author/post")]
        public IActionResult ProcessPostData(AuthorPostViewModel model)
        {
            if (!ModelState.IsValid) return View("Post", model);

            if (_context.EpisodeEntry.Any(x => x.Id == model.Id))
            {

                var post = _context.EpisodeEntry.FirstOrDefault(e => e.Id == model.Id);

                //TODO: Update Model
                var b = 2;
            }
            else
            {
                var entity = _mapper.Map<EpisodeEntry>(model);
                _context.EpisodeEntry.Add(entity);
                _context.SaveChanges();
                model.Id = entity.Id;
            }

            TempData["Message"] = "Yay it saved";
            return RedirectToAction("Writing", "My");

        }

        [HttpPost("author/post/autosave")]
        public IActionResult ProcessAutoSave(AuthorPostViewModel model)
        {
            if (string.IsNullOrEmpty(model.Title))
            {
                //ModelState.MarkFieldValid("Title");
                model.Title = "Title";
            }
            if (string.IsNullOrEmpty(model.Content))
            {
                //ModelState.MarkFieldValid("Content");
                model.Content = "Content";
            }

            ModelState.Clear();
            TryValidateModel(model);

            if (ModelState.IsValid)
            {
                if (_context.EpisodeEntry.Any(x => x.Id == model.Id))
                {

                    var post = _context.EpisodeEntry.FirstOrDefault(e => e.Id == model.Id);

                    //TODO: Update Model
                    var b = 2;
                }
                else
                {
                    var entity = _mapper.Map<EpisodeEntry>(model);
                    var pId = _userService.GetId();

                    if (entity.EpisodeEntryTypeId == 0)
                    {
                        entity.EpisodeEntryTypeId =
                            _context.EpisodeEntryType.First(x => x.Name == StaticValues.Post).Id;
                    }
                    _context.EpisodeEntry.Add(entity);
                    _context.SaveChanges();
                    entity.EpisodeEntryPlayer = new Collection<EpisodeEntryPlayer> { new EpisodeEntryPlayer() { EpisodeEntryId = entity.Id, PlayerId = pId } };
                    _context.SaveChanges();

                    model.Id = entity.Id;
                }
            }
            return Ok(model.Id);
        }

      

        // GET: Authoring/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodeEntry = await _context.EpisodeEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episodeEntry == null)
            {
                return NotFound();
            }

            return View(episodeEntry);
        }

        // POST: Authoring/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var episodeEntry = await _context.EpisodeEntry.FindAsync(id);
            _context.EpisodeEntry.Remove(episodeEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction("Writing", "My");
        }

        private bool EpisodeEntryExists(int id)
        {
            return _context.EpisodeEntry.Any(e => e.Id == id);
        }
    }
}
