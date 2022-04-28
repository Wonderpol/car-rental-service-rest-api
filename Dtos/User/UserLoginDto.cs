using System.ComponentModel.DataAnnotations;

namespace CarRentalRestApi.Dtos.User
{
    public class UserLoginDto
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
        
    }
}