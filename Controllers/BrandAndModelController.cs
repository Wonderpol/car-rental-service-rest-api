using System.Security.Claims;
using CarRentalRestApi.Dtos.RentDtos;
using CarRentalRestApi.Models.Auth;
using CarRentalRestApi.Models.Request;
using CarRentalRestApi.Models.Responses;
using CarRentalRestApi.Models.VehicleModels;
using CarRentalRestApi.Services.BrandService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalRestApi.Controllers
{
    [ApiController]
    [Route("/api/brand-and-model")]
    [Authorize]
    public class BrandAndModelController
    {

        private readonly BrandAndModelService _brandAndModelService;

        public BrandAndModelController(BrandAndModelService brandAndModelService)
        {
            _brandAndModelService = brandAndModelService;
        }

        [HttpPost("addBrand")]
        public ActionResult<ServiceResponse<Brand>> AddNewBrand(Brand brand)
        {
            return _brandAndModelService.AddNewBrand(brand);
        }

        [HttpPost("addModel")]
        public ActionResult<ServiceResponse<Model>> AddNewModel(AddModelRequest addModelRequest)
        {
            return _brandAndModelService.AddNewModel(addModelRequest);
        }

    }
}