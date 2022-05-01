namespace CarRentalRestApi.Models.VehicleModels
{
    public class Car: Vehicle
    {
        public int NumberOfSeats { get; set; }
        public double Acceleration { get; set; }
        public string TransmissionType { get; set; }
        public string ChassisType { get; set; }
    }
}