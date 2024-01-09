using Microsoft.AspNetCore.Mvc;
using Timesheet.Models;
using Timesheet.Models.Project;
using Timesheet.Service;

namespace Timesheet.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class projectController : ControllerBase
    {

        ProjectService _projectService = new ProjectService();
        AuthService _authService = new AuthService();

        [HttpPost]
        public IActionResult CreateProject([FromBody] Project project, [FromHeader(Name = "Authorization")] string token)
        {
            if(_authService.CheckSession(token, DateTime.Now)){
                (int code, CreatedID createdID) = _projectService.CreateProject(project, token);

                return StatusCode(code, createdID);
            } else {
                return StatusCode(401);
            }
        }

        [HttpDelete]
        [Route("{projectid:int}/entry/{entryid:int}")]
        public IActionResult DeleteEntry(int projectid, int entryid, [FromHeader(Name = "Authorization")] string token)
        {
            if(_authService.CheckSession(token, DateTime.Now)){
                int code = _projectService.DeleteEntry(projectid, entryid);

                return StatusCode(code);
            } else {
                return StatusCode(401);
            }
        }

        [HttpDelete]
        [Route("{projectid:int}")]
        public IActionResult DeleteProject(int projectid, [FromHeader(Name = "Authorization")] string token)
        {
            if(_authService.CheckSession(token, DateTime.Now)){
                int code = _projectService.DeleteProject(projectid);

                return StatusCode(code);
            } else {
                return StatusCode(401);
            }
        }

        [HttpGet]
        [Route("{projectid:int}")]
        public IActionResult GetProject(int projectId, [FromHeader(Name = "Authorization")] string token)
        {
            if(_authService.CheckSession(token, DateTime.Now)){
                (int code, ProjectDetails project) = _projectService.GetProject(projectId);

                return StatusCode(code, project);
            } else {
                return StatusCode(401);
            }
        }

        [HttpGet]
        public IActionResult GetProjects([FromHeader(Name = "Authorization")] string token)
        {
            if(_authService.CheckSession(token, DateTime.Now)){
                (int code, List<Project> projects) = _projectService.GetProjects();

                return StatusCode(code, projects);
            } else {
                return StatusCode(401);
            }
        }

        [HttpPost]
        [Route("{projectid:int}/entry")]
        public IActionResult CreateEntry(int projectId, [FromBody] Entry entry, [FromHeader(Name = "Authorization")] string token)
        {
            if(_authService.CheckSession(token, DateTime.Now)){
                (int code, CreatedID createdID) = _projectService.CreateEntry(projectId, entry);

                return StatusCode(code, createdID);
            } else {
                return StatusCode(401);
            }
        }
    }
}
