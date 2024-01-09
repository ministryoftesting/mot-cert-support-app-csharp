using System.ComponentModel.DataAnnotations;

namespace Timesheet.Models.Project
{
    public class Entry
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Hours { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public Entry()
        {
            // Default constructor
        }

        public Entry(int id, DateTime date, int hours, string description)
        {
            Id = id;
            Date = date;
            Hours = hours;
            Description = description;
        }

        public Entry(DateTime date, int hours, string description)
        {
            Date = date;
            Hours = hours;
            Description = description;
        }

        public override string ToString()
        {
            return $"Entry{{id={Id}, date={Date}, hours={Hours}, description='{Description}'}}";
        }
    }
}
