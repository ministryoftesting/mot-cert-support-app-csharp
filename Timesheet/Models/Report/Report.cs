namespace Timesheet.Models.Report
{
    public class Report
    {
        public int total { get; set; }

        public List<ProjectSummary> projectSummaries { get; set; } = new();

        public Report(int total, List<ProjectSummary> projectSummaries)
        {
            this.total = total;
            this.projectSummaries = projectSummaries;
        }
        public override string ToString()
        {
            return $"Report: total={total}, projectSummaries={string.Join(",", projectSummaries)}";
        }
    }
}