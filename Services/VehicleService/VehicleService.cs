using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRentalRestApi.Data;
using CarRentalRestApi.Dtos.Vehicles;
using CarRentalRestApi.Dtos.Vehicles.CaravanDtos;
using CarRentalRestApi.Dtos.Vehicles.CarDtos;
using CarRentalRestApi.Models.Request;
using CarRentalRestApi.Models.Responses;
using CarRentalRestApi.Models.VehicleModels;
using CarRentalRestApi.Services.BrandService;
using CarRentalRestApi.Services.FilesService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Type = CarRentalRestApi.Models.Auth.Type;

namespace CarRentalRestApi.Services.VehicleService
{
    public class VehicleService : IVehicleService
    {
        private readonly ChassisTypeService _chassisTypeService;
        private readonly BrandAndModelService _brandAndModelService;
        private readonly TransmissionTypeService _transmissionTypeService;
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;


        public VehicleService(IMapper mapper, DataContext dataContext, IFileService fileService,
            ChassisTypeService chassisTypeService, BrandAndModelService brandAndModelService, TransmissionTypeService transmissionTypeService)
        {
            _mapper = mapper;
            _dataContext = dataContext;
            _fileService = fileService;
            _chassisTypeService = chassisTypeService;
            _brandAndModelService = brandAndModelService;
            _transmissionTypeService = transmissionTypeService;
        }

        public async Task<ServiceResponse<List<GetVehicleDto>>> GetAllVehicles()
        {
            var vehicles = await _dataContext.Vehicles
                .Include(veh => veh.ChassisType)
                .Include(veh => veh.TransmissionType)
                .Include(veh => veh.Brand)
                .Include(veh => veh.Model)
                .ToListAsync();
            var response = new ServiceResponse<List<GetVehicleDto>>
            {
                Data = vehicles.Select(veh => _mapper.Map<GetVehicleDto>(veh)).ToList()
            };
            return response;
        }

        //TODO : Check if id exists if not return 404 HTTP CODE response
        public async Task<ServiceResponse<GetVehicleDto>> GetVehicleById(int id)
        {
            var vehicle = await _dataContext.Vehicles
                .Include(veh => veh.ChassisType)
                .Include(veh => veh.TransmissionType)
                .Include(veh => veh.Brand)
                .Include(veh => veh.Model)
                .FirstAsync(veh => veh.Id == id);
            var response = new ServiceResponse<GetVehicleDto>
            {
                Data = _mapper.Map<GetVehicleDto>(vehicle)
            };
            return response;
        }

        public async Task<ServiceResponse<List<GetVehicleDto>>> AddCar(AddCarDto newCar, IFormFile image)
        {
            var response = new ServiceResponse<List<GetVehicleDto>>();
            var chassisType = await _chassisTypeService.ObtainChassisTypeByName(newCar.ChassisType);
            var model = _brandAndModelService.GetModelByName(newCar.Model);
            var brand = _brandAndModelService.GetBrandByName(newCar.Brand);
            var transmissionType = _transmissionTypeService.getTransmissionTypeByName(newCar.TransmissionType);
            var car = _mapper.Map<Car>(newCar);
            car.ChassisType = chassisType;
            car.Model = model;
            car.Brand = brand;
            car.TransmissionType = transmissionType;
            _dataContext.Vehicles.Add(car);


            await _dataContext.SaveChangesAsync();
            await _fileService.UploadVehiclePhoto(car.Id, image);
            response.Data = null;
            return response;
        }

