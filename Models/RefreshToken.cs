using System;

namespace CarRentalRestApi.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}