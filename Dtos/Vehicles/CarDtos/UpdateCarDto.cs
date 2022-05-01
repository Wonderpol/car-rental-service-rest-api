namespace CarRentalRestApi.Dtos.Vehicles.CarDtos
{
    public class UpdateCarDto: VehicleDto
    {
        //Car 
        public int NumberOfSeats { get; set; }
        public int Hp { get; set; }
        public double Acceleration { get; set; }
        public string TransmissionType { get; set; }
        public string ChassisType { get; set; }
    }
}