namespace CarRentalRestApi.Models.Request
{
    public class UpdateMillage
    {
        public int VehicleId { get; set; }
        public long NewMillage { get; set; }
    }
}