using System;
using System.ComponentModel.DataAnnotations;

namespace CarRentalRestApi.Models.VehicleModels
{
    public class TransmissionType
    {
        [Key]
        public int Id { get; set; }

        public String name { get; set; }
    }
}