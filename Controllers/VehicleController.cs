using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalRestApi.Dtos.Vehicles;
using CarRentalRestApi.Models;
using CarRentalRestApi.Models.Responses;
using CarRentalRestApi.Services.VehicleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace CarRentalRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<AddVehicleDto>>>> AddVehicle(AddVehicleDto newVehicle)
        {
            return Ok(await _vehicleService.AddVehicle(newVehicle));
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetVehicleDto>>>> DeleteVehicle(int id)
        {
            var response = await _vehicleService.DeleteVehicle(id);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetVehicleDto>> UpdateVehicle(UpdateVehicleDto updateVehicleDto)
        {
            var response = await _vehicleService.UpdateVehicle(updateVehicleDto);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
            
        }

    }
}