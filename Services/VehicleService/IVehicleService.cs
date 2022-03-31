using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalRestApi.Models;

namespace CarRentalRestApi.Services.VehicleService
{
    public interface IVehicleService
    {
       Task<List<Vehicle>> GetAllVehicles();
        Task<Vehicle> GetVehicleById(int id);
        Task<List<Vehicle>> AddVehicle(Vehicle newVehicle);
    }
}