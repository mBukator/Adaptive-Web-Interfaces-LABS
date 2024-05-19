using System.ComponentModel.DataAnnotations;

namespace LR7.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(15)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(15)]
        public string LastName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; } = string.Empty;

        public DateTime LastLoginDate { get; set; }

        public int FailedLoginAttempts { get; set; }
    }
}
