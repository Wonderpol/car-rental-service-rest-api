using AutoMapper;
using CarRentalRestApi.Dtos.RentDtos;
using CarRentalRestApi.Dtos.User;
using CarRentalRestApi.Dtos.Vehicles;
using CarRentalRestApi.Dtos.Vehicles.CaravanDtos;
using CarRentalRestApi.Dtos.Vehicles.CarDtos;
using CarRentalRestApi.Models.Auth;
using CarRentalRestApi.Models.RentVehicleModels;
using CarRentalRestApi.Models.VehicleModels;

namespace CarRentalRestApi
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Caravan, GetVehicleDto>();
            CreateMap<Car, GetVehicleDto>();

            CreateMap<AddCarDto, Car>()
                .ForMember(dest => dest.ChassisType,
                    act => act.Ignore())
                .ForMember(dest => dest.Brand,
                    act => act.Ignore())
                .ForMember(dest => dest.Model,
                    act => act.Ignore())
                .ForMember(dest => dest.TransmissionType,
                    act => act.Ignore());
                

            CreateMap<Car, AddCarDto>();

            CreateMap<AddCaravanDto, Caravan>()
                .ForMember(dest => dest.ChassisType,
                    act => act.Ignore())
                .ForMember(dest => dest.Brand,
                    act => act.Ignore())
                .ForMember(dest => dest.Model,
                    act => act.Ignore())
                .ForMember(dest => dest.TransmissionType,
                    act => act.Ignore());
            
            CreateMap<User, UserGetDto>();
            
            CreateMap<UpdateCarDto, Car>();
            CreateMap<UpdateCaravanDto, Caravan>();

            CreateMap<Rent, RentGetDto>();
            CreateMap<RentGetDto, Rent>();

        }
    }
}