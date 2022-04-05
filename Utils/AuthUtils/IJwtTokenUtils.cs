using System;
using System.Collections.Generic;
using System.Security.Claims;
using CarRentalRestApi.Models;

namespace CarRentalRestApi.Utils.AuthUtils
{
    public interface IJwtTokenUtils
    {
        public string GenerateToken(DateTime? expires, string tokenSecret, IEnumerable<Claim> claims = null);
        public string GenerateAccessToken(User user);
        public string GenerateRefreshToken();
        public bool ValidateRefreshToken(string refreshToken);
    }
}