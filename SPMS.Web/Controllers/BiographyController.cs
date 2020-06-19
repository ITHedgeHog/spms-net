using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SPMS.Web.Models;
using SPMS.Web.Service;
using SPMS.Web.ViewModels;
using SPMS.Web.ViewModels.Biography;

namespace SPMS.Web.Controllers
{
    [Authorize(Roles = "player")]
    public class BiographyController : Controller
    {
        private readonly SpmsContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public BiographyController(SpmsContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        [AllowAnonymous]
        // GET: Biography
        public async Task<IActionResult> Index()
        {
            var vm = new BiographiesViewModel
            {
                Postings = await _context.Posting.Where(x => x.Name != "Undefined").OrderBy(x => x.Name).ToListAsync(),
                Biographies = await _context.Biography.ToListAsync()
            };

            return View(vm);
        }

        [AllowAnonymous]
        // GET: Biography/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var vm = new BiographyViewModel();
            if (id == null)
            {
                return NotFound();
            }

            var biography = await _context.Biography.Include(x => x.Player).Include(x=>x.Status).Include(x => x.Posting).ProjectTo<BiographyViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (biography == null)
            {
                return NotFound();
            }

            return View(biography);
        }

        public IActionResult Create()
        {
            var vm = new CreateBiographyViewModel { Postings = _context.Posting.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList(),
            
            Statuses = _context.BiographyStatus.Select(x => new SelectListItem(x.Name, x.Id.ToString()))};
            return View(vm);
        }

        // POST: Biography/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname,Surname,DateOfBirth,Species,Homeworld,Gender,Born,Eyes,Hair,Height,Weight,Affiliation,Assignment,Rank,RankImage,PostingId,StatusId")] CreateBiographyViewModel biography)
        {
            var vm = new CreateBiographyViewModel
            {
                Postings = _context.Posting.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList(),
                Statuses = _context.BiographyStatus.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            };
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Biography>(biography);

                entity.Owner = User.Claims.First(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                entity.PlayerId = _context.Player.First(x => x.AuthString == _userService.GetAuthId()).Id;
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(biography);
        }

        // GET: Biography/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //EditBiographyViewModel biography = new EditBiographyViewModel() { }; ;
                
            var biography = await _context.Biography.Include(b => b.Player).Include(b => b.Posting)
                .Include(b => b.Status)
                .Where(x => x.Id == id).ProjectTo<EditBiographyViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

            if (biography.Owner != _userService.GetAuthId()|| biography == default(EditBiographyViewModel))
            {
                return NotFound();
            }

            biography.Postings = _context.Posting.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            biography.Statuses = _context.BiographyStatus.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
      
            return View(biography);
        }

        // POST: Biography/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Firstname,Surname,DateOfBirth,Species,Homeworld,Gender,Born,Eyes,Hair,Height,Weight,Affiliation,Assignment,Rank,RankImage,PostingId,PlayerId,History,StatusId")] EditBiographyViewModel biography)
        {
            if (id != biography.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _mapper.Map<Biography>(biography);
                    _context.Update(entity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BiographyExists(biography.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "My");
            }

            biography.Postings = _context.Posting.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();

            return View(biography);
        }

        // GET: Biography/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biography = await _context.Biography
                .FirstOrDefaultAsync(m => m.Id == id);
            if (biography == null)
            {
                return NotFound();
            }

            return View(biography);
        }

        // POST: Biography/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var biography = await _context.Biography.FindAsync(id);
            _context.Biography.Remove(biography);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BiographyExists(int id)
        {
            return _context.Biography.Any(e => e.Id == id);
        }
    }
}
