using AutoMapper;
using CarRentalRestApi.Dtos.User;
using CarRentalRestApi.Dtos.Vehicles;
using CarRentalRestApi.Dtos.Vehicles.CaravanDtos;
using CarRentalRestApi.Dtos.Vehicles.CarDtos;
using CarRentalRestApi.Models;
using CarRentalRestApi.Models.VehicleModels;

namespace CarRentalRestApi
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            // CreateMap<Vehicle, GetVehicleDto>();
            CreateMap<Caravan, GetVehicleDto>();
            CreateMap<Car, GetVehicleDto>();
            CreateMap<AddVehicleDto, Vehicle>();
            CreateMap<AddCarDto, Car>();
            CreateMap<AddCaravanDto, Caravan>();
            CreateMap<User, UserGetDto>();
        }
    }
}