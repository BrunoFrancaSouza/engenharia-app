using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Engenharia.Domain.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public List<UserRole> UserRoles{ get; set; }
    }
}
