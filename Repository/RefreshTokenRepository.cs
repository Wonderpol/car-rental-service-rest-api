using System;
using System.Threading.Tasks;
using CarRentalRestApi.Data;
using CarRentalRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalRestApi.Repository
{
    public class RefreshTokenRepository: IRefreshTokenRepository
    {
        private readonly DataContext _dataContext;

        public RefreshTokenRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Guid> AddToken(RefreshToken refreshToken)
        {
            _dataContext.RefreshTokens.Add(refreshToken);
            await _dataContext.SaveChangesAsync();
            return refreshToken.Id;
        }

        public async Task<bool> RemoveToken(Guid uuid)
        {
            try
            {
                var token = await _dataContext.RefreshTokens.FirstAsync(token => token.Id.Equals(uuid));
                _dataContext.RefreshTokens.Remove(token);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}