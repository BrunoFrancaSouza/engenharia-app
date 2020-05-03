using Engenharia.Application.Authorization;
using Engenharia.Domain.Auth;
using Engenharia.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Engenharia.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        [AllowAnonymous]
        //[HasPermission(Permissions.PermissionRead)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new PermissionService().GetAll();
            return Ok(response);
        }

        //[HasPermission(Permissions.PermissionRead)]
        //[HttpGet]
        //public IActionResult GetActives()
        //{
        //    var response = new PermissionService().GetActives();
        //    return Ok(response);
        //}
    }
}