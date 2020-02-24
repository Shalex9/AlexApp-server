using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AlexApp.Application;
using AlexApp.Application.Services;
using AlexApp.Application.Services.Contracts;
using AlexApp.Data;
using AlexApp.Data.Repositories;
using AlexApp.Domain.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace AlexApp.Api
{
    public class Startup
    {
        public IConfiguration _config { get; }
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _config = configuration;
            _env = env;

        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddDbContext<AlexAppDbContext>(options => options.UseSqlServer(_config["ConnectionStrings"]));

            const string jwtSchemeName = "JwtBearer";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = jwtSchemeName;
                options.DefaultChallengeScheme = jwtSchemeName;
            })
                .AddJwtBearer(jwtSchemeName, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenSignKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(5)
                    };
                });

            services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(typeof(ApplicationMappingProfile));
                cfg.AddProfile(typeof(DataMappingProfile));
            });

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (!_env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseFileServer();

            app.Use(async (context, next) =>
            {
                if (context.Request.Method != HttpMethods.Get
                    || context.Request.Path.Value.StartsWith("/api/"))
                {
                    await next.Invoke();
                }
                else
                {
                    if (_env.WebRootPath != null)
                    {
                        string indexHtmlPath = "dist/index.html";
                        var fullPath = Path.Combine(_env.WebRootPath, indexHtmlPath);
                        if (System.IO.File.Exists(fullPath))
                        {
                            context.Response.ContentType = "text/html";
                            await context.Response.SendFileAsync(fullPath);
                            return;
                        }
                    }

                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }
            });

            app.UseAuthentication();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
