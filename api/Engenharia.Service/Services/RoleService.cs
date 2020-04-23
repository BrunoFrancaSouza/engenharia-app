using AutoMapper;
using Engenharia.Domain.DTOs;
using Engenharia.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engenharia.Service.Services
{
    public class RoleService<TContext> : BaseService<Role, TContext> where TContext : DbContext
    {
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public RoleService(RoleManager<Role> roleManager, UserManager<User> userManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public new List<RoleDto> GetAll()
        {
            var roles = roleManager.Roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Active = r.Active
            })
            .ToList();

            var response = mapper.Map<List<RoleDto>>(roles);

            return response;
        }

        public async Task<RoleDto> GetById(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            var response = mapper.Map<RoleDto>(role);

            return response;
        }

        public async Task<RoleDto> GetByName(string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            var response = mapper.Map<RoleDto>(role);

            return response;
        }

        public async Task<RoleDto> Create(RoleDto roleDto)
        {
            var role = mapper.Map<Role>(roleDto);
            var result = await roleManager.CreateAsync(role);

            if (result.Succeeded)
                return mapper.Map<RoleDto>(role);

            var error = result.Errors.First();

            throw new ArgumentException($"{error.Code} - {error.Description}");
        }

        public async Task<RoleDto> Update(RoleDto roleDto)
        {
            var role = await roleManager.FindByIdAsync(roleDto.Id.ToString());

            if (role == null)
                throw new Exception("Registro não encontrado.");

            role.Name = roleDto.Name;
            role.Description = roleDto.Description;
            role.Active = roleDto.Active.HasValue ? roleDto.Active.Value : false;

            var result = await roleManager.UpdateAsync(role);

            if (result.Succeeded)
                return mapper.Map<RoleDto>(role);

            throw new Exception(result.Errors.ToString());
        }

        public new async Task<bool> Delete(int roleId)
        {
            var roleDto = await GetById(roleId.ToString());

            if (roleDto == null)
                throw new Exception("Registro não encontrado.");

            var role = mapper.Map<Role>(roleDto);
            var result = await roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return true;

            throw new Exception(result.Errors.ToString());

        }

        public async Task<bool> DeleteMany(string[] roleIds)
        {
            if (roleIds == null || roleIds.Count() == 0)
                throw new ArgumentException("Parâmetro 'roleIds' incorreto");

            foreach (var roleId in roleIds)
            {
                var role = await roleManager.FindByIdAsync(roleId);

                if (role == null)
                    throw new ArgumentException($"Role Id {roleId} não existe.");

                var result = await roleManager.DeleteAsync(role);

                if (result.Succeeded == false)
                    throw new Exception(result.Errors.ToString());
            }

            return true;

        }
    }
}
