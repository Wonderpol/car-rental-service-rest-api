using System;
using System.Threading.Tasks;
using CarRentalRestApi.Models;

namespace CarRentalRestApi.Repository
{
    public interface IRefreshTokenRepository
    {
        Task<Guid> AddToken(RefreshToken refreshToken);
        Task<bool> RemoveToken(Guid uuid);
    }
}