        public async Task<ServiceResponse<List<GetVehicleDto>>> AddCaravan(AddCaravanDto newCaravan, IFormFile image)
        {
            var response = new ServiceResponse<List<GetVehicleDto>>();
            var model = _brandAndModelService.GetModelByName(newCaravan.Model);
            var brand = _brandAndModelService.GetBrandByName(newCaravan.Brand);
            var transmissionType = _transmissionTypeService.getTransmissionTypeByName(newCaravan.TransmissionType);
            var car = _mapper.Map<Caravan>(newCaravan);
            car.Brand = brand;
            car.Model = model;
            car.TransmissionType = transmissionType;
            _dataContext.Vehicles.Add(car);

            await _dataContext.SaveChangesAsync();
            await _fileService.UploadVehiclePhoto(car.Id, image);
            response.Data = await _dataContext.Vehicles.Select(veh => _mapper.Map<GetVehicleDto>(veh)).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<GetVehicleDto>>> DeleteVehicle(int id)
        {
            var response = new ServiceResponse<List<GetVehicleDto>>();

            try
            {
                var vehicle = await _dataContext.Vehicles.FirstAsync(veh => veh.Id == id);
                _dataContext.Vehicles.Attach(vehicle);
                _dataContext.Vehicles.Remove(vehicle);
                await _dataContext.SaveChangesAsync();

                response.Data = await _dataContext.Vehicles.Select(veh => _mapper.Map<GetVehicleDto>(veh))
                    .ToListAsync();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetVehicleDto>> UpdateCar(UpdateCarDto updatedCar)
        {
            var response = new ServiceResponse<GetVehicleDto>();

            try
            {
                var vehicle = await _dataContext.Vehicles.FirstOrDefaultAsync(veh => veh.Id == updatedCar.Id);
                if (vehicle.TypeOfVehicle == Type.Car)
                {
                    var car = _mapper.Map<Car>(updatedCar);

                    _dataContext.Vehicles.Remove(vehicle);
                    _dataContext.Vehicles.Add(car);

                    await _dataContext.SaveChangesAsync();

                    response.Data = _mapper.Map<GetVehicleDto>(car);
                }
                else
                {
                    response.Success = false;
                    response.Message = "To edit Caravan use another separate endpoint";
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }
        
        public async Task<ServiceResponse<GetVehicleDto>> UpdateCarMillage(UpdateMillage updatedMillage)
        {
            var response = new ServiceResponse<GetVehicleDto>();

            try
            {
                var vehicle = await _dataContext.Vehicles
                    .Include(veh => veh.ChassisType)
                    .Include(veh => veh.TransmissionType)
                    .Include(veh => veh.Brand)
                    .Include(veh => veh.Model)
                    .FirstAsync(veh => veh.Id == updatedMillage.VehicleId);
                if (vehicle.TypeOfVehicle == Type.Car)
                {
                    vehicle.Millage = updatedMillage.NewMillage;

                    _dataContext.Vehicles.Update(vehicle);

                    await _dataContext.SaveChangesAsync();

                    response.Data = _mapper.Map<GetVehicleDto>(vehicle);
                }
                else
                {
                    response.Success = false;
                    response.Message = "To edit Caravan millage use another separate endpoint";
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetVehicleDto>> UpdateCaravan(UpdateCaravanDto updatedCaravan)
        {
            var response = new ServiceResponse<GetVehicleDto>();

            try
            {
                var vehicle = await _dataContext.Vehicles.FirstOrDefaultAsync(veh => veh.Id == updatedCaravan.Id);
                if (vehicle.TypeOfVehicle == Type.Caravan)
                {
                    var caravan = _mapper.Map<Caravan>(updatedCaravan);

                    _dataContext.Vehicles.Remove(vehicle);
                    _dataContext.Vehicles.Add(caravan);

                    await _dataContext.SaveChangesAsync();

                    response.Data = _mapper.Map<GetVehicleDto>(caravan);
                }
                else
                {
                    response.Success = false;
                    response.Message = "To edit Car use another separate endpoint";
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }
        
        public async Task<ServiceResponse<GetVehicleDto>> UpdateCaravanMillage(UpdateMillage updatedCaravanUpdateMillage)
        {
            var response = new ServiceResponse<GetVehicleDto>();

            try
            {
                var vehicle = await _dataContext.Vehicles.FirstOrDefaultAsync(veh => veh.Id == updatedCaravanUpdateMillage.VehicleId);
                if (vehicle.TypeOfVehicle == Type.Caravan)
                {
                    vehicle.Millage = updatedCaravanUpdateMillage.NewMillage;
                    _dataContext.Vehicles.Update(vehicle);

                    await _dataContext.SaveChangesAsync();

                    response.Data = _mapper.Map<GetVehicleDto>(vehicle);
                }
                else
                {
                    response.Success = false;
                    response.Message = "To edit Car use another separate endpoint";
                }
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