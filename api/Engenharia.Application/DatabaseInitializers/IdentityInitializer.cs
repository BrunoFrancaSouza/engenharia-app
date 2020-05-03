using Engenharia.Domain.Auth;
using Engenharia.Domain.Entities.Identity;
using Engenharia.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Engenharia.Application.Database_Initializers
{
    public class IdentityInitializer
    {

        private const string superAdminRoleName = "SuperAdmin";

        public static void SeedData(UserManager<User> userManager, RoleManager<Role> roleManager, string adminEmail, string adminPassword)
        {
            SeedSuperAdminRole(roleManager);
            SeedSuperAdminUser(userManager, adminEmail, adminPassword);
        }

        private static void SeedSuperAdminRole(RoleManager<Role> roleManager)
        {

            if (roleManager.RoleExistsAsync(superAdminRoleName).Result)
                return;

            var superAdminRole = new Role
            {
                Name = superAdminRoleName,
                Description = "Role para total controle do sistema.",
                Active = true
            };

            var result = roleManager.CreateAsync(superAdminRole).Result;

            if (result.Succeeded)
                roleManager.AddClaimAsync(superAdminRole, new Claim(CustomClaimTypes.Permission, Permissions.AccessAll.ValueToString())).Wait();
        }

        private static void SeedSuperAdminUser(UserManager<User> userManager, string adminEmail, string adminPassword)
        {

            if (userManager.FindByEmailAsync(adminEmail).Result != null)
                return;

            var adminUser = new User
            {
                UserName = "Admin",
                FullName = "Super Admin",
                Email = adminEmail
            };

            var result = userManager.CreateAsync(adminUser, adminPassword).Result;

            if (result.Succeeded)
                userManager.AddToRoleAsync(adminUser, superAdminRoleName).Wait();

        }

    }

}
