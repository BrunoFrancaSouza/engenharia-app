//using Engenharia.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Engenharia.Domain.Entities.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
