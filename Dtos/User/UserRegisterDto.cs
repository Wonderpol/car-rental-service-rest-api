using System.ComponentModel.DataAnnotations;

namespace CarRentalRestApi.Dtos.User
{
    public class UserRegisterDto
    { 
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }
    }
}