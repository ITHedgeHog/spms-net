using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPMS.Web.Models;
using SPMS.Web.ViewModels;

namespace SPMS.Web.Controllers
{
    public class MyController : Controller
    {
        private readonly SpmsContext _context;

        public MyController(SpmsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var vm = new MyViewModel();

            vm.IsCreateCharacterEnabled = User.IsInRole("player");

            var owner = User.Claims.First(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            var bios = _context.Biography.Include(b => b.Player).Where(x => x.Player.AuthString == owner);
            foreach (var bio in bios)
            {
                vm.Characters.Add(bio.Id, bio.Firstname + " " + bio.Surname);
            }

            return View(vm);
        }
    }
}
