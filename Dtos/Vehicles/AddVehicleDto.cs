using CarRentalRestApi.Models;

namespace CarRentalRestApi.Dtos.Vehicles
{
    public class AddVehicleDto
    {
        public string Brand { get; set; } = "Bmw";
        public int Year { get; set; } = 2020;
        public int HorsePower { get; set; } = 200;
        public long Millage { get; set; } = 20000;
        public Type TypeOfVehicle { get; set; } = Type.Car;
    }
}