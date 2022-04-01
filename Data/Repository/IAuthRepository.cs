using System.Threading.Tasks;
using CarRentalRestApi.Models;
using CarRentalRestApi.Models.User;

namespace CarRentalRestApi.Data.Repository
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}