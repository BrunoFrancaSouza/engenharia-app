using Engenharia.Domain.Enums;
using System.Collections.Generic;

namespace Engenharia.Domain.DTOs
{
    public class RoleUpdatePermissionsDto
    {
        public RoleDto Role { get; set; }
        //public List<PermissionDto> Permissions { get; set; }
        public List<int> PermissionIds { get; set; }
        public UpdateTypes UpdateType { get; set; }
    }

    
}
