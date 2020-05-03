using System;
using System.ComponentModel.DataAnnotations;

namespace Engenharia.Domain.Auth
{
    public enum Permissions
    {

        [Display(GroupName = "User", Name = "Create", Description = "Can create user")]
        UserCreate = 1,
        [Display(GroupName = "User", Name = "Read", Description = "Can read user")]
        UserRead = 2,
        [Display(GroupName = "User", Name = "Update", Description = "Can update user")]
        UserUpdate = 3,
        [Display(GroupName = "User", Name = "Delete", Description = "Can delete user")]
        UserDelete = 4,

       
        [Display(GroupName = "Role", Name = "Create", Description = "Can create role")]
        RoleCreate = 5,
        [Display(GroupName = "Role", Name = "Read", Description = "Can read role")]
        RoleRead = 6,
        [Display(GroupName = "Role", Name = "Update", Description = "Can update role")]
        RoleUpdate = 7,
        [Display(GroupName = "Role", Name = "Delete", Description = "Can delete role")]
        RoleDelete = 8,

        
        [Display(GroupName = "Permission", Name = "Read", Description = "Can read permission")]
        PermissionRead = 6,


        [Display(GroupName = "SuperAdmin", Name = "AccessAll", Description = "This allows the user to access every feature")]
        AccessAll = 9
    }

    public static class Extensions
    {
        public static string ValueToString(this Permissions permission)
        {
            return permission.ToString("D");
        }
    }


}
