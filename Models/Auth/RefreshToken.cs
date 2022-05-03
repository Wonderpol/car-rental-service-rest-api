using System;

namespace CarRentalRestApi.Models.Auth
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}