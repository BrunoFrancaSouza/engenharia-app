using AutoMapper;
using Engenharia.Domain.DTOs;
using Engenharia.Domain.Identity;
using Engenharia.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

            authService = new AuthService<AppContext>(config, userManager, signInManager, mapper);
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
            try
            {
                var user = mapper.Map<User>(userDto);
                var result = await userManager.CreateAsync(user, userDto.Password);
                var response = mapper.Map<UserDto>(user);

                if (result.Succeeded)
                    return Created("GetUser", response);

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                string strMensagemErro = "Erro de requisição no banco de dados: ";
                string strExceptionMessage = $"ExceptionMessage: { ex.Message }";
                return this.StatusCode(StatusCodes.Status500InternalServerError, strMensagemErro + Environment.NewLine + strExceptionMessage);
            }

        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {

            try
            {
                if (userLogin == null)
                    return StatusCode(StatusCodes.Status400BadRequest, "Requisição incompleta.");

                var user = await userManager.FindByEmailAsync(userLogin.Email);

                if (user == null)
                    return StatusCode(StatusCodes.Status401Unauthorized, "Email incorreto!");

                var result = await signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

                if (result.Succeeded)
                {
                    var appUser = await userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == userLogin.Email.ToUpper());
                    var response = mapper.Map<UserLoginDto>(appUser);

                    return Ok(new
                    {
                        token = GenerateJWToken(appUser).Result,
                        user = response
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Senha incorreta!");
                }

                //return Unauthorized();
            }
            catch (Exception ex)
            {
                string strMensagemErro = "Erro de requisição no banco de dados: ";
                string strExceptionMessage = $"ExceptionMessage: { ex.Message }";
                return this.StatusCode(StatusCodes.Status500InternalServerError, strMensagemErro + Environment.NewLine + strExceptionMessage);
            }
        }

        private async Task<string> GenerateJWToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
