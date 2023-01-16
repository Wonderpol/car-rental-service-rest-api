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
            var response = new ServiceResponse<Brand>();

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

        public Brand GetBrandByName(string name)
        {
            return _dataContext.Brands.FirstOrDefault(b => b.name.Equals(name));
        }

        public Model GetModelByName(string name)
        {
            return _dataContext.Models.Include(m => m.Brand)
                .FirstOrDefault(m => m.name.Equals(name));
        }

        public ServiceResponse<List<Model>> GetModelsListByBrand(string brand)
        {
            var obtainedBrand = _dataContext.Brands.FirstOrDefault(b => b.name.Equals(brand));

            if (obtainedBrand == null)
                return new ServiceResponse<List<Model>>
                {
                    Message = "Brand does not exists",
                    Success = false
                };

            var models = _dataContext.Models
                .Include(m => m.Brand)
                .Where(m => m.Brand.Id == obtainedBrand.Id)
                .ToList();

            return new ServiceResponse<List<Model>>
            {
                Data = models,
                Message = "This is your awesome data",
                Success = true
            };
        }

        public ServiceResponse<List<Brand>> GetAllBrands()
        {
            var brands = _dataContext.Brands.ToList();

            var response = new ServiceResponse<List<Brand>>
            {
                Data = brands,
                Message = "Your awesome response",
                Success = true
            };

            return response;
        }

        public ServiceResponse<List<Model>> GetAllModels()
        {
            var models = _dataContext.Models
                .Include(m => m.Brand).ToList();

            var response = new ServiceResponse<List<Model>>
            {
                Data = models,
                Message = "Your awesome response",
                Success = true
            };

            return response;
        }

        public ServiceResponse<Model> AddNewModel(AddModelRequest addModelRequest)
        {
            var response = new ServiceResponse<Model>();

            var brand = _dataContext.Brands.FirstOrDefault(brand => brand.name.Equals(addModelRequest.BrandName));
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

            var model = new Model
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