using System;
using System.Threading.Tasks;
using CarRentalRestApi.Data;
using CarRentalRestApi.Models.VehicleModels;
using Microsoft.EntityFrameworkCore;

namespace CarRentalRestApi.Services
{
    public class ChassisTypeService
    {
        private readonly DataContext _dataContext;

        public ChassisTypeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async void AddChassisType(ChassisType chassisType)
        {
            _dataContext.ChassisTypes.Add(chassisType);
            await _dataContext.SaveChangesAsync();
        }
        public async Task<ChassisType> ObtainChassisTypeByName(String name)
        {
            return await _dataContext.ChassisTypes.FirstOrDefaultAsync(type => type.Chassis == name);
        }
    }
}