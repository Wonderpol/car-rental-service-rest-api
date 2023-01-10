using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalRestApi.Models.VehicleModels
{
    public class Car: Vehicle
    {
        public int NumberOfSeats { get; set; }
        public double Acceleration { get; set; }
    }
}