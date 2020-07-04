using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Common;
using SPMS.Domain.Models;

namespace SPMS.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Policy = "Administrator")]
    public class PlayerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISpmsContext _context;

        public PlayerController(ISpmsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Admin/Player
        public async Task<IActionResult> Index()
        {
            var vm = new PlayerListViewModel
            {
                Players = await _context.Player.Include(x => x.Roles).ThenInclude(roles => roles.PlayerRole).ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
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
        public async Task<IActionResult> Create([Bind("Id,DisplayName,AuthString")] Player player, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                await _context.Player.AddAsync(player, token);
                await _context.SaveChangesAsync(token);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,DisplayName,AuthString")] Player player, CancellationToken token)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Player.Update(player);
                    await _context.SaveChangesAsync(token);
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
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken token)
        {
            var player = await _context.Player.FindAsync(id);
            _context.Player.Remove(player);
            await _context.SaveChangesAsync(token);
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Player.Any(e => e.Id == id);
        }

        [HttpPost("admin/user/switch/player/role")]
        public async Task<IActionResult> SwitchPlayerRole(int id, CancellationToken token)
        {
            var role = await _context.PlayerRole.FirstAsync(x => x.Name == StaticValues.PlayerRole, cancellationToken: token);

            var player = await _context.Player.Include(x => x.Roles).FirstAsync(x => x.Id == id, cancellationToken: token);

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

            await _context.SaveChangesAsync(token);
            return RedirectToAction("Index");
        }

        [HttpPost("admin/user/switch/admin/role")]
        public async Task<IActionResult> SwitchAdminRole(int id, CancellationToken token)
        {
            var role = await _context.PlayerRole.FirstAsync(x => x.Name == StaticValues.AdminRole, cancellationToken: token);

            var player = await _context.Player.Include(x => x.Roles).FirstAsync(x => x.Id == id, cancellationToken: token);

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

            await _context.SaveChangesAsync(token);
            return RedirectToAction("Index");
        }
    }
}
