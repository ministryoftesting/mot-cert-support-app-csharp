using Timesheet.Models.Report;
using Timesheet.Models.Project;
using Timesheet.DB;

namespace Timesheet.Service 
{

    public class ReportService 
    {

        private IProjectDB projectDB;

        public ReportService() 
        {
            projectDB = new ProjectDB();
        }

        public ReportService(IProjectDB projectDB) 
        {
            this.projectDB = projectDB;
        }

        public Report GetReport() 
        {
            int timesheetTotal = 0;
            List<Project> projects = projectDB.GetProjects();
            List<ProjectSummary> projectDetails = new List<ProjectSummary>();

            foreach(Project project in projects) {
                List<Entry> entries = projectDB.GetProject(project.Id).entries;
                int projectTotal = 0;

                foreach(Entry entry in entries) {
                    timesheetTotal += entry.Hours;
                    projectTotal += entry.Hours;
                }
                
                projectDetails.Add(new ProjectSummary(project.Id, project.Name, projectTotal));
            }

            return new Report(timesheetTotal, projectDetails);
        }

    }

}