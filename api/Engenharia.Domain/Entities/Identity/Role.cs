using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;

namespace Engenharia.Domain.Entities.Identity
{
    //public class Role : IdentityRole<int>, IBaseEntity
    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
        public IEnumerable<IdentityRoleClaim<int>> RoleClaims { get; set; }

    }
}
