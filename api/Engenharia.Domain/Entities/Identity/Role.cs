using Engenharia.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engenharia.Domain.Identity
{
    //public class Role : IdentityRole<int>, IBaseEntity
    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public List<UserRole> UserRoles { get; set; }
        
    }
}
