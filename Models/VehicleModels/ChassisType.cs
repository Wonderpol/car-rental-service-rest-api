using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarRentalRestApi.Models.VehicleModels
{
    public class ChassisType
    {
        [Key] public int Id { get; set; }

        public string Chassis { get; set; }

        [JsonIgnore] public List<Vehicle> Vehicles { get; set; }
    }
}