using AlexApp.Application.Dto;
using AlexApp.Application.Services.Contracts;
using AlexApp.Domain.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AlexApp.Api.Controllers
{
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("api/users")]
        public ActionResult GetRange(int page = 1, int pageSize = 50)
        {
            return Ok(_userService.GetRange(page, pageSize, null));
        }

        [HttpGet("api/users/{id}")]
        public ActionResult<UserDto> Get(int id)
        {
            UserDto user = _userService.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("api/users/current")]
        public ActionResult<UserDto> GetCurrent()
        {
            var identity = User.Identity as ClaimsIdentity;
            UserDto user = _userService.Get(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
            //var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            //var userNameClaim = identity.Claims.FirstOrDefault(c => c.Type == "username");

            //return Ok(_userService.GetByName(userNameClaim.Value));
        }

        //[HttpPost("api/users")]
        //public ActionResult<int> Create([FromBody] NewUser newUser)
        //{
        //    _userService.Create(newUser);
        //    return Ok();
        //}

        //[HttpPut("api/users/{id}")]
        //public ActionResult Update(int id, [FromBody] UserUpdate update)
        //{
        //    _userService.Update(id, update);
        //    return Ok();
        //}

        //[HttpPatch("api/users/{id}/password")]
        //public ActionResult ChangePassword(int id, PasswordUpdate update)
        //{
        //    _userService.ChangePassword(id, update);
        //    return Ok();
        //}
    }
}
