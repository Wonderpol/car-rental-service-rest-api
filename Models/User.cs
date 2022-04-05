using System.ComponentModel.DataAnnotations;

namespace CarRentalRestApi.Models
{
    public enum Roles
    {
        User,
        Admin
    }
    public class User
    {
        public int Id { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [Required] 
        public Roles Role { get; set; } = Roles.User;
    }
} 