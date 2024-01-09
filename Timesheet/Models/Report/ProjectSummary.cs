namespace Timesheet.Models.Report
{
    public class ProjectSummary
    {
        public int id { get; set; }

        public string name { get; set; } = string.Empty;

        public int hours { get; set; }

        public ProjectSummary(int id, string name, int hours)
        {
            this.id = id;
            this.name = name;
            this.hours = hours;
        }

        public override string ToString()
        {
            return $"ProjectSummary: id={id}, name={name}, hours={hours}";
        }
    }
}