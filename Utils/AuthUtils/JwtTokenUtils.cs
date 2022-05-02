using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CarRentalRestApi.Models;
using CarRentalRestApi.Models.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CarRentalRestApi.Utils.AuthUtils
{
    public class JwtTokenUtils: IJwtTokenUtils
    {
        private readonly AuthConfig _authConfig;

        public JwtTokenUtils(AuthConfig authConfig)
        {
            _authConfig = authConfig;
        }

        public string GenerateToken(DateTime? expires, string tokenSecret, IEnumerable<Claim> claims = null)
        {
            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(tokenSecret)
            );

            var loginCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = loginCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
        public string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            
            return GenerateToken(DateTime.Now.AddDays(7), _authConfig.AccessTokenSecret, claims);
        }

        public string GenerateRefreshToken()
        {
            return GenerateToken(DateTime.Now.AddMonths(1), _authConfig.RefreshTokenSecret);
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    System.Text.Encoding.ASCII.GetBytes(_authConfig.RefreshTokenSecret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken securityToken);
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return false;
            }
        }
    }
}