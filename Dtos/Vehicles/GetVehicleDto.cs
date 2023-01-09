using CarRentalRestApi.Models;
using CarRentalRestApi.Models.VehicleModels;

namespace CarRentalRestApi.Dtos.Vehicles
{
    public class GetVehcileDto: VehicleDto
    {
        
        //Car 
        public int? NumberOfSeats { get; set; } = null;
        public int? Hp { get; set; } = null;
        public double? Acceleration { get; set; } = null;
        public string TransmissionType { get; set; }
        public ChassisType ChassisType { get; set; }
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