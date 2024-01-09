using Timesheet.DB;
using Timesheet.Models.Project;
using Timesheet.Models;

namespace Timesheet.Service
{
    public class ProjectService
    {
        private readonly IProjectDB _projectDB;

        public ProjectService(IProjectDB projectDB)
        {
            _projectDB = projectDB;
        }

        public ProjectService()
        {
            _projectDB = new ProjectDB();
        }

        public (int code, CreatedID id) CreateProject(Project project, string token)
        {
            int projectCreated = _projectDB.CreateProject(project);

            if(projectCreated > 0) {
                CreatedID createdID = new CreatedID(projectCreated);

                return (201, createdID);
            } else {
                return (500, null);
            }
        }

        // HERE TO UPDATE:

        public int DeleteProject(int projectId) {
            ProjectDetails project = _projectDB.GetProject(projectId);

            if(project != null){
                foreach(Entry entry in project.entries){
                    _projectDB.DeleteEntry(projectId, entry.Id);
                }

                _projectDB.DeleteProject(projectId);

                return 202;
            } else {
                return 404;
            }
        }

        public int DeleteEntry(int projectId, int entryId) {
            bool entryDeleted = _projectDB.DeleteEntry(projectId, entryId);

            if(entryDeleted) {
                return 202;
            } else {
                return 404;
            }
        }

        public (int code, ProjectDetails projectDetails) GetProject(int projectId) {
            ProjectDetails project = _projectDB.GetProject(projectId);

            if(project != null) {
                return (200, project);
            } else {
                return (404, null);
            }
        }

        public (int code, List<Project> projects) GetProjects() {
            List<Project> projects = _projectDB.GetProjects();

            return (200, projects);
        }

        public (int code, CreatedID id) CreateEntry(int projectId, Entry entry) {
            int entryCreated = _projectDB.StoreEntry(projectId, entry);

            if(entryCreated > 0) {
                CreatedID createdID = new CreatedID(entryCreated);

                return (201, createdID);
            } else {
                return (500, null);
            }
        }
    }
}