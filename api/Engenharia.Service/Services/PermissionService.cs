using Engenharia.Domain.Auth;
using Engenharia.Domain.DTOs;
using Engenharia.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace Engenharia.Service.Services
{
    public class PermissionService
    {
        private Type enumPermissionsType = typeof(Permissions);

        public List<PermissionDto> GetAll()
        {

            var result = Enum.GetNames(enumPermissionsType)
                       .Select(permission => new
                       {
                           Id = GetValue(permission),
                           GroupName = GetGroupName(permission),
                           Name = permission,
                           Description = GetDescription(permission),
                           IsActive = PermissionIsActive(permission),
                       })
                       .Select(p => new PermissionDto
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Description = p.Description,
                           IsActive = p.IsActive
                       })
                       .OrderBy(p => p.Name)
                       .ToList();

            return result;
        }

        public List<PermissionsGroupedDto> GetAllGrouped()
        {

            var result = Enum.GetNames(enumPermissionsType)
                       .Select(permission => new
                       {
                           Id = GetValue(permission),
                           GroupName = GetGroupName(permission),
                           Name = GetPermissionName(permission),
                           Description = GetDescription(permission),
                           IsActive = PermissionIsActive(permission),
                       })
                       .GroupBy(p => p.GroupName)
                       .Select(g => new PermissionsGroupedDto
                       {
                           GroupName = g.Key,
                           Permissions = g.Select(p => new PermissionDto
                           {
                               Id = p.Id,
                               Name = p.Name,
                               Description = p.Description,
                               IsActive = p.IsActive
                           }).ToList()
                       })
                       .OrderBy(p => p.GroupName)
                       .ToList();

            return result;
        }

        public List<PermissionDto> GetFromRoleClaims(List<Claim> claims)
        {
            var permissionClaims = claims.Where(c => c.Type == CustomClaimTypes.Permission);

            if (permissionClaims == null)
                return null;

            var permissions = permissionClaims.Select(c => new
            {
                Permission = (Permissions)int.Parse(c.Value),
            }).ToList();

            var result = permissions
            .Select(p => new
            {
                Id = GetValue(p.Permission.ToString()),
                GroupName = GetGroupName(p.Permission.ToString()),
                Name = p.Permission.ToString(),
                Description = GetDescription(p.Permission.ToString()),
                IsActive = PermissionIsActive(p.Permission.ToString()),
            })
            .Select(p => new PermissionDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                IsActive = p.IsActive
            })
            .OrderBy(p => p.Name)
            .ToList();

            return result;
        }

        public bool PermissionIsActive(string permission)
        {
            var member = GetMemberInfo(permission);
            var obsolete = member.GetCustomAttribute<ObsoleteAttribute>();
            return obsolete == null ? true : false;
        }

        public bool PermissionIsActive(int value)
        {
            var permission = GetPermissionName(value);
            var member = GetMemberInfo(permission);
            var obsolete = member.GetCustomAttribute<ObsoleteAttribute>();
            return obsolete == null ? true : false;
        }

        private int GetValue(string _permission)
        {
            Permissions permission = (Permissions)Enum.Parse(enumPermissionsType, _permission);
            return (int)permission;
        }

        public string GetPermissionName(string permission)
        {
            var result = enumPermissionsType.GetMember(permission).First().GetCustomAttribute<DisplayAttribute>().Name;
            return result;
        }

        public string GetPermissionName(int value)
        {
            var result = Enum.Parse(enumPermissionsType, value.ToString()).ToString();
            return result;
        }

        public string GetDescription(string permission)
        {
            var member = GetMemberInfo(permission);
            var result = member.GetCustomAttribute<DisplayAttribute>().Description;
            return result;
        }

        public string GetDescription(int value)
        {
            var permissionName = GetPermissionName(value);
            var member = GetMemberInfo(permissionName);
            var result = member.GetCustomAttribute<DisplayAttribute>().Description;
            return result;
        }

        public string GetGroupName(string permission)
        {
            var member = GetMemberInfo(permission);
            var result = member.GetCustomAttribute<DisplayAttribute>().GroupName;
            return result;
        }

        public string GetPermissionGroupName(int value)
        {
            var permissionName = GetPermissionName(value);
            var member = GetMemberInfo(permissionName);
            var result = member.GetCustomAttribute<DisplayAttribute>().GroupName;
            return result;
        }

        private MemberInfo GetMemberInfo(string permission)
        {
            var result = enumPermissionsType.GetMember(permission).First();
            return result;
        }

    }
}
