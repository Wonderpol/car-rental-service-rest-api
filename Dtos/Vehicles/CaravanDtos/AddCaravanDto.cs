using CarRentalRestApi.Models;

namespace CarRentalRestApi.Dtos.Vehicles.CaravanDtos
{
    public class AddCaravanDto
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
        
        public double Space { get; set; }
        public bool IsBathroomInside { get; set; }
        public int NumberOfAllowedPeople { get; set; }
        public double TotalLenght { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int NumberOfAxis { get; set; }
    }
}