using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalRestApi.Dtos.Vehicles;
using CarRentalRestApi.Dtos.Vehicles.CaravanDtos;
using CarRentalRestApi.Dtos.Vehicles.CarDtos;
using CarRentalRestApi.Models.Responses;
using CarRentalRestApi.Services.FilesService;
using CarRentalRestApi.Services.VehicleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalRestApi.Controllers
{
    [ApiController]
    [Route("/api/vehicle")]
    [Authorize]
    public class VehicleController: ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IFileService _fileService;

        public VehicleController(IVehicleService vehicleService, IFileService fileService)
        {
            _vehicleService = vehicleService;
            _fileService = fileService;
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<GetVehicleDto>>>> Get()
        {
            return Ok(await _vehicleService.GetAllVehicles());
        }
        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<GetVehicleDto>>> GetSingle(int id)
        {
            return Ok(await _vehicleService.GetVehicleById(id));
        }

        [HttpGet("image/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<byte[]>> GetVehicleImage(int id)
        {
            var image = await _fileService.GetVehicleImage(id);
            return File(image.Item1, image.Item2);
        }

        [HttpPost("addCar")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetVehicleDto>>>> AddVehicle([FromForm] AddCarDto newCar, [FromForm] IFormFile image)
        {
            return Ok(await _vehicleService.AddCar(newCar, image));
        }
        
        [HttpPost("addCaravan")]
        [Authorize(Roles = "Admin")]
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

        [HttpPut("updateCar")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetVehicleDto>> UpdateCar(UpdateCarDto updateCarDto)
        {
            var response = await _vehicleService.UpdateCar(updateCarDto);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
            
        }
        
        [HttpPut("updateCaravan")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetVehicleDto>> UpdateCaravan(UpdateCaravanDto updateCaravanDto)
        {
            var response = await _vehicleService.UpdateCaravan(updateCaravanDto);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
            
        }

    }
}