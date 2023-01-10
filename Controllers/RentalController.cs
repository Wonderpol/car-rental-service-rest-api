using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRentalRestApi.Dtos.RentDtos;
using CarRentalRestApi.Models.Responses;
using CarRentalRestApi.Services.RentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalRestApi.Controllers
{
    [ApiController]
    [Route("/api/rent")]
    [Authorize]
    public class RentalController : ControllerBase
    {
        private readonly IRentService _rentService;

        public RentalController(IRentService rentService)
        {
            _rentService = rentService;
        }

        [HttpGet("getAllRentals")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<RentGetDto>>>> GetAllRentals()
        {
            return Ok(await _rentService.GetAllRentals());
        }


        [HttpPost("rentVehicle")]
        public async Task<ActionResult<ServiceResponse<List<RentGetDto>>>> AddNewRent(AddRentDto addRentDto)
        {
            var userId = int.Parse(User.Claims.First(cla => cla.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _rentService.AddNewRent(addRentDto, userId));
        }

        [HttpPost("getNotAvailableDates")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<DateTimeOffset>>>> GetVehicleRentalDates(int vehicleId)
        {
            return Ok(await _rentService.GetVehicleRentalDates(vehicleId));
        }

        [HttpGet("getMyRentals")]
        public async Task<ActionResult<ServiceResponse<List<RentGetDto>>>> GetMyRentals()
        {
            var userId = int.Parse(User.Claims.First(cla => cla.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _rentService.GetMyRents(userId));
        }
    }
}