using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalRestApi.Dtos.Vehicles;
using CarRentalRestApi.Dtos.Vehicles.CaravanDtos;
using CarRentalRestApi.Dtos.Vehicles.CarDtos;
using CarRentalRestApi.Models.Request;
using CarRentalRestApi.Models.Responses;
using CarRentalRestApi.Models.VehicleModels;
using CarRentalRestApi.Services.BrandService;
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
    public class VehicleController : ControllerBase
    {
        private readonly BrandAndModelService _brandAndModelService;
        private readonly IFileService _fileService;
        private readonly IVehicleService _vehicleService;


        public VehicleController(IVehicleService vehicleService, IFileService fileService,
            BrandAndModelService brandAndModelService)
        {
            _vehicleService = vehicleService;
            _fileService = fileService;
            _brandAndModelService = brandAndModelService;
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<GetVehicleDto>>>> Get()
        {
            return Ok(await _vehicleService.GetAllVehicles());
        }
        
        [HttpPost("archiveVehicle")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<bool>>> ArchiveVehicle(int id)
        {
            return Ok(await _vehicleService.ArchiveVehicle(id));
        }

        [HttpGet("getAllBrands")]
        [AllowAnonymous]
        public ActionResult<ServiceResponse<List<Brand>>> GetAllBrands()
        {
            return Ok(_brandAndModelService.GetAllBrands());
        }

        [HttpGet("getAllModels")]
        [AllowAnonymous]
        public ActionResult<ServiceResponse<List<Brand>>> GetAllModels()
        {
            return Ok(_brandAndModelService.GetAllModels());
        }

        [HttpGet("getAllBrandModels")]
        [AllowAnonymous]
        public ActionResult<ServiceResponse<List<Brand>>> GetAllBrandModels(string brandName)
        {
            return Ok(_brandAndModelService.GetModelsListByBrand(brandName));
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
        public async Task<ActionResult<ServiceResponse<List<GetVehicleDto>>>> AddVehicle([FromForm] AddCarDto newCar,
            [FromForm] IFormFile image)
        {
            return Ok(await _vehicleService.AddCar(newCar, image));
        }

        [HttpPost("addCaravan")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetVehicleDto>>>> AddCaravan(
            [FromForm] AddCaravanDto newCaravan, [FromForm] IFormFile image)
        {
            return Ok(await _vehicleService.AddCaravan(newCaravan, image));
        }


        [HttpDelete("removeVehicle/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetVehicleDto>>>> DeleteVehicle(int id)
        {
            var response = await _vehicleService.DeleteVehicle(id);
            if (!response.Success) return NotFound(response);

            return Ok(response);
        }

        [HttpPut("updateCar")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetVehicleDto>> UpdateCar(UpdateCarDto updateCarDto)
        {
            var response = await _vehicleService.UpdateCar(updateCarDto);
            if (response.Data == null) return NotFound(response);

            return Ok(response);
        }

        [HttpPut("updateCarMillage")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetVehicleDto>> UpdateCarMillage(UpdateMillage updateCarDto)
        {
            var response = await _vehicleService.UpdateCarMillage(updateCarDto);
            if (response.Data == null) return NotFound(response);

            return Ok(response);
        }


        [HttpPut("updateCaravan")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetVehicleDto>> UpdateCaravan(UpdateCaravanDto updateCaravanDto)
        {
            var response = await _vehicleService.UpdateCaravan(updateCaravanDto);
            if (response.Data == null) return NotFound(response);

            return Ok(response);
        }

        [HttpPut("updateCaravanMillage")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetVehicleDto>> UpdateCaravanMillage(UpdateMillage updateCaravanDto)
        {
            var response = await _vehicleService.UpdateCaravanMillage(updateCaravanDto);
            if (response.Data == null) return NotFound(response);

            return Ok(response);
        }
    }
}