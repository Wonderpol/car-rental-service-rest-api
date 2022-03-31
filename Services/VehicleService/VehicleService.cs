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
        
        public async Task<List<Vehicle>> GetAllVehicles()
        {
            return vehicles;
        }

        public async Task<Vehicle> GetVehicleById(int id)
        {
            return vehicles.FirstOrDefault(car => car.Id == id);
        }

        public async Task<List<Vehicle>> AddVehicle(Vehicle newVehicle)
        {
            vehicles.Add(newVehicle);
            return vehicles;
        }
    }
}