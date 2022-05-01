using CarRentalRestApi.Models;

namespace CarRentalRestApi.Dtos.Vehicles
{
    public class VehicleDto
    {
        //Base vehicle
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