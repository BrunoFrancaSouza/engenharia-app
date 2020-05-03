using Engenharia.Domain.Auth;
using Engenharia.Domain.Entities.Identity;
using Engenharia.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Engenharia.Application.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        UserManager<User> userManager;
        RoleManager<Role> roleManager;

        public PermissionHandler(UserManager<User> _userManager, RoleManager<Role> _roleManager)
        {
            userManager = _userManager;
            roleManager = _roleManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
                return;

            // If user does not have the scope claim, get out of here
            //var permissionsClaim = context.User.Claims.SingleOrDefault(c => c.Type == CustomClaimType.Permission);
            //if (permissionsClaim == null)
            //    return;

            // Get all the roles the user belongs to and check if any of the roles has the permission required
            // for the authorization to succeed.
            var user = await userManager.GetUserAsync(context.User);
            var userRoleNames = await userManager.GetRolesAsync(user);
            var userRoles = roleManager.Roles.Where(x => userRoleNames.Contains(x.Name));

            foreach (var role in userRoles)
            {
                var roleClaims = await roleManager.GetClaimsAsync(role);
                var permissions = roleClaims.Where(x => (x.Type == CustomClaimTypes.Permission &&
                                                        x.Value == requirement.Permission &&
                                                        x.Issuer == "LOCAL AUTHORITY") ||
                                                        x.Value == Permissions.AccessAll.ValueToString())
                                            .Select(x => x.Value);

                if (permissions.Any())
                {
                    context.Succeed(requirement);
                    return;
                }
            }
        }
    }
}
