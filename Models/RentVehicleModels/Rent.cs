using CarRentalRestApi.Models.Auth;
using CarRentalRestApi.Models.VehicleModels;

namespace CarRentalRestApi.Models.RentVehicleModels
{
    public class Rent
    {
        public int Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public User User { get; set; }
        public long StartRentTimestamp { get; set; }
        public long EndRentTimestamp { get; set; }
    }
}