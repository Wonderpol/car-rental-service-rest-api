using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalRestApi.Dtos.Vehicles;
using CarRentalRestApi.Models;
using CarRentalRestApi.Services.VehicleService;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<ServiceResponse<List<GetVehicleDto>>>> Get()
        {
            return Ok(await _vehicleService.GetAllVehicles());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetVehicleDto>>> GetSingle(int id)
        {
            return Ok(await _vehicleService.GetVehicleById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<AddVehicleDto>>>> AddVehicle(AddVehicleDto newVehicle)
        {
            return Ok(await _vehicleService.AddVehicle(newVehicle));
        }

    }
}