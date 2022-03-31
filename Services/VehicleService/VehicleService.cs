using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalRestApi.Models;

namespace CarRentalRestApi.Services.VehicleService
{
    public class VehicleService: IVehicleService
    {
        private static List<Vehicle> vehicles = new List<Vehicle>
        {
            new Vehicle(), new Vehicle{Id = 1, Brand = "Audi"}
        };
        
        public async Task<ServiceResponse<List<Vehicle>>> GetAllVehicles()
        {
            var response = new ServiceResponse<List<Vehicle>>
            {
                Data = vehicles
            };
            return response;
        }

        public async Task<ServiceResponse<Vehicle>> GetVehicleById(int id)
        {
            var response = new ServiceResponse<Vehicle>
            {
                Data = vehicles.FirstOrDefault(car => car.Id == id)
            };
            return response;
        }

        public async Task<ServiceResponse<List<Vehicle>>> AddVehicle(Vehicle newVehicle)
        {
            var response = new ServiceResponse<List<Vehicle>>();
            vehicles.Add(newVehicle);
            response.Data = vehicles;
            return response;
        }
    }
}