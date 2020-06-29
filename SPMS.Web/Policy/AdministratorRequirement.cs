using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Services;

namespace SPMS.Web.Policy
{
    public class AdministratorRequirement : IAuthorizationRequirement
    {
        public string AdministratorRole { get; set; }

        public AdministratorRequirement()
        {
            AdministratorRole = StaticValues.AdminRole;
        }
    }

    public class AdministratorHandler : AuthorizationHandler<AdministratorRequirement>
    {
        private readonly ISpmsContext _context;
        private readonly IUserService _userService;

        public AdministratorHandler(ISpmsContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            AdministratorRequirement requirement)
        {
            var player = _context.Player.Include(p => p.Roles).ThenInclude(roles => roles.PlayerRole)
                .FirstOrDefault(x => x.AuthString == _userService.GetAuthId());

            if (player != null)
            {
                if (player.Roles.Any(r => r.PlayerRole.Name == requirement.AdministratorRole))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }


    public class PlayerRequirement : IAuthorizationRequirement
    {
        public string PlayerRole { get; set; }

        public PlayerRequirement()
        {
            PlayerRole = StaticValues.PlayerRole;
        }
    }

    public class PlayerPolicyHandler : AuthorizationHandler<PlayerRequirement>
    {
        private readonly ISpmsContext _context;
        private readonly IUserService _userService;

        public PlayerPolicyHandler(ISpmsContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PlayerRequirement requirement)
        {
            var player = _context.Player.Include(p => p.Roles).ThenInclude(roles => roles.PlayerRole).FirstOrDefault(x => x.AuthString == _userService.GetAuthId());

            if (player != null)
            {
                if (player.Roles.Any(r => r.PlayerRole.Name == requirement.PlayerRole))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
