using System.ComponentModel.DataAnnotations;
using CarRentalRestApi.Models.Auth;

namespace CarRentalRestApi.Models.VehicleModels
{
    public abstract class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public int HorsePower { get; set; }
        public long Millage { get; set; }
        public long VinNumber { get; set; }
        public string Model { get; set; }
        public double PricePerHour { get; set; }
        public string RegistrationPlate { get; set; }
        public Type TypeOfVehicle { get; set; }
    }
}