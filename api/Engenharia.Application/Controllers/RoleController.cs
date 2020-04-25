using AutoMapper;
using Engenharia.Domain.DTOs;
using Engenharia.Domain.Identity;
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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
                var response = roleService.GetAll();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await roleService.GetById(id);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string roleName)
        {
            var response = await roleService.GetByName(roleName);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(RoleDto roleDto)
        {
            var response = await roleService.Create(roleDto);
            return Created("GetRole", response);
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> Update(RoleDto roleDto)
        {
            var response = await roleService.Update(roleDto);
            return Created("GetRole", response);
        }

        [AllowAnonymous]
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> Delete(int roleId)
        {
            var response = await roleService.Delete(roleId);
            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete("DeleteMany")]
        public async Task<IActionResult> DeleteMany([FromQuery] string[] roleIds)
        {
            var response = await roleService.DeleteMany(roleIds);
            return Ok();
        }

    }
}
