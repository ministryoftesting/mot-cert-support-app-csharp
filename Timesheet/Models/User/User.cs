using System.ComponentModel.DataAnnotations;

namespace Timesheet.Models.User
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        public User() { }

        public User(int id, string username, string email, string password, string role)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            Role = role;
        }

        public User(string username, string email, string password, string role)
        {
            Username = username;
            Email = email;
            Password = password;
            Role = role;
        }

        public override string ToString()
        {
            return $"User{{id={Id}, username='{Username}', email='{Email}', password='{Password}', role='{Role}'}}";
        }
    }
}
