using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarRentalRestApi.Models.Auth;
using CarRentalRestApi.Models.VehicleModels;

namespace CarRentalRestApi.Models.RentVehicleModels
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public long StartRentTimestamp { get; set; }
        public long EndRentTimestamp { get; set; }
    }
}