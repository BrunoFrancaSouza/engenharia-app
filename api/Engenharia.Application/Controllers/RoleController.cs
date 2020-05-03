using AutoMapper;
using Engenharia.Application.Authorization;
using Engenharia.Domain.Auth;
using Engenharia.Domain.DTOs;
using Engenharia.Domain.Entities.Identity;
//using Engenharia.Domain.Identity;
using Engenharia.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Engenharia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        //private readonly RoleManager<Role> roleManager;
        //private readonly UserManager<User> userManager;
        //private readonly IMapper mapper;

        private RoleService<AppContext> roleService;

        public RoleController(RoleManager<Role> roleManager, UserManager<User> userManager, IMapper mapper)
        {
            //this.roleManager = roleManager;
            //this.userManager = userManager;
            //this.mapper = mapper;

            roleService = new RoleService<AppContext>(roleManager, userManager, mapper);
        }

        //[AllowAnonymous]
        [HasPermission(Permissions.RoleRead)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await roleService.GetAll();
            return Ok(response);
        }

        //[AllowAnonymous]
        [HasPermission(Permissions.RoleRead)]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await roleService.GetById(id);
            return Ok(response);
        }

        //[AllowAnonymous]
        [HasPermission(Permissions.RoleRead)]
        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string roleName)
        {
            var response = await roleService.GetByName(roleName);
            return Ok(response);
        }

        //[AllowAnonymous]
        //[Authorize(Permissions.RoleCreate.ValueToString())]
        [HasPermission(Permissions.RoleCreate)]
        [HttpPost]
        public async Task<IActionResult> Create(RoleDto roleDto)
        {
            var response = await roleService.Create(roleDto);
            return Created("GetRole", response);
        }

        //[AllowAnonymous]
        [HasPermission(Permissions.RoleUpdate)]
        [HttpPut]
        public async Task<IActionResult> Update(RoleDto roleDto)
        {
            var response = await roleService.Update(roleDto);
            return Created("GetRole", response);
        }

        //[AllowAnonymous]
        [HasPermission(Permissions.RoleDelete)]
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> Delete(int roleId)
        {
            var response = await roleService.Delete(roleId);
            return Ok();
        }

        //[AllowAnonymous]
        [HasPermission(Permissions.RoleDelete)]
        [HttpDelete("DeleteMany")]
        public async Task<IActionResult> DeleteMany([FromQuery] string[] roleIds)
        {
            var response = await roleService.DeleteMany(roleIds);
            return Ok(response);
        }

        //[AllowAnonymous]
        [HasPermission(Permissions.RoleUpdate)]
        [HttpPost("UpdatePermissions")]
        public async Task<IActionResult> UpdatePermissions(RoleUpdatePermissionsDto roleUpdatePermissionsDto)
        {
            var response = await roleService.UpdatePermissions(roleUpdatePermissionsDto);
            return Ok(response);
        }

    }
}
