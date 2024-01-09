using Newtonsoft.Json;

namespace Timesheet.Models.Project
{
    public class ProjectDetails
    {
        public string name { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public int total { get; set; } = 0;

        public List<Entry> entries { get; set; } = new List<Entry>();

        public ProjectDetails(string name, string description, int total, List<Entry> entries)
        {
            this.name = name;
            this.description = description;
            this.total = total;
            this.entries = entries;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}