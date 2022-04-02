using System.Threading.Tasks;
using CarRentalRestApi.Dtos.User;
using CarRentalRestApi.Models;

namespace CarRentalRestApi.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<UserGetDto>> GetMe(int id);
    }
}