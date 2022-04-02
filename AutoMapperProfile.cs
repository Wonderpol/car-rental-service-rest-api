using AutoMapper;
using CarRentalRestApi.Dtos.User;
using CarRentalRestApi.Dtos.Vehicles;
using CarRentalRestApi.Models;

namespace CarRentalRestApi
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Vehicle, GetVehicleDto>();
            CreateMap<AddVehicleDto, Vehicle>();
            CreateMap<User, UserGetDto>();
        }
    }
}