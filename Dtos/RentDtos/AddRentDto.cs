using CarRentalRestApi.Models.VehicleModels;

namespace CarRentalRestApi.Dtos.RentDtos
{
    public class AddRentDto
    {
        public int VehicleId { get; set; }
        public long StartRentTimestamp { get; set; }
        public long EndRentTimestamp { get; set; }
    }
}