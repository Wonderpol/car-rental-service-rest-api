namespace CarRentalRestApi.Dtos.Vehicles.CaravanDtos
{
    public class UpdateCaravanDto: VehicleDto
    {
        public double Space { get; set; } 
        public bool IsBathroomInside { get; set; }
        public int NumberOfAllowedPeople { get; set; }
        public double TotalLenght { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int NumberOfAxis { get; set; }
    }
}