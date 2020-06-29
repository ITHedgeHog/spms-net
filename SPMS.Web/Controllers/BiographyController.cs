using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Services;
using SPMS.Application.ViewModels;
using SPMS.Application.ViewModels.Biography;
using SPMS.Domain.Models;

namespace SPMS.Web.Controllers
{
    [Authorize(Policy = "Player")]
    public class BiographyController : Controller
    {
        private readonly ISpmsContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public BiographyController(ISpmsContext context, IMapper mapper, IUserService userService)
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
        public async Task<IActionResult> Create(CancellationToken token, [Bind("Firstname,Surname,DateOfBirth,Species,Homeworld,Gender,Born,Eyes,Hair,Height,Weight,Affiliation,Assignment,Rank,RankImage,PostingId,StatusId")] CreateBiographyViewModel biography)
        {
            var vm = new CreateBiographyViewModel
            {
                Postings = _context.Posting.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList(),
                Statuses = _context.BiographyStatus.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            };
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Biography>(biography);

                entity.PlayerId = _context.Player.First(x => x.AuthString == _userService.GetAuthId()).Id;
                await _context.Biography.AddAsync(entity, token);
                await _context.SaveChangesAsync(token);
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

            if (biography.Player.AuthString != _userService.GetAuthId()|| biography == default(EditBiographyViewModel))
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
        public async Task<IActionResult> Edit(CancellationToken token, int id, [Bind("Id,Firstname,Surname,DateOfBirth,Species,Homeworld,Gender,Born,Eyes,Hair,Height,Weight,Affiliation,Assignment,Rank,RankImage,PostingId,PlayerId,History,StatusId")] EditBiographyViewModel biography)
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
                    _context.Biography.Update(entity);
                    await _context.SaveChangesAsync(token);
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
                return RedirectToAction("Characters", "My");
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
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken token)
        {
            var biography = await _context.Biography.FindAsync(id);
            _context.Biography.Remove(biography);
            await _context.SaveChangesAsync(token);
            return RedirectToAction(nameof(Index));
        }

        private bool BiographyExists(int id)
        {
            return _context.Biography.Any(e => e.Id == id);
        }
    }
}
