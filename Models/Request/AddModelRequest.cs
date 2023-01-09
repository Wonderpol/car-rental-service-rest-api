using System;
using CarRentalRestApi.Models.VehicleModels;

namespace CarRentalRestApi.Models.Request
{
    public class AddModelRequest
    {
        public String ModelName { get; set; }
        public String BrandName { get; set; }
    }
}