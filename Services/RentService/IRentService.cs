using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalRestApi.Dtos.RentDtos;
using CarRentalRestApi.Models.RentVehicleModels;
using CarRentalRestApi.Models.Responses;

namespace CarRentalRestApi.Services.RentService
{
    public interface IRentService
    {
        Task<ServiceResponse<List<RentGetDto>>> GetAllRentals();
        Task<ServiceResponse<int>> AddNewRent(AddRentDto addRentDto, int userId);
        Task<ServiceResponse<List<DateTimeOffset>>> GetVehicleRentalDates(int vehicleId);

        Task<ServiceResponse<List<RentGetDto>>> GetMyRents(int userId);
    }
}