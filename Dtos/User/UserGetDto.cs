using System;

namespace CarRentalRestApi.Dtos.User
{
    public class UserGetDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}