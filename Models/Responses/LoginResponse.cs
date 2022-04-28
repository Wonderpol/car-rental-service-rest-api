using CarRentalRestApi.Dtos.User;

namespace CarRentalRestApi.Models.Responses
{
    public class LoginResponse: ServiceResponse<UserGetDto>
    {
        public string Access { get; set; }
        public string Refresh { get; set; }

        public bool isAdmin { get; set; }
    }
}