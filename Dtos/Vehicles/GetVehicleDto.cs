using CarRentalRestApi.Models;

namespace CarRentalRestApi.Dtos.Vehicles
{
    public class GetVehicleDto
    {
        //Base
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
        //Car 
        public int? NumberOfSeats { get; set; } = null;
        public int? Hp { get; set; } = null;
        public double? Acceleration { get; set; } = null;
        public string TransmissionType { get; set; }
        public string ChassisType { get; set; }
        //Caravan
        public double? Space { get; set; } = null;
        public bool? IsBathroomInside { get; set; } = null;
        public int? NumberOfAllowedPeople { get; set; } = null;
        public double? TotalLenght { get; set; } = null;
        public double? Height { get; set; } = null;
        public double? Width { get; set; } = null;
        public int? NumberOfAxis { get; set; } = null;
    }
}