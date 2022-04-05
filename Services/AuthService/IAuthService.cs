using System.Threading.Tasks;
using CarRentalRestApi.Dtos.User;
using CarRentalRestApi.Models;
using CarRentalRestApi.Models.Responses;

namespace CarRentalRestApi.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<LoginResponse> Login(string email, string password);

        Task<ServiceResponse<bool>> Logout(int id);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<UserGetDto>> GetMe(int id);

        Task<User> GetUserById(int id);
        Task<LoginResponse> RefreshToken(string token);
    }
}