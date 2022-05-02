using CarRentalRestApi.Models.VehicleModels;

namespace CarRentalRestApi.Dtos.RentDtos
{
    public class RentGetDto
    {
        public int Id { get; set; }
        public Vehicle Type { get; set; }
        public Models.Auth.User User { get; set; }
        public long StartRentTimestamp { get; set; }
        public long EndRentTimestamp { get; set; }
    }
}