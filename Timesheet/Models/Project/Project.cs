using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Timesheet.Models.Project
{
    [Table("Project")]
    public class Project
    {
        
        [Key]
        [Column("projectid")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name must be set")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description must be set")]
        public string Description { get; set; } = string.Empty;

        public Project()
        {
            
        }

        public Project(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Project(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        
    }
}
