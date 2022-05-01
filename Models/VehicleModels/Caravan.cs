namespace CarRentalRestApi.Models.VehicleModels
{
    public class Caravan: Vehicle
    {
        public double Space { get; set; } 
        public bool IsBathroomInside { get; set; }
        public int NumberOfAllowedPeople { get; set; }
        public double TotalLength { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int NumberOfAxis { get; set; }
    }
}