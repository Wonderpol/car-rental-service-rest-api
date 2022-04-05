using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRentalRestApi.Data;
using CarRentalRestApi.Dtos.Vehicles;
using CarRentalRestApi.Models;
using CarRentalRestApi.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace CarRentalRestApi.Services.VehicleService
{
    public class VehicleService: IVehicleService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        
        public VehicleService(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }
        
        public async Task<ServiceResponse<List<GetVehicleDto>>> GetAllVehicles()
        {
            var vehicles = await _dataContext.Vehicles.ToListAsync();
            var response = new ServiceResponse<List<GetVehicleDto>>
            {
                Data = vehicles.Select(veh => _mapper.Map<GetVehicleDto>(veh)).ToList()
            };
            return response;
        }

        //TODO : Check if id exists if not return 404 HTTP CODE response
        public async Task<ServiceResponse<GetVehicleDto>> GetVehicleById(int id)
        {
            var vehicle = await _dataContext.Vehicles.FirstOrDefaultAsync(veh => veh.Id == id);
            var response = new ServiceResponse<GetVehicleDto>
            {
                Data = _mapper.Map<GetVehicleDto>(vehicle)
            };
            return response;
        }

        public async Task<ServiceResponse<List<GetVehicleDto>>> AddVehicle(AddVehicleDto newVehicle)
        {
            var response = new ServiceResponse<List<GetVehicleDto>>();
            Vehicle vehicle = _mapper.Map<Vehicle>(newVehicle);
            _dataContext.Vehicles.Add(vehicle);
            await _dataContext.SaveChangesAsync();
            response.Data = await _dataContext.Vehicles.Select(veh => _mapper.Map<GetVehicleDto>(veh)).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<GetVehicleDto>>> DeleteVehicle(int id)
        {
            var response = new ServiceResponse<List<GetVehicleDto>>();

            try
            {
                Vehicle vehicle = await _dataContext.Vehicles.FirstAsync(veh => veh.Id == id);
                _dataContext.Vehicles.Remove(vehicle);
                await _dataContext.SaveChangesAsync();
                
                response.Data = await _dataContext.Vehicles.Select(veh => _mapper.Map<GetVehicleDto>(veh)).ToListAsync();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            
            return response;
        }

        public async Task<ServiceResponse<GetVehicleDto>> UpdateVehicle(UpdateVehicleDto updatedVehicle)
        {
            var response = new ServiceResponse<GetVehicleDto>();

            try
            {
                Vehicle vehicle = await _dataContext.Vehicles.FirstOrDefaultAsync(veh => veh.Id == updatedVehicle.Id);
                vehicle.Brand = updatedVehicle.Brand;
                vehicle.Millage = updatedVehicle.Millage;
                vehicle.HorsePower = updatedVehicle.HorsePower;
                vehicle.TypeOfVehicle = updatedVehicle.TypeOfVehicle;
                vehicle.Year = updatedVehicle.Year;

                //Test if it works without this update call - due to the docs it should
                // _dataContext.Vehicles.Update(dbVehicle);
                await _dataContext.SaveChangesAsync();

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