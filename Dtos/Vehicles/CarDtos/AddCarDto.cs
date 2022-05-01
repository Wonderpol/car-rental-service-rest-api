using CarRentalRestApi.Models;

namespace CarRentalRestApi.Dtos.Vehicles.CarDtos
{
    public class AddCarDto
    {
        public string Brand { get; set; }
        public int Year { get; set; }
        public int HorsePower { get; set; }
        public long Millage { get; set; }
        public long VinNumber { get; set; }
        public string Model { get; set; }
        public double PricePerHour { get; set; }
        public string RegistrationPlate { get; set; }
        public Type TypeOfVehicle { get; set; }
        
        public int NumberOfSeats { get; set; }
        public int Hp { get; set; }
        public double Acceleration { get; set; }
        public string TransmissionType { get; set; }
        public string ChassisType { get; set; }
    }
}