using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarRentalRestApi.Models.Auth;

namespace CarRentalRestApi.Models.VehicleModels
{
    public abstract class Vehicle
    {
        [Key] public int Id { get; set; }

        [ForeignKey("BrandId")] public Brand Brand { get; set; }

        public int Year { get; set; }
        public int HorsePower { get; set; }
        public long Millage { get; set; }
        public long VinNumber { get; set; }

        [ForeignKey("ModelId")] public Model Model { get; set; }

        public double PricePerDay { get; set; }
        public string RegistrationPlate { get; set; }
        public Type TypeOfVehicle { get; set; }

        [ForeignKey("ChassisTypeId")] public ChassisType ChassisType { get; set; }

        [ForeignKey("TransmissionTypeId")] public TransmissionType TransmissionType { get; set; }

        public bool IsArchived { get; set; } = false;
    }
}