using AutoMapper;
using Engenharia.Domain.DTOs;
using Engenharia.Domain.Entities.Identity;
using Engenharia.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Engenharia.Service.Services
{
    public class ExternalAuthService<TContext> : BaseService<User, TContext> where TContext : DbContext
    {
        private readonly IConfiguration config;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        //private readonly FacebookAuthSettings facebookAuthSettings;
        private static readonly HttpClient client = new HttpClient();

        //public ExternalAuthService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        public ExternalAuthService(IConfiguration config, UserManager<User> userManager, IMapper mapper)
        {
            this.config = config;
            this.userManager = userManager;
            //this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public async Task<LoginResponseDto> FacebookLogin(string appId, string appSecret, string accessToken)
        {
            // 1.generate an app access token
            var appAccessTokenResponse = await client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={appId}&client_secret={appSecret}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);

            // 2. validate the user access token
            var userAccessTokenValidationResponse = await client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={accessToken}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
                //return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid facebook token.", ModelState));
                throw new ArgumentException("Invalid facebook token.");

            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={accessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                user = new User
                {
                    //FirstName = userInfo.FirstName,
                    //LastName = userInfo.LastName,
                    //FacebookId = userInfo.Id,
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    //PictureUrl = userInfo.Picture.Data.Url
                };

                var result = await userManager.CreateAsync(user, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));

                if (!result.Succeeded)
                    throw new Exception(result.Errors.ToString());
            }

            // generate the jwt for the local user...
            user = await userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
                throw new Exception("Failed to create local user account.");

            var userDto = mapper.Map<UserDto>(user);
            var roles = await userManager.GetRolesAsync(user);
            var jwtService = new JwtService();
            var token = await jwtService.GenerateToken(user, roles, config["JWT_SECRET"]);
            var response = new LoginResponseDto
            {
                token = token,
                user = userDto
            };

            return response;
        }
    }
}
