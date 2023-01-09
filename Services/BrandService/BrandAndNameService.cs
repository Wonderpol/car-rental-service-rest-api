using System.Linq;
using AutoMapper;
using CarRentalRestApi.Data;
using CarRentalRestApi.Models.Responses;
using CarRentalRestApi.Models.VehicleModels;

namespace CarRentalRestApi.Services.BrandService
{
    public class BrandService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public BrandService(DataContext dataContext, IMapper mapper)
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
            
            return response;
        }
    }
}