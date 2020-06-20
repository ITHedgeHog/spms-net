using System;
using System.Collections.Generic;
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



        [HttpGet("author/post/data")]
        public IActionResult Post()
        { 
            // TODO: Find active episode 
            var activeEpisode = _context.Episode.Include(e => e.Status).FirstOrDefault(e => e.Status.Name == StaticValues.Active);

            if (activeEpisode == default(Episode))
                return RedirectToAction("Writing", "My");

            var vm = new AuthorPostViewModel(activeEpisode.Id);
            vm.Authors.Add(_userService.GetId());


            return View(vm);
        }
        [HttpGet("author/post/{id}/data")]
        public IActionResult PostDataExists(int id)
        {
            var vm = _context.EpisodeEntry.ProjectTo<AuthorPostViewModel>(_mapper.ConfigurationProvider).FirstOrDefault(x => x.Id == id);
            if (vm == default(AuthorPostViewModel))
            {
                return NotFound("Could not find post");
            }

            return View("Post", vm);
        }

        [HttpPost("author/post/data")]
        public IActionResult ProcessPostData(AuthorPostViewModel model)
        {
            if (!ModelState.IsValid) return View("Post", model);
            TempData["Message"] = "Yay it saved";
            return RedirectToAction("Writing", "My");

        }

        // GET: Authoring/Create
        public IActionResult Create()
        {
            // TODO: Find active episode 
            var activeEpisode = _context.Episode.Include(e => e.Status).FirstOrDefault(e => e.Status.Name == "Active");

            if (activeEpisode == default(Episode))
                return RedirectToAction("Index", "My");

            var vm = new CreateEpisodeEntryViewModel(activeEpisode.Id);


            return View(vm);
        }

        // POST: Authoring/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,CreatedAt,Content")] EpisodeEntry episodeEntry)
        {
            if (!ModelState.IsValid) return View(episodeEntry);
            _context.Add(episodeEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction("Writing", "My");
        }

        // GET: Authoring/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodeEntry = await _context.EpisodeEntry.FindAsync(id);
            if (episodeEntry == null)
            {
                return NotFound();
            }
            return View(episodeEntry);
        }

        // POST: Authoring/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CreatedAt,Content")] EpisodeEntry episodeEntry)
        {
            if (id != episodeEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(episodeEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpisodeEntryExists(episodeEntry.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Writing", "My");
            }
            return View(episodeEntry);
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
            return RedirectToAction(nameof(Index));
        }

        private bool EpisodeEntryExists(int id)
        {
            return _context.EpisodeEntry.Any(e => e.Id == id);
        }
    }
}
