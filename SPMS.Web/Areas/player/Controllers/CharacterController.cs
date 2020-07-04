using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Character.Command;
using SPMS.Application.Character.Query;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Domain.Models;
using SPMS.ViewModel.character;

namespace SPMS.Web.Areas.player.Controllers
{
    [Area("player")]
    public class CharacterController : Controller
    {
        private readonly ISpmsContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public CharacterController(ISpmsContext context, IMapper mapper, IUserService userService, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            _mediator = mediator;
        }

        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var dto = await _mediator.Send(new GetCharacterQuery() { Id = 0 }, cancellationToken);

            var vm = _mapper.Map<EditCharacterViewModel>(dto);

            return View(vm);
        }

       

        [HttpGet("player/biography/{id}/edit")]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {


            var dto = await _mediator.Send(new GetCharacterQuery() {Id = id}, cancellationToken);
            if (dto == null)
            {
                return NotFound();
            }

            var biography = _mapper.Map<EditCharacterViewModel>(dto);

            return View(biography);
        }


        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessEdit(int id, [Bind("Id,Firstname,Surname,DateOfBirth,Species,Homeworld,Gender,Born,Eyes,Hair,Height,Weight,Affiliation,Assignment,Rank,RankImage,PostingId,PlayerId,History,StatusId,StateId,PlayerId")] EditCharacterViewModel biography, CancellationToken cancellationToken)
        {
            if (id != biography.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var command = _mapper.Map<UpdateCharacterCommand>(biography);
                var result = await _mediator.Send(command, cancellationToken);

                TempData["Message"] = result switch
                {
                    UpdateCharacterResponse.Created => "Character Created",
                    UpdateCharacterResponse.Updated => "Character updated",
                    _ => TempData["Message"]
                };

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
            return RedirectToAction("Characters", "My", new {area = "player"});
        }

        private bool BiographyExists(int id)
        {
            return _context.Biography.Any(e => e.Id == id);
        }
    }
}
