using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRentalRestApi.Dtos.Vehicles;
using CarRentalRestApi.Models;

namespace CarRentalRestApi.Services.VehicleService
{
    public class VehicleService: IVehicleService
    {
        private readonly IMapper _mapper;

        private static List<Vehicle> vehicles = new()
        {
            new Vehicle(), new Vehicle{Id = 1, Brand = "Audi"}
        };

        public VehicleService(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        public async Task<ServiceResponse<List<GetVehicleDto>>> GetAllVehicles()
        {
            var response = new ServiceResponse<List<GetVehicleDto>>
            {
                Data = vehicles.Select(veh => _mapper.Map<GetVehicleDto>(veh)).ToList()
            };
            return response;
        }

        public async Task<ServiceResponse<GetVehicleDto>> GetVehicleById(int id)
        {
            var response = new ServiceResponse<GetVehicleDto>
            {
                Data = _mapper.Map<GetVehicleDto>(vehicles.FirstOrDefault(car => car.Id == id))
            };
            return response;
        }

        public async Task<ServiceResponse<List<GetVehicleDto>>> AddVehicle(AddVehicleDto newVehicle)
        {
            var response = new ServiceResponse<List<GetVehicleDto>>();
            vehicles.Add(_mapper.Map<Vehicle>(newVehicle));
            response.Data = vehicles.Select(veh => _mapper.Map<GetVehicleDto>(veh)).ToList();
            return response;
        }
    }
}