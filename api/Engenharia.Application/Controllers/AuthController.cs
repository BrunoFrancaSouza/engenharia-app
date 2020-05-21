using AutoMapper;
using Engenharia.Domain.DTOs;
using Engenharia.Domain.Entities.Identity;
using Engenharia.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Engenharia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;

        private AuthService<AppContext> authService;

        public AuthController(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            this.config = config;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;

            //authService = new AuthService<AppContext>(config, userManager, signInManager, mapper);
            authService = new AuthService<AppContext>(config, userManager, mapper);
        }

        //[AllowAnonymous]
        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            return Ok(new UserDto());
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            var result = await userManager.CreateAsync(user, userDto.Password);
            var response = mapper.Map<UserDto>(user);

            if (result.Succeeded)
                return Created("GetUser", response);

            return BadRequest(result.Errors);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {

            if (loginRequest == null)
                return StatusCode(StatusCodes.Status400BadRequest, $"Parâmetro '{nameof(loginRequest)}' incorreto.");

            var user = await userManager.FindByEmailAsync(loginRequest.Email);

            if (user == null)
                return StatusCode(StatusCodes.Status401Unauthorized, "Email incorreto!");

            var result = await signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);

            if (result.Succeeded)
            {
                var userDto = mapper.Map<UserDto>(user);
                var roles = await userManager.GetRolesAsync(user);
                //var token = authService.GenerateJWToken(user).Result;
                var token = await new JwtService().GenerateToken(user, roles, config["JWT_SECRET"]);
                var response = new LoginResponseDto
                {
                    token = token,
                    user = userDto
                };

                return Ok(response);
            }

            return StatusCode(StatusCodes.Status401Unauthorized, "Senha incorreta!");
        }
    }
}
