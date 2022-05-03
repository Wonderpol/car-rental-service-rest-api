using System;
using System.Threading.Tasks;
using CarRentalRestApi.Models;
using CarRentalRestApi.Models.Auth;

namespace CarRentalRestApi.Repository
{
    public interface IRefreshTokenRepository
    {
        Task<Guid> AddToken(RefreshToken refreshToken);
        Task<bool> RemoveToken(int userId);
        Task<RefreshToken> GetTokenByToken(string token);
    }
}