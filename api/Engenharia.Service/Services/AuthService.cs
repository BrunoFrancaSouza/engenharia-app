using AutoMapper;
using Engenharia.Domain.DTOs;
using Engenharia.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Engenharia.Service.Services
{
    public class AuthService<TContext> : BaseService<User, TContext> where TContext : DbContext
    {
        private readonly IConfiguration config;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;

        public AuthService(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            this.config = config;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public async Task Register(UserDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            var result = await userManager.CreateAsync(user, userDto.Password);
            var response = mapper.Map<UserDto>(user);

            if (!result.Succeeded)
                throw new Exception(result.Errors.ToString());

        }


    }
}
