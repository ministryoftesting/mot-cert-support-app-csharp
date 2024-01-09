using Microsoft.AspNetCore.Mvc;
using Timesheet.Models.Report;
using Timesheet.Service;

namespace Timesheet.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class reportController : ControllerBase
    {

        ReportService _reportService = new ReportService();
        AuthService _authService = new AuthService();

        [HttpGet]
        public ActionResult GetReport([FromHeader(Name = "Authorization")] string token)
        {
            if(_authService.CheckSession(token, DateTime.Now))
            {
                Report report = _reportService.GetReport();
                
                return StatusCode(200, report);
            }
            else
            {
                return StatusCode(401);
            }
        }

    }
}
