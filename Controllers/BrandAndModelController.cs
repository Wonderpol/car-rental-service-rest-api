using System.Net;
using System.Security.Claims;
using CarRentalRestApi.Dtos.RentDtos;
using CarRentalRestApi.Models.Auth;
using CarRentalRestApi.Models.Request;
using CarRentalRestApi.Models.Responses;
using CarRentalRestApi.Models.VehicleModels;
using CarRentalRestApi.Services.BrandService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalRestApi.Controllers
{
    [ApiController]
    [Route("/api/brand-and-model")]
    [Authorize]
    public class BrandAndModelController : ControllerBase
    {

        private readonly BrandAndModelService _brandAndModelService;

        public BrandAndModelController(BrandAndModelService brandAndModelService)
        {
            _brandAndModelService = brandAndModelService;
        }

        [HttpPost("addBrand")]
        public ActionResult<ServiceResponse<Brand>> AddNewBrand(Brand brand)
        {
            var response = _brandAndModelService.AddNewBrand(brand);
            if (response.Data == null)
            {
                return StatusCode((int)response.HttpStatusCode);
            }

            return Ok(response);
        }

        [HttpPost("addModel")]
        public ActionResult<ServiceResponse<Model>> AddNewModel(AddModelRequest addModelRequest)
        {
            var response = _brandAndModelService.AddNewModel(addModelRequest);
            if (response.Data == null)
            {
                return StatusCode((int)response.HttpStatusCode);
            }

            return Ok(response);
        }

    }
}