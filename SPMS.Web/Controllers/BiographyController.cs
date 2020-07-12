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
using SPMS.Application.Services;
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

       
    }
}
