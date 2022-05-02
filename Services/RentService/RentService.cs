using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CarRentalRestApi.Data;
using CarRentalRestApi.Dtos.RentDtos;
using CarRentalRestApi.Models.Auth;
using CarRentalRestApi.Models.RentVehicleModels;
using CarRentalRestApi.Models.Responses;
using CarRentalRestApi.Models.VehicleModels;
using CarRentalRestApi.Services.AuthService;
using CarRentalRestApi.Services.VehicleService;
using CarRentalRestApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace CarRentalRestApi.Services.RentService
{
    public class RentService: IRentService
    {
        
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly IAuthService _authService;
        private readonly IVehicleService _vehicleService;

        public RentService(IMapper mapper, DataContext dataContext, IAuthService authService, IVehicleService vehicleService)
        {
            _mapper = mapper;
            _dataContext = dataContext;
            _authService = authService;
            _vehicleService = vehicleService;
        }
        
        public Task<ServiceResponse<List<RentGetDto>>> GetAllRentals()
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse<int>> AddNewRent(AddRentDto addRentDto, int userId)
        {
            var response = new ServiceResponse<int>();

            var user = await _authService.GetUserById(userId);

            var vehicle = await _dataContext.Vehicles.FirstOrDefaultAsync(veh => veh.Id == addRentDto.VehicleId);
            
            
            var rentAllDates = DateUtils.GetDatesBetweenTwoDates(
                addRentDto.StartRentTimestamp.ConvertTimestampToDateTimeOffset(),
                addRentDto.EndRentTimestamp.ConvertTimestampToDateTimeOffset());

            var alreadyReservedDays = this.GetVehicleRentalDates(addRentDto.VehicleId).Result.Data;

            var canReserve = DateUtils.CheckIfCanRentVehicleBasedOnTime(addRentDto.StartRentTimestamp, addRentDto.EndRentTimestamp,
                alreadyReservedDays);
            
            //Check whether car is not reserved in wanted time
            if (canReserve)
            {
                var newRent = new Rent
                {
                    User = user,
                    Vehicle = vehicle,
                    StartRentTimestamp = addRentDto.StartRentTimestamp,
                    EndRentTimestamp = addRentDto.EndRentTimestamp
                };

                _dataContext.Rents.Add(newRent);
            
                await _dataContext.SaveChangesAsync();

                response.Data = newRent.Id;
                response.Message = "You rented a vehicle";
                return response;
            }


            response.Success = false;
            response.Message = "This date is already taken";
            
            return response;
        }

        public async Task<ServiceResponse<List<DateTimeOffset>>> GetVehicleRentalDates(int vehicleId)
        {
            var response = new ServiceResponse<List<DateTimeOffset>>();

            var rents = await _dataContext.Rents.Where(rent => rent.Vehicle.Id == vehicleId).ToListAsync();

            var allRentDatesForVehicle = new List<DateTimeOffset>();

            foreach (var rent in rents)
            {
                var rentAllDates = DateUtils.GetDatesBetweenTwoDates(
                    DateUtils.ConvertTimestampToDateTimeOffset(rent.StartRentTimestamp),
                    DateUtils.ConvertTimestampToDateTimeOffset(rent.EndRentTimestamp));
                allRentDatesForVehicle.AddRange(rentAllDates);
            }

            response.Data = allRentDatesForVehicle;

            return response;
        }
    }
}