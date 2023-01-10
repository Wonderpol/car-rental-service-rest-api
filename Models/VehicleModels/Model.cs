using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CarRentalRestApi.Models.VehicleModels
{
    public class Model
    {
        [Key]
        public int Id { get; set; }

        public String name { get; set; }

        [JsonIgnore]
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
    }
}