using AutoMapper;
using Engenharia.Application.Authorization;
using Engenharia.Domain.Auth;
using Engenharia.Domain.DTOs;
using Engenharia.Domain.Entities.Identity;
using Engenharia.Domain.Models;
using Engenharia.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Engenharia.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private RoleService<AppContext> roleService;

        public PermissionController(RoleManager<Role> roleManager, IMapper mapper)
        {
            //this.roleManager = roleManager;
            //this.userManager = userManager;
            //this.mapper = mapper;

            roleService = new RoleService<AppContext>(roleManager, mapper);
        }

        //[AllowAnonymous]
        [HasPermission(Permissions.PermissionRead)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new PermissionService().GetAll();
            return Ok(response);
        }

        //[AllowAnonymous]
        [HasPermission(Permissions.PermissionRead)]
        [HttpGet("GetByRole")]
        public async Task<IActionResult> GetByRole([FromQuery]int? roleId)
        {
            var roleClaims = await roleService.GetRoleClaims(roleId.ToString(), CustomClaimTypes.Permission);
            var response = new PermissionService().GetFromRoleClaims(roleClaims.ToList());
            return Ok(response);
        }


    }
}