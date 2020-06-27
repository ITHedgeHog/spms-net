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
using SPMS.Web.Areas.Admin.ViewModels;
using SPMS.Web.Models;
using SPMS.Web.ViewModels.Biography;

namespace SPMS.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Policy = "Administrator")]
    public class PlayerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly SpmsContext _context;

        public PlayerController(SpmsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Admin/Player
        public async Task<IActionResult> Index()
        {
            var vm = new PlayerListViewModel
            {
                Players = await _context.Player.Include(x => x.Roles).ThenInclude(roles => roles.PlayerRole).ProjectTo<PlayerViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync()
            };
            return View(vm);
        }

        // GET: Admin/Player/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Admin/Player/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Player/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DisplayName,AuthString")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Admin/Player/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // POST: Admin/Player/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DisplayName,AuthString")] Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Admin/Player/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Admin/Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Player.FindAsync(id);
            _context.Player.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Player.Any(e => e.Id == id);
        }

        [HttpPost("admin/user/switch/player/role")]
        public async Task<IActionResult> SwitchPlayerRole(int id)
        {
            var role = await _context.PlayerRole.FirstAsync(x => x.Name == StaticValues.PlayerRole);

            var player = await _context.Player.Include(x => x.Roles).FirstAsync(x => x.Id == id);

            if (player.Roles.All(x => x.PlayerRoleId != role.Id))
            {
                TempData["message"] = "Role added";
                player.Roles.Add(new PlayerRolePlayer(){PlayerId = player.Id, PlayerRoleId = role.Id});
            }
            else
            {
                TempData["message"] = "Role removed";
                player.Roles.Remove(player.Roles.First(x => x.PlayerRoleId == role.Id));
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost("admin/user/switch/admin/role")]
        public async Task<IActionResult> SwitchAdminRole(int id)
        {
            var role = await _context.PlayerRole.FirstAsync(x => x.Name == StaticValues.AdminRole);

            var player = await _context.Player.Include(x => x.Roles).FirstAsync(x => x.Id == id);

            if (player.Roles.All(x => x.PlayerRoleId != role.Id))
            {
                TempData["message"] = "Role added";
                player.Roles.Add(new PlayerRolePlayer() { PlayerId = player.Id, PlayerRoleId = role.Id });
            }
            else
            {
                TempData["message"] = "Role removed";
                player.Roles.Remove(player.Roles.First(x => x.PlayerRoleId == role.Id));
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
