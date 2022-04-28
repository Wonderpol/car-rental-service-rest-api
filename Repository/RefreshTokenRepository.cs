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

            var userRefreshToken = await _dataContext.RefreshTokens.FirstOrDefaultAsync(token => token.UserId.Equals(refreshToken.UserId));
            
            if (userRefreshToken != null)
            {
                userRefreshToken.Token = refreshToken.Token;
                _dataContext.RefreshTokens.Update(userRefreshToken);
                await _dataContext.SaveChangesAsync();
                return userRefreshToken.Id;
            }

            _dataContext.RefreshTokens.Add(refreshToken);
            await _dataContext.SaveChangesAsync();
            return refreshToken.Id;
        }
        
        public async Task<bool> RemoveToken(int userId)
        {
            try
            {
                var token = await _dataContext.RefreshTokens.FirstAsync(token => token.UserId.Equals(userId));
                _dataContext.RefreshTokens.Remove(token);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public async Task<RefreshToken> GetTokenByToken(string givenToken)
        {
            var refreshToken =  await _dataContext.RefreshTokens.FirstOrDefaultAsync(token => token.Token.Equals(givenToken));

            return refreshToken;
        }
    }
}