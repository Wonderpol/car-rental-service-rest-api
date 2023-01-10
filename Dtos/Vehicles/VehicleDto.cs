using CarRentalRestApi.Models;
using CarRentalRestApi.Models.Auth;
using CarRentalRestApi.Models.VehicleModels;

namespace CarRentalRestApi.Dtos.Vehicles
{
    public class VehicleDto
    {
        //Base vehicle
        public int Id { get; set; }
        public Brand Brand { get; set; }
        public int Year { get; set; }
        public int HorsePower { get; set; }
        public long Millage { get; set; }
        public long VinNumber { get; set; }
        public Model Model { get; set; }
        public double PricePerDay { get; set; }
        public string RegistrationPlate { get; set; }
        public Type TypeOfVehicle { get; set; }
        public TransmissionType TransmissionType { get; set; }

    }
}