using System.Collections.Generic;
using CarRentalRestApi.Models.Auth;
using CarRentalRestApi.Models.RentVehicleModels;
using CarRentalRestApi.Models.VehicleModels;
using Microsoft.AspNetCore.Http;

namespace CarRentalRestApi.Models.Mailing
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public User User { get; set; }
        public Rent Rent { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}