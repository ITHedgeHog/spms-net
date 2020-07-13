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
using SPMS.Application.Dtos;
using SPMS.Domain.Models;
using SPMS.ViewModel;

namespace SPMS.Web.Controllers
{
    [Authorize(Policy = "Player")]
    public class BiographyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BiographyController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [AllowAnonymous]
        // GET: Biography
        public async Task<IActionResult> Index()
        {
            var dto = await _mediator.Send(new BiographyListQuery() {Url = HttpContext.Request.Host.Host});

            var vm = _mapper.Map<BiographyListViewModel>(dto);

            return View(vm);
        }

        [AllowAnonymous]
        // GET: Biography/Details/5
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                BiographyDto dto = await _mediator.Send(new GetBiographyQuery() {Id = id});

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
