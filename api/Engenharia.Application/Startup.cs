﻿using AutoMapper;
using Engenharia.Application.Authorization;
using Engenharia.Application.Database_Initializers;
using Engenharia.Application.Extensions;
using Engenharia.Domain.Entities.Identity;
using Engenharia.Infra.Data.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace Engenharia.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private string MySqlUrl;
        private string MySqlPort;
        private string MySqlDatabase;
        private string MySqlUser;
        private string MySqlPassword;

        private string AdminEmail;
        private string AdminPassword;

        private string JwtSecret;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            LoadEnvironmentVariables();

            var applicationDbConnectionString = GetApplicationDbConnectionString();
            var logDbConnectionString = GetApplicationDbConnectionString();

            services.AddDbContext<AppContext>(x => x.UseMySql(applicationDbConnectionString));
            services.AddDbContext<LogContext>(x => x.UseMySql(logDbConnectionString));

            IdentityBuilder builder = services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<AppContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();

           
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>  // Configuração JWT
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

            })
            .AddGoogle(options =>
            {
                options.ClientId = Configuration["GOOGLE_AUTH_CLIENT_ID"];
                options.ClientSecret = Configuration["GOOGLE_AUTH_CLIENT_SECRET"];
            })
            .AddFacebook(options =>
            {
                options.AppId = Configuration["FACEBOOK_AUTH_APP_ID"];
                options.AppSecret = Configuration["FACEBOOK_AUTH_APP_SECRET"];
            })
            ;

            services.AddAutoMapper();
            services.AddCors();

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

                options.Filters.Add(new AuthorizeFilter(policy)); // Toda vez que alguém chamar uma rota, será requerido que o usuário esteja autenticado
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;  // https://pt.stackoverflow.com/questions/364558/web-api-n%C3%A3o-retorna-dados-relacionados
            });

            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            //Register the Permission policy handlers
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Initialize minimum configs for database (roles, SuperAdmin user, etc ... ) case not exists
            IdentityInitializer.SeedData(userManager, roleManager, AdminEmail, AdminPassword);

            app.ConfigureCustomExceptionMiddleware();

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseMvc();
        }

        private void LoadEnvironmentVariables()
        {
            MySqlUrl = Configuration["MYSQL_URL"];
            MySqlPort = Configuration["MYSQL_PORT"];
            MySqlDatabase = Configuration["MYSQL_DATABASE"];
            MySqlUser = Configuration["MYSQL_USER"];
            MySqlPassword = Configuration["MYSQL_PASSWORD"];
            //MySqlPassword = Configuration["MYSQL_ROOT_PASSWORD"];

            AdminEmail = Configuration["ADMIN_EMAIL"];
            AdminPassword = Configuration["ADMIN_PASSWORD"];

            JwtSecret = Configuration["JWT_SECRET"];
        }

        public string GetApplicationDbConnectionString()
        {
            return GetMySqlConnectionString(MySqlUrl, MySqlPort, MySqlDatabase, MySqlUser, MySqlPassword);
        }

        private string GetLogDbConnectionString()
        {
            return GetMySqlConnectionString(MySqlUrl, MySqlPort, MySqlDatabase, MySqlUser, MySqlPassword);
        }

        private string GetMySqlConnectionString(string server, string port, string dataBase, string user, string password)
        {
            var result = $"Server={server};port={port};DataBase={dataBase};Uid={user};Pwd={password}";
            return result;
        }
    }
}
