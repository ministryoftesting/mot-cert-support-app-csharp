using Microsoft.AspNetCore.Mvc;
using Timesheet.Models.Auth;
using Timesheet.Service;

namespace Timesheet.Controllers
{

    [Route("v1/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {

        private AuthService _authService = new AuthService();

        [HttpPost]
        [Route("validate")]
        public ActionResult<string> ValidateToken([FromBody] Token tokenPayload)
        {
            bool result = _authService.CheckSession(tokenPayload.token, DateTime.Now);
            if(result)
                return StatusCode(200);
            else
                return StatusCode(401);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<Credentials> CheckLogin([FromBody] Login loginPayload)
        {
            (int code, Credentials credentials) = _authService.Login(loginPayload.email, loginPayload.password);
            return StatusCode(code, credentials);
        }

        [HttpPost]
        [Route("logout")]
        public ActionResult<string> Logout([FromBody] Token tokenPayload)
        {
            int code = _authService.Logout(tokenPayload.token);
            return StatusCode(code);
        }
    }
}