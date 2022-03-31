using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalRestApi.Models;

namespace CarRentalRestApi.Services.VehicleService
{
    public interface IVehicleService
    {
       Task<ServiceResponse<List<Vehicle>>> GetAllVehicles();
        Task<ServiceResponse<Vehicle>> GetVehicleById(int id);
        Task<ServiceResponse<List<Vehicle>>> AddVehicle(Vehicle newVehicle);
    }
}