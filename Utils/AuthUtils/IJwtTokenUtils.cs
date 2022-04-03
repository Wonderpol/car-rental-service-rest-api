using System;
using System.Collections.Generic;
using System.Security.Claims;
using CarRentalRestApi.Models;

namespace CarRentalRestApi.Utils.AuthUtils
{
    public interface IJwtTokenUtils
    {
        public string GenerateToken(DateTime? expires, IEnumerable<Claim> claims = null);
        public string GenerateAccessToken(User user);
    }
}