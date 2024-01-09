using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Timesheet.Models.User;
using Timesheet.Service;

namespace Timesheet.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        UserService _userService = new UserService();
        AuthService _authService = new AuthService();

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user, [FromHeader(Name = "Authorization")] string token)
        {
            if(_authService.CheckSession(token, DateTime.Now)){
                (int code, User createdUser) = _userService.CreateUser(user);
                return StatusCode(code, createdUser);
            } else {
                return StatusCode(401);
            }
        }

        [HttpDelete]
        [Route("{userid:int}")]
        public IActionResult DeleteUser(int userId, [FromHeader(Name = "Authorization")] string token)
        {
            if(_authService.CheckSession(token, DateTime.Now)){
                int code = _userService.DeleteUser(userId);
                
                return StatusCode(code);
            } else {
                return StatusCode(401);
            }
        }

        [HttpGet]
        [Route("{userid:int}")]
        public IActionResult GetUserProfile(int userId, [FromHeader(Name = "Authorization")] string token)
        {
            if(_authService.CheckSession(token, DateTime.Now)){
                (int code, User userProfile) = _userService.GetUserProfile(userId);
                return StatusCode(code, userProfile);
            } else {
                return StatusCode(401);
            }
        }

        [HttpPut]
        [Route("{userid:int}")]
        public IActionResult UpdateUserProfile(int userId, [FromBody] User user, [FromHeader(Name = "Authorization")] string token)
        {
            if(_authService.CheckSession(token, DateTime.Now)){
                int code = _userService.UpdateUser(userId, user);
                return StatusCode(code);
            } else {
                return StatusCode(401);
            }
        }

        [HttpGet]
        public ActionResult<string> GetUsers([FromHeader(Name = "Authorization")] string token)
        {
            if(_authService.CheckSession(token, DateTime.Now)){
                (int code, List<User> userList) = _userService.GetUsers();
                return StatusCode(code, userList);
            } else {
                return StatusCode(401);
            }
        }
    }
}
