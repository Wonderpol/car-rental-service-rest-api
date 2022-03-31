using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalRestApi.Models;
using CarRentalRestApi.Services.VehicleService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CarRentalRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController: ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Vehicle>>>> Get()
        {
            return Ok(await _vehicleService.GetAllVehicles());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> GetSingle(int id)
        {
            return Ok(await _vehicleService.GetVehicleById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Vehicle>>>> AddVehicle(Vehicle newVehicle)
        {
            return Ok(await _vehicleService.AddVehicle(newVehicle));
        }

    }
}