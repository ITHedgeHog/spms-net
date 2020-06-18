using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPMS.Web.Models;
using SPMS.Web.Service;
using SPMS.Web.ViewModels;

namespace SPMS.Web.Controllers
{
    [Authorize(Roles = "player")]
    public class MyController : Controller
    {
        private readonly SpmsContext _context;
        private readonly IUserService _userService;

        public MyController(SpmsContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var vm = new MyViewModel();

            vm.IsCreateCharacterEnabled = _userService.IsPlayer();

            var owner = _userService.GetAuthId();
            var bios = _context.Biography.Include(b => b.Player).Where(x => x.Player.AuthString == owner);
            foreach (var bio in bios)
            {
                vm.Characters.Add(bio.Id, bio.Firstname + " " + bio.Surname);
            }

            return View(vm);
        }
    }
}
