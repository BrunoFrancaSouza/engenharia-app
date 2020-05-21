using AutoMapper;
using Engenharia.Domain.DTOs;
using Engenharia.Domain.Entities.Identity;
using Engenharia.Domain.Models;
using Engenharia.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Engenharia.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalAuthController : ControllerBase
    {

        private readonly FacebookAuthSettings facebookAuthSettings;

        private readonly IConfiguration config;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;

        private ExternalAuthService<AppContext> externalAuthService;

        public ExternalAuthController(IOptions<FacebookAuthSettings> fbAuthSettingsAccessor, UserManager<User> userManager)
        {
            facebookAuthSettings = fbAuthSettingsAccessor.Value;
            this.config = config;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;

            externalAuthService = new ExternalAuthService<AppContext>(config, userManager, mapper);
        }

        // POST api/externalauth/facebook
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Facebook([FromBody] FacebookLoginDto model)
        {
            var response = await externalAuthService.FacebookLogin(facebookAuthSettings.AppId, facebookAuthSettings.AppSecret, model.AccessToken);

            if (response != null)
                return Ok(response);

            return BadRequest();

            
        }
    }
}