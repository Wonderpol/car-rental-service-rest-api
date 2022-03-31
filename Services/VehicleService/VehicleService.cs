using System;
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
            Vehicle vehicle = _mapper.Map<Vehicle>(newVehicle);
            vehicle.Id = vehicles.Max(veh => veh.Id) + 1;
            vehicles.Add(vehicle);
            response.Data = vehicles.Select(veh => _mapper.Map<GetVehicleDto>(veh)).ToList();
            return response;
        }

        public async Task<ServiceResponse<List<GetVehicleDto>>> DeleteVehicle(int id)
        {
            var response = new ServiceResponse<List<GetVehicleDto>>();
            vehicles.Remove(vehicles.Find(veh => veh.Id == id));
            response.Data = vehicles.Select(veh => _mapper.Map<GetVehicleDto>(veh)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetVehicleDto>> UpdateVehicle(UpdateVehicleDto updatedVehicle)
        {
            var response = new ServiceResponse<GetVehicleDto>();

            try
            {
                Vehicle vehicle = vehicles.FirstOrDefault(veh => veh.Id == updatedVehicle.Id);

                vehicle.Brand = updatedVehicle.Brand;
                vehicle.Millage = updatedVehicle.Millage;
                vehicle.HorsePower = updatedVehicle.HorsePower;
                vehicle.TypeOfVehicle = updatedVehicle.TypeOfVehicle;
                vehicle.Year = updatedVehicle.Year;

                response.Data = _mapper.Map<GetVehicleDto>(vehicle);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}