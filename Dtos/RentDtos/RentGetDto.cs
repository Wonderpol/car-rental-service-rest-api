using CarRentalRestApi.Dtos.User;
using CarRentalRestApi.Models.VehicleModels;

namespace CarRentalRestApi.Dtos.RentDtos
{
    public class RentGetDto
    {
        public int Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public UserGetDto User { get; set; }
        public long StartRentTimestamp { get; set; }
        public long EndRentTimestamp { get; set; }
    }
}