using System;
using System.Collections.Generic;
using System.Text;

namespace Engenharia.Domain.DTOs
{
    public class PermissionsGroupedDto
    {
        public string GroupName { get; set; }
        public IEnumerable<PermissionDto> Permissions { get; set; }
    }
}
