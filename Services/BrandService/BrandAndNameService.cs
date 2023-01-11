using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using CarRentalRestApi.Data;
using CarRentalRestApi.Models.Request;
using CarRentalRestApi.Models.Responses;
using CarRentalRestApi.Models.VehicleModels;
using Microsoft.EntityFrameworkCore;

namespace CarRentalRestApi.Services.BrandService
{
    public class BrandAndModelService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public BrandAndModelService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        
        public ServiceResponse<Brand> AddNewBrand(Brand brand)
        {
            ServiceResponse <Brand> response = new ServiceResponse<Brand>();

            var obtainValue = _dataContext.Brands.FirstOrDefault(br => br.name.Equals(brand.name));
            if (obtainValue == null)
            {
                _dataContext.Brands.Add(brand);
                _dataContext.SaveChanges();
                response.Message = "Added new brand";
                response.Success = true;
                return response;
            }

            response.Success = false;
            response.Message = "Already exists";
            response.HttpStatusCode = HttpStatusCode.Conflict;
            
            return response;
        }

        public Brand GetBrandByName(String name)
        {
            return _dataContext.Brands.FirstOrDefault(b => b.name.Equals(name));
        }

        public Model GetModelByName(String name)
        {
            return _dataContext.Models.Include(m => m.Brand)
                .FirstOrDefault(m => m.name.Equals(name));
        }

        public ServiceResponse<List<Model>> GetModelsListByBrand(String brand)
        {
            var obtainedBrand = _dataContext.Brands.FirstOrDefault(b => b.name.Equals(brand));

            if (obtainedBrand == null)
            {
                return new ServiceResponse<List<Model>>
                {
                    Message = "Brand does not exists",
                    Success = false
                };
            }

            return new ServiceResponse<List<Model>>
            {
                Data = _dataContext.Models.Where(model => model.Id.Equals(obtainedBrand.Id)).ToList(),
                Message = "This is your awesome data",
                Success = true
            };
        }

        public ServiceResponse<List<Brand>> GetAllBrands()
        {
            List<Brand> brands = _dataContext.Brands.ToList();

            ServiceResponse<List<Brand>> response = new ServiceResponse<List<Brand>>
            {
                Data = brands,
                Message = "Your awesome response",
                Success = true,
            };

            return response;
        }

        public ServiceResponse<Model> AddNewModel(AddModelRequest addModelRequest)
        {
            ServiceResponse<Model> response = new ServiceResponse<Model>();

            Brand brand = _dataContext.Brands.FirstOrDefault(brand => brand.name.Equals(addModelRequest.BrandName));
            var obtainModel = _dataContext.Models.FirstOrDefault(m => m.name.Equals(addModelRequest.ModelName));


            if (brand == null)
            {
                response.Message = "Brand " + addModelRequest.BrandName + " not found";
                response.Success = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;

                return response;
            }

            if (obtainModel != null)
            {
                response.Message = "Model " + addModelRequest.ModelName + " already exists";
                response.Success = false;
                response.HttpStatusCode = HttpStatusCode.Conflict;
                return response;
            }

            Model model = new Model
            {
                Brand = brand,
                name = addModelRequest.ModelName
            };

            _dataContext.Models.Add(model);
            _dataContext.SaveChanges();

            response.Data = model;
            response.Success = true;
            
            return response;
        }
    }
}