using System;
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
using SPMS.Application.Biography.Query;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Domain.Models;
using SPMS.ViewModel;

namespace SPMS.Web.Controllers
{
    [Authorize(Policy = "Player")]
    public class BiographyController : Controller
    {
        //TODO: Remove
        private readonly ISpmsContext _context;
        private readonly IMapper _mapper;
        //TODO: Remove
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        private readonly IIdentifierMask _masker;

        public BiographyController(ISpmsContext context, IMapper mapper, IUserService userService, IMediator mediator, IIdentifierMask masker)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            _mediator = mediator;
            _masker = masker;
        }

        [AllowAnonymous]
        // GET: Biography
        public async Task<IActionResult> Index()
        {
            var bio = await _context.Biography.Include(x => x.State).Include(x => x.Status).ToListAsync();
            var bioDto = await _context.Biography.Include(x => x.State).Include(x => x.Status)
                .ProjectTo<Application.Dtos.BiographyDto>(_mapper.ConfigurationProvider).ToListAsync();
            var vm = new BiographiesDto
            {
                Postings = await _context.Posting.Where(x => x.Name != "Undefined").OrderBy(x => x.Name).ToListAsync(),
                Biographies = await _context.Biography.Include(x => x.State).Include(x => x.Status).ProjectTo<Application.Dtos.BiographyDto>(_mapper.ConfigurationProvider).ToListAsync()
            };

            return View(vm);
        }

        [AllowAnonymous]
        // GET: Biography/Details/5
        public async Task<IActionResult> Details(string id)
        {

            try
            {
                int intId = _masker.RevealId(id);

                BiographyDto dto = await _mediator.Send(new GetBiographyQuery() {Id = intId});

                var biography = _mapper.Map<BiographyViewModel>(dto);

                return View(biography);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        public IActionResult Create()
        {
            var vm = new CreateBiographyViewModel
            {
                Postings = _context.Posting.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList(),

                Statuses = _context.BiographyState.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            };
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
                Statuses = _context.BiographyState.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
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

        [HttpGet("player/biography/{id}/edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //EditBiographyViewModel biography = new EditBiographyViewModel() { }; ;

            var biography = await _context.Biography.Include(b => b.Player).Include(b => b.Posting)
                .Include(b => b.State)
                .Where(x => x.Id == id).ProjectTo<EditBiographyViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

            if (biography.Player.AuthString != _userService.GetAuthId() || biography == default(EditBiographyViewModel))
            {
                return NotFound();
            }

            biography.Postings = _context.Posting.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            biography.Statuses = _context.BiographyState.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();

            return View(biography);
        }

        // POST: Biography/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessEdit(CancellationToken token, int id, [Bind("Id,Firstname,Surname,DateOfBirth,Species,Homeworld,Gender,Born,Eyes,Hair,Height,Weight,Affiliation,Assignment,Rank,RankImage,PostingId,PlayerId,History,StatusId")] EditBiographyViewModel biography)
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
                return RedirectToAction("Characters", "My", new { area = "player" });
            }

            biography.Postings = _context.Posting.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();

            return View("Edit", biography);
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
