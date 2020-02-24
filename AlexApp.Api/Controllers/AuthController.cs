using AlexApp.Application.Services.Contracts;
using AlexApp.Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AlexApp.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private IConfiguration _config;
        private IUserService _userService;

        public AuthController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost("/api/token")]
        public IActionResult Token([FromBody]TokenRequest tokenRequest)
        {
            if (!_userService.CheckUser(tokenRequest.Username, tokenRequest.Password))
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var claims = new Claim[]{
                new Claim(ClaimTypes.NameIdentifier, tokenRequest.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenSignKey"]));
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(jwtToken);
        }
    }

    public class TokenRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
