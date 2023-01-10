using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalRestApi.Dtos.Vehicles;
using CarRentalRestApi.Dtos.Vehicles.CaravanDtos;
using CarRentalRestApi.Dtos.Vehicles.CarDtos;
using CarRentalRestApi.Models.Request;
using CarRentalRestApi.Models.Responses;
using Microsoft.AspNetCore.Http;

namespace CarRentalRestApi.Services.VehicleService
{
    public interface IVehicleService
    {
       Task<ServiceResponse<List<GetVehicleDto>>> GetAllVehicles();
        Task<ServiceResponse<GetVehicleDto>> GetVehicleById(int id);
        // Task<ServiceResponse<List<GetVehicleDto>>> AddCar(AddCarDto newCar, IFormFile image);
        Task<ServiceResponse<List<GetVehicleDto>>> AddCar(AddCarDto newCar, IFormFile image);
        Task<ServiceResponse<List<GetVehicleDto>>> AddCaravan(AddCaravanDto newCaravan, IFormFile image);

        Task<ServiceResponse<List<GetVehicleDto>>> DeleteVehicle(int id);

        Task<ServiceResponse<GetVehicleDto>> UpdateCar(UpdateCarDto updatedCar);
        Task<ServiceResponse<GetVehicleDto>> UpdateCaravan(UpdateCaravanDto updatedCaravan);

        Task<ServiceResponse<GetVehicleDto>> UpdateCaravanMillage(UpdateMillage updatedCaravanUpdateMillage);
        Task<ServiceResponse<GetVehicleDto>> UpdateCarMillage(UpdateMillage updatedCaravanUpdateMillage);


    }
}