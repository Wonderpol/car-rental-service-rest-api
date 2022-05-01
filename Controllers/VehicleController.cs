using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalRestApi.Dtos.Vehicles;
using CarRentalRestApi.Dtos.Vehicles.CaravanDtos;
using CarRentalRestApi.Dtos.Vehicles.CarDtos;
using CarRentalRestApi.Models.Responses;
using CarRentalRestApi.Services.VehicleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalRestApi.Controllers
{
    [ApiController]
    [Route("/api/vehicle")]
    [Authorize]
    public class VehicleController: ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<GetVehicleDto>>>> Get()
        {
            return Ok(await _vehicleService.GetAllVehicles());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetVehicleDto>>> GetSingle(int id)
        {
            return Ok(await _vehicleService.GetVehicleById(id));
        }

        [HttpPost("addCar")]
        // [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<GetVehicleDto>>>> AddVehicle(AddCarDto newCar)
        {
            return Ok(await _vehicleService.AddCar(newCar));
        }
        
        [HttpPost("addCaravan")]
        // [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<GetVehicleDto>>>> AddCaravan(AddCaravanDto newCaravan)
        {
            return Ok(await _vehicleService.AddCaravan(newCaravan));
        }


        [HttpDelete("removeVehicle/{id}")]
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

        [HttpPut("updateVehicle")]
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