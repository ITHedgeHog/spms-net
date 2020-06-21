using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SPMS.Web.Models;
using SPMS.Web.Service;

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
        private readonly SpmsContext _context;
        private readonly IUserService _userService;

        public AdministratorHandler(SpmsContext context, IUserService userService)
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
        private readonly SpmsContext _context;
        private readonly IUserService _userService;

        public PlayerPolicyHandler(SpmsContext context, IUserService userService)
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
